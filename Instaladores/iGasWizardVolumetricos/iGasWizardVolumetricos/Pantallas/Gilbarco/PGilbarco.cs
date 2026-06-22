using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraWizard;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Interfaces;
using iGasWizardVolumetricos.Persistencia.Entidades;
using iGasWizardVolumetricos.Persistencia.Logica;
using _c = iGasWizardVolumetricos.Generales.Constantes;
using _u = iGasWizardVolumetricos.Generales.Utilerias;

namespace iGasWizardVolumetricos.Pantallas.Gilbarco
{
    public partial class PGilbarco : DevExpress.XtraEditors.XtraUserControl, IUserPages
    {
        VariablesVolPersistencia p = null;

        public PGilbarco()
        {
            InitializeComponent();

            p = new VariablesVolPersistencia();
        }

        #region Funciones

        private void mostrar()
        {
            if (WorkItem.Objetos<VariablesVol>.Exist())
            {
                VariablesVol t = WorkItem.Objetos<VariablesVol>.Get();
                txtDecimalesPreset.Text = preset(t.DecimalesPresetPAM);
                txtDecimalesPresetLitros.Text = preset(t.DecimalesPresetPAMLitros);
                txtConDigitoAjuste.Text = ajuste(t.ConDigitoAjuste);
                txtDigitoAjusteVol.Text = ajuste(t.DigitoAjusteVol);
                txtDigitoGilbarco.Text = digito(t.DigitosGilbarco);
            }
        }

        private string digito(int valor)
        {
            switch (valor)
            {
                case 5: return "000.00";
                case 6: return "0000.00";
                default: return null;
            }
        }

        private string autorizacion(int valor)
        {
            switch (valor)
            {
                case 0: return "Sin Límite";
                case 1: return "Por Importe";
                default: return null;
            }
        }

        private string ajuste(int decimales)
        {
            switch (decimales)
            {
                case 0: return "00000.000";
                case 1: return "000000.00";
                case 2: return "0000000.0";
                default: return null;
            }
        }

        private string nivelPrecio(int valor)
        {
            if (valor == 1)
            {
                return "Contado";
            }
            else
            {
                return "Crédito";
            }
        }

        private string preset(int decimales)
        {
            switch (decimales)
            {
                case 0: return "00000000";
                case 1: return "0000000.0";
                case 2: return "000000.00";
                case 3: return "00000.000";
                default: return null;
            }
        }

        private VariablesVol obtener()
        {
            VariablesVol t = new VariablesVol();
            t.SetUpPam1000 = null;
            t.AjustePam = false;
            t.ModoAutorizaPam = 0;
            t.MaximoPresetPAM = 9999;
            t.DecimalesPresetPAM = preset(txtDecimalesPreset.Text);
            t.DecimalesPresetPAMLitros = preset(txtDecimalesPresetLitros.Text);
            t.ConDigitoAjuste = ajuste(txtConDigitoAjuste.Text);
            t.DigitoAjusteVol = ajuste(txtDigitoAjusteVol.Text);
            t.DigitosGilbarco = digito();
            return t;
        }

        private int digito()
        {
            if (txtDigitoGilbarco.Text.Equals("000.00"))
            {
                return 5;
            }
            else
            {
                return 6;
            }
        }

        private int ajuste(string mascara)
        {
            switch (mascara)
            {
                case "00000.000": return 0;
                case "000000.00": return 1;
                case "0000000.0": return 2;
                default: return 1;
            }
        }

        private int preset(string mascara)
        {
            switch (mascara)
            {
                case "00000000": return 0;
                case "0000000.0": return 1;
                case "000000.00": return 2;
                case "00000.000": return 3;
                default: return 2;
            }
        }

        #endregion

        #region IUserPages Members

        public void DoInit(DevExpress.XtraWizard.BaseWizardPage parent)
        {
            parent.Text = _c.VARIABLES.TITULOGILBARCO;
            mostrar();
        }

        public void NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            try
            {
                VariablesVol t = obtener();

                string ex = p.Guardar(t);
                if (string.IsNullOrEmpty(ex))
                {
                    WorkItem.Objetos<VariablesVol>.Add(t);

                    foreach (BaseWizardPage item in e.Page.Owner.Pages)
                    {
                        var c = item.Controls.OfType<XtraUserControl>();
                        if (c.Count() > 0 && c.First().Name.Equals("PIslas"))
                        {
                            e.Page.Owner.SelectedPage = item;
                            break;
                        }
                    }
                }
                else
                {
                    _u.Error(ex);
                }
            }
            catch (Exception ex)
            {
                _u.Error(ex);
            }

            e.Handled = true;
        }

        public void PrevClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            foreach (BaseWizardPage item in e.Page.Owner.Pages)
            {
                var c = item.Controls.OfType<XtraUserControl>();
                if (c.Count() > 0 && c.First().Name.Equals("PMarcaDispensario"))
                {
                    e.Page.Owner.SelectedPage = item;
                    break;
                }
            }

            e.Handled = true;
        }

        public void CancelClick(FormClosingEventArgs e)
        {
        }

        #endregion
    }
}
