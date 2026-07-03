using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraWizard;
using iGasWizardVolumetricos.Extensiones;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Interfaces;
using iGasWizardVolumetricos.Persistencia.Entidades;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace iGasWizardVolumetricos.Pantallas.Licenciamiento
{
    public partial class PLicenciamiento : XtraUserControl, IUserPages
    {
        private Presenter Presenter { get; set; }
        private Licencia Licenciamiento { get; set; }
        private Image ImagenTemporal { get; set; }

        private const string NOESTACION_VALIDATION = @"E[0-9]{5}";
        private const string GET_NOESTACION_NUMBERS_ONLY = @"[0-9]{1,5}";

        public PLicenciamiento()
        {
            this.InitializeComponent();
            this.Licenciamiento = new Licencia();
            this.Presenter = new Presenter();

            this.ConfiguraControles();
            this.CrearEventos();
        }

        private void CrearEventos()
        {
            this.chkEsTemporal.CheckedChanged += this.chkEsTemporal_CheckedChanged;
            this.chkEsTemporalInocua.CheckedChanged += this.chkEsTemporalInocua_CheckedChanged;

            this.txtRazonSocial.Enter += this.txtRazonSocial_Enter;
            this.txtRazonSocial.Leave += this.item_Leave;

            this.txtNoSentinel.Enter += this.txtNoSentinel_Enter;
            this.txtNoSentinel.Leave += this.item_Leave;
            this.txtNoSentinel.KeyPress += this.txtNoSentinel_KeyPress;

            this.txtLicenciaVolumetrico.Enter += this.txtLicenciaVolumetrico_Enter;
            this.txtLicenciaVolumetrico.Leave += this.item_Leave;

            this.txtLicenciaControlVersiones.Enter += this.txtLicenciaVolumetrico_Enter;
            this.txtLicenciaControlVersiones.Leave += this.item_Leave;

            this.txtLicenciaInocua.Enter += this.txtLicenciaVolumetrico_Enter;
            this.txtLicenciaInocua.Leave += this.item_Leave;

            this.txtLicenciaHasp.Enter += this.txtNoSentinel_Enter;
            this.txtLicenciaHasp.Leave += this.item_Leave;
            this.txtLicenciaHasp.KeyPress += this.txtLicenciaHasp_KeyPress;

            this.chkEsTemporal.Enter += this.chkEsTemporal_Enter;
            this.chkEsTemporal.Leave += this.item_Leave;

            this.chkEsTemporalInocua.Enter += this.chkEsTemporal_Enter;
            this.chkEsTemporalInocua.Leave += this.item_Leave;

            this.dtFechaVence.Enter += this.chkEsTemporal_Enter;
            this.dtFechaVence.Leave += this.item_Leave;

            this.dtFechaVenceInocua.Enter += this.chkEsTemporal_Enter;
            this.dtFechaVenceInocua.Leave += this.item_Leave;

            this.dtFechaControlVersiones.Enter += this.chkEsTemporal_Enter;
            this.dtFechaControlVersiones.Leave += this.item_Leave;

            this.txtNoEstacion.Validating += this.txtNoEstacion_Validating;
            this.txtNoEstacion.KeyPress += this.txtNoEstacion_KeyPress;
        }

        private void ConfiguraControles()
        {
            this.txtRazonSocial.Properties.MaxLength = 80;
            this.txtRazonSocial.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;

            this.txtNoSentinel.Properties.MaxLength = 5;

            this.txtLicenciaVolumetrico.Properties.MaxLength = 8;
            this.txtLicenciaVolumetrico.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;

            this.txtLicenciaInocua.Properties.MaxLength = 8;
            this.txtLicenciaInocua.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;

            this.txtLicenciaControlVersiones.Properties.MaxLength = 8;
            this.txtLicenciaControlVersiones.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;

            this.txtLicenciaHasp.Properties.MaxLength = 20;

            this.dtFechaVence.Properties.NullText = string.Empty;
            this.dtFechaVence.EditValue = null;
            this.dtFechaVence.Properties.ReadOnly = !this.chkEsTemporal.Checked;
            this.dtFechaVence.Properties.ShowClear = false;

            this.dtFechaVenceInocua.Properties.NullText = string.Empty;
            this.dtFechaVenceInocua.EditValue = null;
            this.dtFechaVenceInocua.Properties.ReadOnly = !this.chkEsTemporalInocua.Checked;
            this.dtFechaVenceInocua.Properties.ShowClear = false;

            this.dtFechaControlVersiones.Properties.NullText = string.Empty;
            this.dtFechaControlVersiones.EditValue = null;
            this.dtFechaControlVersiones.Properties.ShowClear = false;

            this.txtNoEstacion.Properties.MaxLength = 6;
            this.txtNoEstacion.Properties.CharacterCasing = CharacterCasing.Upper;
        }

        private void item_Leave(object sender, EventArgs e)
        {
            this.picExample.EditValue = this.ImagenTemporal;
        }
        private void chkEsTemporal_Enter(object sender, EventArgs e)
        {
            this.ImagenTemporal = (Image)this.picExample.EditValue;
            this.picExample.EditValue = global::iGasWizardVolumetricos.Properties.Resources.FechaVencimiento;
        }
        private void txtNoSentinel_Enter(object sender, EventArgs e)
        {
            this.ImagenTemporal = (Image)this.picExample.EditValue;
            this.picExample.EditValue = global::iGasWizardVolumetricos.Properties.Resources.Sentinel_Volumetrico;
        }
        private void txtRazonSocial_Enter(object sender, EventArgs e)
        {
            this.ImagenTemporal = (Image)this.picExample.EditValue;
            this.picExample.EditValue = global::iGasWizardVolumetricos.Properties.Resources.RazonSocial;
        }
        private void txtLicenciaVolumetrico_Enter(object sender, EventArgs e)
        {
            this.ImagenTemporal = (Image)this.picExample.EditValue;
            this.picExample.EditValue = global::iGasWizardVolumetricos.Properties.Resources.Licencia;
        }
        private void chkEsTemporal_CheckedChanged(object sender, EventArgs e)
        {
            var item = (CheckEdit)sender;
            this.dtFechaVence.Properties.ReadOnly = !item.Checked;
            this.dtFechaVence.EditValue = item.Checked ? DateTime.Now.AddMonths(1) : (DateTime?)null;
        }
        private void chkEsTemporalInocua_CheckedChanged(object sender, EventArgs e)
        {
            var item = (CheckEdit)sender;
            this.dtFechaVenceInocua.Properties.ReadOnly = !item.Checked;
            this.dtFechaVenceInocua.EditValue = item.Checked ? DateTime.Now.AddMonths(1) : (DateTime?)null;
        }
        private void txtNoSentinel_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = (int)e.KeyChar;
            if (!((key >= 48 && key <= 57) || // Numeros del 0 al 9
                (key == 38) || // El simbolo '&'
                (key == 8))) // El simbolo 'Retroceso (Borrar)'
            {
                e.Handled = true;
            }
        }
        private void txtLicenciaHasp_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = (int)e.KeyChar;
            if (!((key >= 48 && key <= 57) || // Numeros del 0 al 9
                (key == 8))) // Retroceso (Borrar)
            {
                e.Handled = true;
            }
        }
        private void txtNoEstacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            TextEdit est = (TextEdit)sender;

            est.Properties.MaxLength = (est.Text.StartsWith("E")) ? 6 : 5;

            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }

            if (!est.Text.Contains("E"))
            {
                if (e.KeyChar.Equals('E') || e.KeyChar.Equals('e'))
                {
                    e.Handled = false;
                }
            }

            if (char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
        }
        private void txtNoEstacion_Validating(object sender, CancelEventArgs e)
        {
            TextEdit est = (TextEdit)sender;
            //E[0-9]{5}
            if (!string.IsNullOrEmpty(est.Text))
            {
                if (Regex.IsMatch(est.Text, NOESTACION_VALIDATION))
                {
                    return;
                }

                Match match = Regex.Match(est.Text, GET_NOESTACION_NUMBERS_ONLY);

                if (!match.Success)
                {
                    est.Focus();
                    est.ErrorText = "Debe proporcionar un No. de Estación válido\r\nque empieze con 'E' y seguido de 5 carácteres numéricos";
                    Utilerias.Informacion("No. Estación no tiene un formato correcto.");
                    e.Cancel = true;
                }
                else
                {
                    est.Text = "E" + int.Parse(match.Value).ToString("D5");
                }
            }
        }

        #region IUserPages Members

        public void DoInit(BaseWizardPage parent)
        {
            parent.Text = "Licenciamiento";

            this.BeginInvoke(new MethodInvoker(() =>
                {
                    this.Licenciamiento = WorkItem.Objetos<EmpresaConfiguracion>.Exist() ?
                                                WorkItem.Objetos<EmpresaConfiguracion>.Get().GetLicencia() :
                                                (new Licencia());

                    this.dtFechaVence.BeginSafe(delegate { this.dtFechaVence.EditValue = this.Licenciamiento.FechaVence; });
                    this.txtRazonSocial.BeginSafe(delegate { this.txtRazonSocial.Text = this.Licenciamiento.RazonSocial; });
                    this.txtNoSentinel.BeginSafe(delegate { this.txtNoSentinel.Text = this.Licenciamiento.NoSentinel.ToString(); });
                    this.txtLicenciaVolumetrico.BeginSafe(delegate { this.txtLicenciaVolumetrico.Text = this.Licenciamiento.LicenciaVolumetrico; });
                    this.txtLicenciaHasp.BeginSafe(delegate { this.txtLicenciaHasp.Text = this.Licenciamiento.LicenciaHaspDispensarios; });
                    this.chkEsTemporal.BeginSafe(delegate { this.chkEsTemporal.Checked = this.Licenciamiento.EsTemporal; });
                    this.txtRazonSocial.Focus();
                }));
            this.txtRazonSocial.Focus();
        }

        public void NextClick(object sender, WizardCommandButtonClickEventArgs e)
        {
            string razonSocial = this.txtRazonSocial.Text;
            string noSentinel = this.txtNoSentinel.Text;
            string licVol = this.txtLicenciaVolumetrico.Text;
            string noEstacion = this.txtNoEstacion.Text.Trim();

            e.Handled = string.IsNullOrEmpty(razonSocial);
            if (e.Handled)
            {
                Utilerias.Informacion("Debe especificar una Razón Social");
                this.txtRazonSocial.Focus();
                return;
            }

            e.Handled = string.IsNullOrEmpty(noSentinel);
            if (e.Handled)
            {
                Utilerias.Informacion("Debe especificar un Número de Sentinel");
                this.txtNoSentinel.Focus();
                return;
            }

            e.Handled = (string.IsNullOrEmpty(noEstacion) && !Regex.IsMatch(noEstacion, "E[0-9]{5}"));
            if (e.Handled)
            {
                Utilerias.Informacion("Debe especificar un Número de Estación.");
                this.txtNoEstacion.Focus();
                return;
            }

            int nEstacion = 0;
            int.TryParse(noEstacion.Replace("E", string.Empty), out nEstacion);

            e.Handled = string.IsNullOrEmpty(licVol);
            if (e.Handled)
            {
                Utilerias.Informacion("Debe especificar una licencia válida");
                this.txtLicenciaVolumetrico.Focus();
                return;
            }

            string licHasp = this.txtLicenciaHasp.Text.Trim();
            e.Handled = string.IsNullOrEmpty(licHasp);
            if (e.Handled)
            {
                Utilerias.Informacion("Debe especificar la licencia HASP del Servicio de Dispensarios");
                this.txtLicenciaHasp.Focus();
                return;
            }

            int nSentinel = 0;
            int.TryParse(noSentinel, out nSentinel);

            this.Licenciamiento.RazonSocial = razonSocial;
            this.Licenciamiento.NoSentinel = nSentinel;
            this.Licenciamiento.NoEstacion = nEstacion;
            this.Licenciamiento.LicenciaVolumetrico = licVol;
            this.Licenciamiento.LicenciaHaspDispensarios = licHasp;
            this.Licenciamiento.EsTemporal = this.chkEsTemporal.Checked;
            this.Licenciamiento.FechaVence = this.chkEsTemporal.Checked ? (DateTime?)this.dtFechaVence.EditValue : null;
            string msj = string.Empty;

            e.Handled = !this.Presenter.LicenciaValida(this.Licenciamiento, ref msj);
            if (e.Handled)
            {
                Utilerias.Informacion("La licencia no es válida.");
                this.txtLicenciaVolumetrico.Focus();
                return;
            }
            else if (this.Licenciamiento.EsTemporal && this.Licenciamiento.FechaVence != null)
            {
                if (this.Licenciamiento.FechaVence.Value.Date <= DateTime.Now.Date)
                {
                    Utilerias.Informacion(string.Format("La licencia caducó el {0:dd/MM/yyyy}.", this.Licenciamiento.FechaVence.Value.Date));
                    this.txtLicenciaVolumetrico.Focus();
                    return;
                }
                else if (this.Licenciamiento.FechaVence.Value.Date >= DateTime.Now.Date.AddDays(5))
                {
                    if (!Utilerias.Confirmacion(string.Format("La licencia caduca el día {0:dd/MM/yyyy}.\r\n¿Desea continuar?", this.Licenciamiento.FechaVence.Value.Date)))
                    {
                        this.txtLicenciaVolumetrico.Focus();
                        return;
                    }
                }
            }

            this.Licenciamiento.FechaVenceControlVersiones = (DateTime?)this.dtFechaControlVersiones.EditValue;
            this.Licenciamiento.LicenciaControlVersiones = this.txtLicenciaControlVersiones.Text;

            e.Handled = !this.Presenter.LicenciaControlVersiones(this.Licenciamiento, ref msj);
            if (e.Handled)
            {
                Utilerias.Informacion("La licencia de control de versiones no es válida.");
                this.txtLicenciaControlVersiones.Focus();
                return;
            }

            this.Licenciamiento.FechaVenceInocua = (DateTime?)this.dtFechaVenceInocua.EditValue;
            this.Licenciamiento.EsTemporalInocuo = this.chkEsTemporalInocua.Checked;
            this.Licenciamiento.LicienciaInocua = this.txtLicenciaInocua.Text;

            e.Handled = !this.Presenter.LicenciaInocua(this.Licenciamiento, ref msj);
            if (e.Handled)
            {
                Utilerias.Informacion("La licencia inocua no es válida.");
                this.txtLicenciaInocua.Focus();
                return;
            }

            //e.Handled = !this.Presenter.ValidaSentinel(this.Licenciamiento.NoSentinel.ToString());
            if (e.Handled)
            {
                Utilerias.Informacion("No. Serie Sentinel no es válido o no esta conectado.");
                this.txtNoSentinel.Focus();
                return;
            }
            else
            {
                EmpresaConfiguracion emp = WorkItem.Objetos<EmpresaConfiguracion>.Exist() ?
                                                WorkItem.Objetos<EmpresaConfiguracion>.Get() :
                                                new EmpresaConfiguracion();
                emp.SetLicencia(this.Licenciamiento);
                emp.NumEstacion = noEstacion;

                WorkItem.Objetos<Licencia>.Add(this.Licenciamiento);
                WorkItem.Objetos<EmpresaConfiguracion>.Add(emp);
            }
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
