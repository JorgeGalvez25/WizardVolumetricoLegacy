using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraWizard;
using iGasWizardVolumetricos.Generales;
using _u = iGasWizardVolumetricos.Generales.Utilerias;
using _c = iGasWizardVolumetricos.Generales.Constantes;
using iGasWizardVolumetricos.Interfaces;
using iGasWizardVolumetricos.Persistencia.Logica.Conexion;

namespace iGasWizardVolumetricos
{
    public partial class Wizard : XtraForm
    {
        public Wizard()
        {
            this.InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(this.WizardControles_FormClosing);
            this.wcVolumetricos.SelectedPageChanging += new WizardPageChangingEventHandler(this.wcVolumetricos_SelectedPageChanging);
            this.wcVolumetricos.PrevClick += new WizardCommandButtonClickEventHandler(this.wcVolumetricos_PrevClick);
            this.wcVolumetricos.NextClick += new WizardCommandButtonClickEventHandler(this.wcVolumetricos_NextClick);
            this.wcVolumetricos.CancelClick += new CancelEventHandler(this.wcVolumetricos_CancelClick);
            this.wcVolumetricos.FinishClick += new CancelEventHandler(this.wcVolumetricos_FinishClick);

            this.Load += new EventHandler(Wizard_Load);
            this.Shown += new EventHandler(Wizard_Shown);
            this.Disposed += new EventHandler(Wizard_Disposed);

            WorkItem.Status = InstallStatus.Successful;
        }

        #region Delegados

        delegate void cursor(Cursor valor);

        private void cursorCallBack(Cursor valor)
        {
            if (this.InvokeRequired)
            {
                cursor control = new cursor(cursorCallBack);

                this.Invoke(control, new object[] { valor });
            }
            else
            {
                this.Cursor = valor;
                Application.UseWaitCursor = valor == Cursors.WaitCursor;
            }
        }

        #endregion

        #region Eventos

        void WizardControles_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                cursorCallBack(Cursors.WaitCursor);

                if (wcVolumetricos.SelectedPage.Controls[0] is IUserPages)
                {
                    IUserPages p = (IUserPages)wcVolumetricos.SelectedPage.Controls[0];
                    p.CancelClick(e);
                }

                if (!e.Cancel)
                {
                    e.Cancel = !_u.Confirmacion(_c.MENSAJES.CONFIRMACION_SALIR);
                }

                if (!e.Cancel)
                {
                    WorkItem.Status = InstallStatus.Cancelled;
                    _u.CancelarInstalacion();
                    Environment.Exit(0);
                }
            }
            finally
            {
                cursorCallBack(Cursors.Default);
            }
        }

        void wcVolumetricos_FinishClick(object sender, CancelEventArgs e)
        {
            this.FormClosing -= new FormClosingEventHandler(WizardControles_FormClosing);
            Close();
        }

        void wcVolumetricos_CancelClick(object sender, CancelEventArgs e)
        {
            Close();
        }

        void wcVolumetricos_NextClick(object sender, WizardCommandButtonClickEventArgs e)
        {
            try
            {
                cursorCallBack(Cursors.WaitCursor);

                if (e.Page.Controls[0] is IUserPages)
                {
                    IUserPages p = (IUserPages)e.Page.Controls[0];
                    p.NextClick(sender, e);
                }
            }
            catch (Exception ex)
            {
                _u.Error(ex);
                e.Handled = true;
            }
            finally
            {
                cursorCallBack(Cursors.Default);
            }
        }

        void wcVolumetricos_PrevClick(object sender, WizardCommandButtonClickEventArgs e)
        {
            try
            {
                cursorCallBack(Cursors.WaitCursor);

                if (e.Page.Controls[0] is IUserPages)
                {
                    IUserPages p = (IUserPages)e.Page.Controls[0];
                    p.PrevClick(sender, e);
                }
            }
            finally
            {
                cursorCallBack(Cursors.Default);
            }
        }

        void wcVolumetricos_SelectedPageChanging(object sender, WizardPageChangingEventArgs e)
        {
            try
            {
                cursorCallBack(Cursors.WaitCursor);

                if (e.Direction == Direction.Forward)
                {
                    if (e.Page.Controls[0] is IUserPages)
                    {
                        try
                        {
                            IUserPages p = (IUserPages)e.Page.Controls[0];
                            p.DoInit(e.Page);
                        }
                        catch (Exception ex)
                        {
                            _u.Error(ex);
                            e.Cancel = true;
                        }
                    }
                }
            }
            finally
            {
                cursorCallBack(Cursors.Default);
            }
        }

        void Wizard_Load(object sender, EventArgs e)
        {
            this.wcVolumetricos.Pages.Clear();
            this.wcVolumetricos.BeginInvoke(new MethodInvoker(() =>
            {
                this.wcVolumetricos.BeginInit();

                //if (WorkItem.Pantallas.Length > 0) { welcomeWizardPage1.Controls.Add(WorkItem.Pantallas[0]); }
                this.wcVolumetricos.Pages.Add(welcomeWizardPage1);

                for (int i = 0; i < WorkItem.Pantallas.Length; i++)
                {
                    WorkItem.Pantallas[i].Dock = DockStyle.Fill;
                    WizardPage p = new WizardPage();
                    p.Controls.Add(WorkItem.Pantallas[i]);
                    this.wcVolumetricos.Pages.Add(p);
                }

                //if (WorkItem.Pantallas.Length > 1) { completionWizardPage1.Controls.Add(WorkItem.Pantallas[WorkItem.Pantallas.Length - 1]); }
                this.wcVolumetricos.Pages.Add(completionWizardPage1);

                //(this.wcVolumetricos.Pages[0] as IUserPages).DoInit(welcomeWizardPage1);
                this.wcVolumetricos.EndInit();
            }));
        }

        void Wizard_Shown(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.TopMost = false;
        }

        void Wizard_Disposed(object sender, EventArgs e)
        {
            var conn = WorkItem.Objetos<Conexion>.Get();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        #endregion
    }
}