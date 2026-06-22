using System;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraWizard;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Interfaces;
using iGasWizardVolumetricos.Persistencia.Entidades;

namespace iGasWizardVolumetricos.Pantallas.UbicacionBD
{
    public partial class PUbicarBD : XtraUserControl, IUserPages
    {
        public Presenter Presenter { get; set; }
        public UbicarBD Propiedades { get; set; }

        public PUbicarBD()
        {
            this.InitializeComponent();
            this.Presenter = new Presenter();
            this.txtAlias.Properties.MaxLength = 30;
            this.txtDBPath.Properties.MaxLength = 255;
            this.txtDBPath.Properties.ReadOnly = true;
        }

        private void setEvents()
        {
            this.txtDBPath.Enter += this.txtDBPath_Enter;
            this.txtDBPath.Leave += this.txtDBPath_Leave;
            this.txtDBPath.ButtonClick += this.txtDBPath_ButtonClick;
            this.txtAlias.Leave += this.txtAlias_Leave;
        }
        private void delEvents()
        {
            this.txtDBPath.Enter -= this.txtDBPath_Enter;
            this.txtDBPath.Leave -= this.txtDBPath_Leave;
            this.txtDBPath.ButtonClick -= this.txtDBPath_ButtonClick;
            this.txtAlias.Leave -= this.txtAlias_Leave;
        }
        private bool validarPath(TextEdit ctrl)
        {
            this.lblRutaBDInfo.Text = string.Empty;
            this.pnlInfo.Visible = File.Exists(ctrl.Text);

            if (this.pnlInfo.Visible)
            {
                this.lblRutaBDInfo.Text = string.Format("PRECAUCIÓN: Ya existe una BD en la ruta especificada.\r\nEsta será borrada y reemplazada por una BD vacía.\r\n\r\n¡ESTE ASISTENTE NO REALIZA RESPALDOS DE NINGÚN TIPO!");
                return false;
            }
            return true;
        }
        private bool validarAlias(TextEdit ctrl)
        {
            string alias = ctrl.Text;
            this.lblAliasInfo.Text = string.Empty;
            this.pnlAliasInfo.Visible = (Presenter.ExisteAlias(alias));

            if (this.pnlAliasInfo.Visible)
            {
                this.lblAliasInfo.Text = string.Format("El alias {0} ya existe y será sobreescrito con la nueva configuración.", alias);
                return false;
            }
            return true;
        }
        private bool ValidaControles(string alias, string path, ref string msj)
        {
            bool result = true;

            if (string.IsNullOrEmpty(alias.Trim()))
            {
                msj = "Debe especificar un alias.";
                this.txtAlias.Focus();
                result = false;
            }
            else if (string.IsNullOrEmpty(path.Trim()))
            {
                msj = "Debe especificar una ruta hacia la Base de Datos.";
                this.txtDBPath.Focus();
                result = false;
            }

            return result;
        }

        #region Eventos

        private void txtDBPath_Enter(object sender, EventArgs e)
        {
            this.txtDBPath_ButtonClick(sender, null);
        }
        private void txtDBPath_Leave(object sender, EventArgs e)
        {
            this.validarPath((TextEdit)sender);
        }
        private void txtDBPath_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            int length = this.txtDBPath.Properties.MaxLength - 16;
            int currentLength = 0;
            string path = string.Empty;
            bool flgValido = true;

            do
            {
                flgValido = true;

                using (FolderBrowserDialog f = new FolderBrowserDialog())
                {
                    f.Description = "Seleccione una carpeta donde se encuentra o se creará la Base de Datos de Consola.";

                    if (f.ShowDialog(this.ParentForm) != DialogResult.OK)
                    {
                        break;
                    }

                    currentLength = f.SelectedPath.Length;
                    path = Path.Combine(f.SelectedPath, "GasConsola.fdb");
                }

                if (currentLength > length)
                {
                    flgValido = false;
                    Utilerias.Informacion(string.Format("La longitud de la ruta a la Base de Datos es mayor a {0}.", length));
                }
                else
                {
                    if (string.IsNullOrEmpty(path))
                    {
                        path = @"C:\ImagenCo\DBI\Consola\GasConsola.fdb";
                    }
                    this.txtDBPath.Text = path;
                    UbicarBD conf = new UbicarBD();
                    {
                        conf.Alias = this.txtAlias.Text;
                        conf.RutaBD = path;
                    }

                    if (!Presenter.BDValida(conf))
                    {
                        flgValido = false;
                        Utilerias.Informacion("La Base de Datos no es de Consola válida\r\nPor favor, verifique que sea una Base de Datos correcta.");
                    }
                }
            } while (!flgValido);
        }

        private void txtAlias_Leave(object sender, EventArgs e)
        {
            this.validarAlias((TextEdit)sender);
        }

        #endregion

        #region IUserPages Members

        public void DoInit(BaseWizardPage parent)
        {
            this.setEvents();
            parent.Text = "Configuración de la Base de Datos";

            this.BeginInvoke(new MethodInvoker(() =>
                {
                    this.Propiedades = (WorkItem.Objetos<UbicarBD>.Exist()) ? WorkItem.Objetos<UbicarBD>.Get() : new UbicarBD()
                    {
                        Alias = "GasConsolaImagen",
                        RutaBD = @"C:\ImagenCo\DBI\Consola\GasConsola.fdb",
                    };

                    this.txtAlias.Text = this.Propiedades.Alias;
                    this.txtDBPath.Text = this.Propiedades.RutaBD;

                    this.validarAlias(this.txtAlias);
                    this.validarPath(this.txtDBPath);
                }));
            this.txtAlias.Focus();
        }

        public void NextClick(object sender, WizardCommandButtonClickEventArgs e)
        {
            string msj = string.Empty;
            string path = this.txtDBPath.Text;
            string alias = this.txtAlias.Text;

            e.Handled = !this.ValidaControles(alias, path, ref msj);
            if (e.Handled)
            {
                Utilerias.Informacion(msj);
                return;
            }

            string msjTemplate = "Se reemplazará {0} existente\r\n¿Esta seguro de continuar?";

            if (File.Exists(path))
            {
                if (!Utilerias.Confirmacion(string.Format(msjTemplate, "la Base de Datos")))
                {
                    e.Handled = true;
                    return;
                }
            }

            if (Presenter.ExisteAlias(alias))
            {
                if (!Utilerias.Confirmacion(string.Format(msjTemplate, "el Alias")))
                {
                    e.Handled = true;
                    return;
                }
            }

            e.Handled = !this.Presenter.OperacionsIO(path, ref msj);
            if (e.Handled)
            {
                Utilerias.Informacion(msj);
                return;
            }

            e.Handled = !this.Presenter.OperacionesBDE(alias, path, ref msj);
            if (e.Handled)
            {
                Utilerias.Informacion(msj);
                return;
            }

            this.Propiedades = new UbicarBD()
            {
                Alias = this.txtAlias.Text,
                RutaBD = this.txtDBPath.Text
            };
            WorkItem.Objetos<UbicarBD>.Add(this.Propiedades);

            if (!this.Presenter.PruebaDeConexion(this.Propiedades, ref msj))
            {
                if (msj.Contains("I/O error during"))
                {
                    msj = "Ocurrio un error al leer la Base de Datos o no existe.";
                }

                Utilerias.Error(msj);
            }
        }

        public void PrevClick(object sender, WizardCommandButtonClickEventArgs e)
        {
            this.delEvents();
        }

        public void CancelClick(FormClosingEventArgs e)
        {
        }

        #endregion
    }
}
