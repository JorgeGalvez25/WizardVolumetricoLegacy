using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraWizard;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Interfaces;
using iGasWizardVolumetricos.Persistencia.Entidades;
using iGasWizardVolumetricos.Persistencia.Logica;
using iGasWizardVolumetricos.Persistencia.Logica.Conexion;

namespace iGasWizardVolumetricos.Pantallas.Procesando
{
    public partial class PProcesando : XtraUserControl, IUserPages
    {
        public Presenter Presenter { get; set; }
        private BaseWizardPage WizParent { get; set; }
        private IAsyncResult Async { get; set; }

        public PProcesando()
        {
            this.InitializeComponent();
            this.btnReintentar.Click += this.btnReintentar_Click;
        }

        private void btnReintentar_Click(object sender, EventArgs e)
        {
            this.Proceso();
        }

        private bool Proceso()
        {
            this.lblEstatus.Text = "Preparando proceso...";
            this.WizParent.AllowBack =
                this.WizParent.AllowNext =
                this.WizParent.AllowCancel = false;
            this.pnlProgress.Text = "0";
            bool flgError = false;
            bool flgContinue = false;
            bool result = false;

            try
            {
                this.WizParent.Cursor = Cursors.WaitCursor;
                this.lblEstatus.Text = "Actualizando BD...";
                this.pnlProgress.Text = "15";
                string msj = string.Empty;

                if (!this.Presenter.ExcecutePathScript(ref msj))
                {
                    flgError = true;
                    this.pnlProgress.Text = "50";
                    this.lblEstatus.Text = "Falló el proceso.";

                    if (msj.Contains("EXECUTE PROCEDURE EMPR_REVI"))
                    {
                        string str = msj.Substring(msj.IndexOf("EXECUTE PROCEDURE EMPR_REVI"), 34).Split(new char[] { '(' })[1];
                        msj = string.Format("Debe actualizar la Base de Datos Consola a la versión '{0}' antes de continuar.", str);
                    }

                    Utilerias.Error(msj);
                    return result;
                }

                this.pnlProgress.Text = "50";
                this.lblEstatus.Text = "Editando accesos directos...";
                if (!this.Presenter.EditShortcut(ref msj))
                {
                    flgError = true;
                    flgContinue = true;
                    Utilerias.Informacion(msj);
                    return result;
                }

                this.pnlProgress.Text = "75";
                this.lblEstatus.Text = "Configurando e instalando Servicio de Dispensarios...";
                if (!this.Presenter.ConfigurarEInstalarServicio(ref msj))
                {
                    flgError = true;
                    flgContinue = true;
                    Utilerias.Informacion(msj);
                    return result;
                }

                result = true;
            }
            catch (Exception e)
            {
                Utilerias.Error(e);
            }
            finally
            {
                this.lblEstatus.Text = string.Format("Proceso terminado {0}.", (flgError ? "con errores" : "correctamente"));
                this.pnlProgress.Text = "100";

                this.WizParent.Cursor = Cursors.Default;
                this.WizParent.AllowBack =
                    this.WizParent.AllowCancel = true;
                this.WizParent.AllowNext = !flgError || flgContinue;
            }

            return result;
        }

        #region IUserPages Members

        public void DoInit(BaseWizardPage parent)
        {
            this.WizParent = parent;

            if (this.Presenter == null)
            {
                this.Presenter = new Presenter();
            }
            parent.Text = "Aplicando configuraciones basicas a la Base de Datos y a las aplicaciones";
            this.BeginInvoke(new MethodInvoker(() =>
                {
                    //this.btnReintentar.Visible = !this.Proceso();
                    this.pnlNotificacion.Visible = !this.Proceso();
                }));
        }

        public void NextClick(object sender, WizardCommandButtonClickEventArgs e)
        {
            //Carga los combustibles precargados desde la base de datos al workitem para ser utilizados en otras pantallas.
            CombustibleVolPersistencia p = new CombustibleVolPersistencia();
            ListaCombustibleVol l = p.Obtener();
            WorkItem.Objetos<ListaCombustibleVol>.Add(l);
            Conexion.LimpiarConexiones();
        }

        public void PrevClick(object sender, WizardCommandButtonClickEventArgs e)
        {
        }

        public void CancelClick(FormClosingEventArgs e)
        {
        }

        #endregion
    }
}
