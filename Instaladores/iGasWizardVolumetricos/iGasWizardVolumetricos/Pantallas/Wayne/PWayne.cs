using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraWizard;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Interfaces;
using iGasWizardVolumetricos.Persistencia.Entidades;
using iGasWizardVolumetricos.Persistencia.Logica;
using _c = iGasWizardVolumetricos.Generales.Constantes;
using _u = iGasWizardVolumetricos.Generales.Utilerias;

namespace iGasWizardVolumetricos.Pantallas.Wayne
{
    public partial class PWayne : DevExpress.XtraEditors.XtraUserControl, IUserPages
    {
        VariablesVolPersistencia p = null;

        public PWayne()
        {
            InitializeComponent();

            p = new VariablesVolPersistencia();

            listado();
        }

        #region Funciones

        private void listado()
        {
            gcCombustibles.DataSource = new ListaCombustibleVol();

            gcCombustibles.UseEmbeddedNavigator = false;
            gvCombustibles.OptionsCustomization.AllowFilter = false;
            gvCombustibles.OptionsCustomization.AllowSort = false;
            gvCombustibles.OptionsMenu.EnableColumnMenu = false;
            gvCombustibles.OptionsMenu.EnableFooterMenu = false;
            gvCombustibles.OptionsMenu.EnableGroupPanelMenu = false;
            gvCombustibles.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gvCombustibles.OptionsBehavior.Editable = true;
            gvCombustibles.OptionsView.ShowGroupPanel = false;
            gvCombustibles.OptionsView.ShowFooter = false;
            gvCombustibles.OptionsView.ColumnAutoWidth = false;
            gvCombustibles.OptionsCustomization.AllowColumnMoving = false;
            gvCombustibles.OptionsCustomization.AllowColumnResizing = false;
            gvCombustibles.OptionsCustomization.AllowRowSizing = false;

            gvCombustibles.Columns[0].Visible = false;

            gvCombustibles.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gvCombustibles.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gvCombustibles.Columns[1].OptionsColumn.AllowEdit = false;
            gvCombustibles.Columns[1].Caption = "Combustible";
            gvCombustibles.Columns[1].Width = 100;

            gvCombustibles.Columns[2].Visible = false;

            gvCombustibles.Columns[3].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvCombustibles.Columns[3].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvCombustibles.Columns[3].ColumnEdit = new RepositoryItemSpinEdit();
            (gvCombustibles.Columns[3].ColumnEdit as RepositoryItemSpinEdit).IsFloatValue = false;
            (gvCombustibles.Columns[3].ColumnEdit as RepositoryItemSpinEdit).MinValue = 1;
            (gvCombustibles.Columns[3].ColumnEdit as RepositoryItemSpinEdit).MaxValue = 3;
            (gvCombustibles.Columns[3].ColumnEdit as RepositoryItemSpinEdit).TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            gvCombustibles.Columns[3].Caption = "Id Físico del Producto";
            gvCombustibles.Columns[3].Width = 120;
        }

        private void mostrar()
        {
            if (WorkItem.Objetos<VariablesVol>.Exist())
            {
                VariablesVol t = WorkItem.Objetos<VariablesVol>.Get();
                txtDecimalesPreset.Text = preset(t.DecimalesPresetWayne);
                txtDecimalesPresetLitros.Text = preset(t.DecimalesPresetWayneLitros);
                txtConDigitoAjuste.Text = ajuste(t.ConDigitoAjuste);
                gcCombustibles.DataSource = t.ConProductoPrecio;
            }
            else
            {
                gcCombustibles.DataSource = WorkItem.Objetos<ListaCombustibleVol>.Get();
            }

            gcCombustibles.Refresh();
            gvCombustibles.RefreshData();
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

        private string preset(int decimales)
        {
            switch (decimales)
            {
                case 1: return "0000000.0";
                case 2: return "000000.00";
                case 3: return "00000.000";
                case 4: return "0000.0000";
                default: return null;
            }
        }

        private VariablesVol obtener()
        {
            VariablesVol t = new VariablesVol();
            t.DecimalesPresetWayne = preset(txtDecimalesPreset.Text);
            t.DecimalesPresetWayneLitros = preset(txtDecimalesPresetLitros.Text);
            t.AjusteWayne = false;
            t.NivelPrecioWayne = 1;
            t.ConDigitoAjuste = ajuste();
            t.ConProductoPrecio = (ListaCombustibleVol)gcCombustibles.DataSource;
            return t;
        }

        private int ajuste()
        {
            switch (txtConDigitoAjuste.Text)
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
                case "0000000.0": return 1;
                case "000000.00": return 2;
                case "00000.000": return 3;
                case "0000.0000": return 4;
                default: return 2;
            }
        }

        #endregion

        #region IUserPages Members

        public void DoInit(DevExpress.XtraWizard.BaseWizardPage parent)
        {
            parent.Text = _c.VARIABLES.TITULOWAYNE;

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
