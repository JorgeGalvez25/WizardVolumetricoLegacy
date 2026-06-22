using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraWizard;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Interfaces;
using iGasWizardVolumetricos.Persistencia.Entidades;

namespace iGasWizardVolumetricos.Pantallas.DatosDeEmpresa
{
    public partial class PDatosEmpresa : XtraUserControl, IUserPages
    {
        public Empresa Empresa { get; set; }
        public Presenter Presenter { get; set; }

        private const string NOESTACION_VALIDATION = @"E[0-9]{5}";
        private const string GET_NOESTACION_NUMBERS_ONLY = @"[0-9]{1,5}";
        private const string RFC_VALIDATION = @"((\s)?(&|[A-Z]){3,4})([0-9]{6})((&|[A-Z0-9]){3})";

        public PDatosEmpresa()
        {
            this.InitializeComponent();

            this.Presenter = new Presenter();
            this.Empresa = new Empresa();

            this.Configurar();
        }

        private void Configurar()
        {
            this.txtDireccion.Properties.MaxLength =
                this.txtPoblacion.Properties.MaxLength =
                this.txtNombreComercial.Properties.MaxLength = 50;

            this.txtClavePEMEX.Properties.MaxLength =
                this.txtUsuarioPEMEX.Properties.MaxLength = 20;
            this.txtNombreEstación.Properties.MaxLength = 40;

            this.txtRFC.Properties.MaxLength = 13;
            this.txtRFC.Properties.CharacterCasing =
            this.txtNombreComercial.Properties.CharacterCasing = CharacterCasing.Upper;

            this.txtRFC.Validating += this.txtRFC_Validating;

            this.txtRFC.KeyPress += this.txtRFC_KeyPress;

            this.txtNoEstacion.Properties.ReadOnly = true;
        }

        private bool ValidaControles(ref string msj)
        {
            string nomComercial = this.txtNombreComercial.Text.Trim();
            string rfc = this.txtRFC.Text.Trim();
            string direccion = this.txtDireccion.Text.Trim();
            string poblacion = this.txtPoblacion.Text.Trim();
            string noEstacion = this.txtNoEstacion.Text.Trim();
            string nombreEstacion = this.txtNombreEstación.Text.Trim();
            string usuarioPEMEX = this.txtUsuarioPEMEX.Text.Trim();
            string clavePEMEX = this.txtClavePEMEX.Text.Trim();

            StringComparer comparador = StringComparer.CurrentCulture;

            if (string.IsNullOrEmpty(usuarioPEMEX))
            {
                msj = "Debe especificar su Usuario PEMEX.";
                this.txtUsuarioPEMEX.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(clavePEMEX))
            {
                msj = "Debe especificar su Clave PEMEX.";
                this.txtClavePEMEX.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(nomComercial))
            {
                msj = "Debe especificar un Nombre Comercial.";
                this.txtNombreComercial.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(rfc))
            {
                msj = "Debe especificar un RFC.";
                this.txtRFC.Focus();
                return false;
            }
            else if (!Regex.IsMatch(rfc, RFC_VALIDATION))
            {
                msj = "RFC no tiene un formato correcto.";
                this.txtRFC.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(direccion))
            {
                msj = "Debe especificar una Dirección.";
                this.txtDireccion.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(poblacion))
            {
                msj = "Debe especificar una Población.";
                this.txtPoblacion.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(noEstacion) && !Regex.IsMatch(noEstacion, "E[0-9]{5}"))
            {
                msj = "Debe especificar un Número de Estación.";
                this.txtNoEstacion.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(nombreEstacion))
            {
                msj = "Debe especificar un Nombre de Estación.";
                this.txtNombreEstación.Focus();
                return false;
            }

            return true;
        }

        private void txtRFC_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = (int)e.KeyChar;
            if (!((key >= 97 && key <= 122) || (key >= 65 && key <= 90) || // Texto Mayusculas y minusculas
                (key >= 48 && key <= 57) || // Numeros del 0 al 9
                (key == 38) || // El simbolo '&'
                (key == 8))) // El simbolo 'Retroceso (Borrar)'
            {
                e.Handled = true;
            }
        }

        private void txtRFC_Validating(object sender, CancelEventArgs e)
        {
            TextEdit rfc = (TextEdit)sender;

            if (!string.IsNullOrEmpty(rfc.Text))
            {
                if (!Regex.IsMatch(rfc.Text, RFC_VALIDATION))
                {
                    rfc.Focus();
                    rfc.ErrorText = "Debe proporcionar un RFC válido de no mas de 13 carácteres sin espacios ni guiones";
                    Utilerias.Informacion("RFC no tiene un formato correcto.");
                    e.Cancel = true;
                }
            }
        }

        #region IUserPages Members

        public void DoInit(BaseWizardPage parent)
        {
            parent.Text = "Datos de la Empresa";
            this.BeginInvoke(new MethodInvoker(() =>
                {
                    EmpresaConfiguracion cfg = null;
                    if (WorkItem.Objetos<EmpresaConfiguracion>.Exist())
                    {
                        cfg = WorkItem.Objetos<EmpresaConfiguracion>.Get();
                    }

                    this.Empresa = (cfg == null) ?
                                        this.Presenter.ObtenerEmpresa() :
                                        cfg.GetEmpresa();

                    this.lblRazonSocial.Text = cfg == null ? string.Empty : cfg.GetLicencia().RazonSocial;
                    this.txtNombreComercial.Text = this.Empresa.NombreComercial;
                    this.txtRFC.Text = this.Empresa.RFC;
                    this.txtDireccion.Text = this.Empresa.Direccion;
                    this.txtPoblacion.Text = this.Empresa.Poblacion;
                    this.txtNoEstacion.Text = this.Empresa.NumEstacion;
                    this.txtNombreEstación.Text = this.Empresa.NombreEstacion;
                }));
        }

        public void NextClick(object sender, WizardCommandButtonClickEventArgs e)
        {
            string msj = string.Empty;
            e.Handled = !this.ValidaControles(ref msj);

            if (e.Handled)
            {
                Utilerias.Informacion(msj);
                return;
            }

            this.Empresa.ClavePEMEX = this.txtClavePEMEX.Text;
            this.Empresa.Direccion = this.txtDireccion.Text;
            this.Empresa.NombreComercial = this.txtNombreComercial.Text.ToUpper();
            this.Empresa.NombreEstacion = this.txtNombreEstación.Text;
            this.Empresa.NumEstacion = this.txtNoEstacion.Text;
            this.Empresa.Poblacion = this.txtPoblacion.Text;
            this.Empresa.RFC = this.txtRFC.Text.ToUpper();
            this.Empresa.UsuarioPEMEX = this.txtUsuarioPEMEX.Text;

            EmpresaConfiguracion emp = Presenter.ObtenerConfiguracion(this.Empresa);
            WorkItem.Objetos<EmpresaConfiguracion>.Add(emp);

            e.Handled = !this.Presenter.Guardar(this.Empresa, ref msj);
            if (e.Handled)
            {
                Utilerias.Error(msj);
            }
        }

        public void PrevClick(object sender, WizardCommandButtonClickEventArgs e) { }

        public void CancelClick(FormClosingEventArgs e) { }

        #endregion
    }
}
