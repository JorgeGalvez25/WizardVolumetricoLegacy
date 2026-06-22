using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Interfaces;
using iGasWizardVolumetricos.Persistencia.Entidades;
using iGasWizardVolumetricos.Persistencia.Logica;
using _c = iGasWizardVolumetricos.Generales.Constantes;
using _u = iGasWizardVolumetricos.Generales.Utilerias;

namespace iGasWizardVolumetricos.Pantallas.Tanques
{
    public partial class PTanques : DevExpress.XtraEditors.XtraUserControl, IUserPages
    {
        private string combustible = null;
        private TanqueVolPersistencia p = null;

        public PTanques()
        {
            InitializeComponent();

            p = new TanqueVolPersistencia();

            btnGenerar.Click += new EventHandler(btnGenerar_Click);

            listado();
        }

        #region Funciones

        private void listado()
        {
            gcTanques.DataSource = new ListaTanqueVol();

            RepositoryItemComboBox combo = new RepositoryItemComboBox();
            combo.Items.Clear();
            combo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            combo.NullText = "Seleccione...";

            RepositoryItemSpinEdit numerico = new RepositoryItemSpinEdit();
            numerico.MinValue = 1;
            numerico.MaxValue = 999999999;
            numerico.Increment = 1;
            numerico.IsFloatValue = false;
            numerico.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            numerico.Mask.EditMask = "###,###,###;###,###,###";
            numerico.Mask.ShowPlaceHolders = false;
            numerico.Mask.UseMaskAsDisplayFormat = true;
            numerico.Buttons.Clear();

            RepositoryItemSpinEdit flotante = new RepositoryItemSpinEdit();
            flotante.MinValue = 1.0M;
            flotante.MaxValue = 999999999.99M;
            flotante.Increment = 1;
            flotante.IsFloatValue = true;
            flotante.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            flotante.Mask.EditMask = "###,###,###.00;###,###,###.00";
            flotante.Mask.ShowPlaceHolders = false;
            flotante.Mask.UseMaskAsDisplayFormat = true;
            flotante.Buttons.Clear();

            gcTanques.UseEmbeddedNavigator = false;
            gvTanques.OptionsCustomization.AllowFilter = false;
            gvTanques.OptionsCustomization.AllowSort = false;
            gvTanques.OptionsMenu.EnableColumnMenu = false;
            gvTanques.OptionsMenu.EnableFooterMenu = false;
            gvTanques.OptionsMenu.EnableGroupPanelMenu = false;
            gvTanques.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gvTanques.OptionsBehavior.Editable = true;
            gvTanques.OptionsView.ShowGroupPanel = false;
            gvTanques.OptionsView.ShowFooter = false;
            gvTanques.OptionsView.ColumnAutoWidth = false;
            gvTanques.OptionsCustomization.AllowColumnMoving = false;
            gvTanques.OptionsCustomization.AllowColumnResizing = false;
            gvTanques.OptionsCustomization.AllowRowSizing = false;

            gvTanques.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvTanques.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvTanques.Columns[0].OptionsColumn.AllowEdit = false;
            gvTanques.Columns[0].Caption = "Clave";
            gvTanques.Columns[0].Width = 88;

            gvTanques.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gvTanques.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gvTanques.Columns[1].ColumnEdit = combo;
            gvTanques.Columns[1].Caption = "Combustible";
            gvTanques.Columns[1].Width = 130;

            gvTanques.Columns[2].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvTanques.Columns[2].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvTanques.Columns[2].ColumnEdit = numerico;
            gvTanques.Columns[2].Caption = "Vol. Fondo (Lts.)";
            gvTanques.Columns[2].Width = 125;

            gvTanques.Columns[3].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvTanques.Columns[3].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvTanques.Columns[3].ColumnEdit = numerico;
            gvTanques.Columns[3].Caption = "Capacidad (Lts.)";
            gvTanques.Columns[3].Width = 125;

            gvTanques.Columns[4].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvTanques.Columns[4].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvTanques.Columns[4].ColumnEdit = flotante;
            gvTanques.Columns[4].Caption = "Altura (Cmts.)";
            gvTanques.Columns[4].Width = 100;
        }

        private void configurarListaCombustibles()
        {
            RepositoryItemComboBox combo = (RepositoryItemComboBox)gvTanques.Columns[1].ColumnEdit;
            if (combo.Items.Count.Equals(0))
            {
                ListaCombustibleVol l = WorkItem.Objetos<ListaCombustibleVol>.Get();
                combo.Items.AddRange(l.Select(e => e.Nombre).ToArray());
                combustible = l.First().Nombre;
            }
        }

        private void mostrar()
        {
            if (WorkItem.Objetos<ListaTanqueVol>.Exist())
            {
                gcTanques.DataSource = WorkItem.Objetos<ListaTanqueVol>.Get();
                gcTanques.Refresh();
                gvTanques.RefreshData();
            }
        }

        private bool datosSonValidos()
        {
            ListaTanqueVol l = (ListaTanqueVol)gcTanques.DataSource;
            if (l.Count.Equals(0))
            {
                _u.Informacion(_c.MENSAJES.NO_SE_HAN_GENERADO_TANQUES);
                btnGenerar.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region Eventos

        void btnGenerar_Click(object sender, EventArgs e)
        {
            ListaTanqueVol l = (ListaTanqueVol)gcTanques.DataSource;
            l.Clear();

            for (int i = 0; i < txtTanques.Value; i++)
            {
                l.Add(new TanqueVol()
                {
                    Clave = i + 1,
                    TipoCombustible = combustible,
                    VolumenFondo = 1,
                    Capacidad = 1,
                    Altura = 1
                });
            }

            gcTanques.DataSource = l;
            gcTanques.Refresh();
            gvTanques.RefreshData();
        }

        #endregion

        #region IUserPages Members

        public void DoInit(DevExpress.XtraWizard.BaseWizardPage parent)
        {
            parent.Text = _c.TANQUE.TITULO;

            configurarListaCombustibles();
            mostrar();
        }

        public void NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            if (datosSonValidos())
            {
                ListaTanqueVol l = (ListaTanqueVol)gcTanques.DataSource;
                try
                {
                    string ex = p.Guardar(l);
                    if (string.IsNullOrEmpty(ex))
                    {
                        WorkItem.Objetos<ListaTanqueVol>.Add(l);
                    }
                    else
                    {
                        _u.Error(ex);
                        e.Handled = true;
                    }
                }
                catch (Exception ex)
                {
                    _u.Error(ex);
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        public void PrevClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {

        }

        public void CancelClick(FormClosingEventArgs e)
        {

        }

        #endregion
    }
}
