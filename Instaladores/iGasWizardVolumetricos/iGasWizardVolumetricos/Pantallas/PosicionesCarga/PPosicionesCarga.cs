using System;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Interfaces;
using iGasWizardVolumetricos.Persistencia.Entidades;
using _c = iGasWizardVolumetricos.Generales.Constantes;

namespace iGasWizardVolumetricos.Pantallas.PosicionesCarga
{
    public partial class PPosicionesCarga : DevExpress.XtraEditors.XtraUserControl, IUserPages
    {
        public PPosicionesCarga()
        {
            InitializeComponent();

            txtMangueras.Value = 2;

            listado();

            btnAplicar.Click += new EventHandler(btnAplicar_Click);
        }

        #region Funciones

        private void listado()
        {
            gcPosicion.DataSource = new ListaPosicionCargaVol();

            gcPosicion.UseEmbeddedNavigator = false;
            gvPosicion.OptionsCustomization.AllowFilter = false;
            gvPosicion.OptionsCustomization.AllowSort = false;
            gvPosicion.OptionsMenu.EnableColumnMenu = false;
            gvPosicion.OptionsMenu.EnableFooterMenu = false;
            gvPosicion.OptionsMenu.EnableGroupPanelMenu = false;
            gvPosicion.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gvPosicion.OptionsBehavior.Editable = true;
            gvPosicion.OptionsView.ShowGroupPanel = false;
            gvPosicion.OptionsView.ShowFooter = false;
            gvPosicion.OptionsView.ColumnAutoWidth = false;
            gvPosicion.OptionsCustomization.AllowColumnMoving = false;
            gvPosicion.OptionsCustomization.AllowColumnResizing = false;
            gvPosicion.OptionsCustomization.AllowRowSizing = false;

            gvPosicion.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvPosicion.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvPosicion.Columns[0].OptionsColumn.AllowEdit = false;
            gvPosicion.Columns[0].Caption = "Isla";
            gvPosicion.Columns[0].Width = 87;

            gvPosicion.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvPosicion.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvPosicion.Columns[1].OptionsColumn.AllowEdit = false;
            gvPosicion.Columns[1].Caption = "Dispensario";
            gvPosicion.Columns[1].Width = 88;

            gvPosicion.Columns[2].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvPosicion.Columns[2].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvPosicion.Columns[2].OptionsColumn.AllowEdit = false;
            gvPosicion.Columns[2].Caption = "Posición de Carga";
            gvPosicion.Columns[2].Width = 120;

            gvPosicion.Columns[3].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvPosicion.Columns[3].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvPosicion.Columns[3].ColumnEdit = new RepositoryItemSpinEdit();
            (gvPosicion.Columns[3].ColumnEdit as RepositoryItemSpinEdit).IsFloatValue = false;
            (gvPosicion.Columns[3].ColumnEdit as RepositoryItemSpinEdit).MinValue = 1;
            (gvPosicion.Columns[3].ColumnEdit as RepositoryItemSpinEdit).MaxValue = 3;
            (gvPosicion.Columns[3].ColumnEdit as RepositoryItemSpinEdit).TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            gvPosicion.Columns[3].Caption = "Número de Mangueras";
            gvPosicion.Columns[3].Width = 130;
        }

        private void mostrar()
        {
            ListaDispensarioVol dispensarios = WorkItem.Objetos<ListaDispensarioVol>.Get();
            ListaPosicionCargaVol l = (ListaPosicionCargaVol)gcPosicion.DataSource;
            l.Clear();

            int clave = 0;

            dispensarios.ForEach(d =>
            {
                for (int x = 0; x < d.NumeroPosiciones; x++)
                {
                    l.Add(new PosicionCargaVol()
                    {
                        Isla = d.Isla,
                        Dispensario = d.Clave,
                        Clave = ++clave,
                        NumeroMangueras = 2
                    });
                }
            });

            gcPosicion.DataSource = l;
            gcPosicion.Refresh();
            gvPosicion.RefreshData();
        }

        #endregion

        #region Eventos

        void btnAplicar_Click(object sender, EventArgs e)
        {
            ListaPosicionCargaVol l = (ListaPosicionCargaVol)gcPosicion.DataSource;

            l.ForEach(i =>
                {
                    i.NumeroMangueras = Convert.ToInt32(txtMangueras.Value);
                });

            gcPosicion.DataSource = l;
            gcPosicion.Refresh();
            gvPosicion.RefreshData();
        }

        #endregion

        #region IUserPages Members

        public void DoInit(DevExpress.XtraWizard.BaseWizardPage parent)
        {
            parent.Text = _c.POSICIONESCARGA.TITULO;
            mostrar();
        }

        public void NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            ListaPosicionCargaVol l = (ListaPosicionCargaVol)gcPosicion.DataSource;
            WorkItem.Objetos<ListaPosicionCargaVol>.Add(l);
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
