using System;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Interfaces;
using iGasWizardVolumetricos.Persistencia.Entidades;
using _c = iGasWizardVolumetricos.Generales.Constantes;

namespace iGasWizardVolumetricos.Pantallas.Dispensarios
{
    public partial class PDispensarios : DevExpress.XtraEditors.XtraUserControl, IUserPages
    {
        public PDispensarios()
        {
            InitializeComponent();

            txtPosiciones.Value = 2;

            listado();

            btnAplicar.Click += new EventHandler(btnAplicar_Click);
        }

        #region Funciones

        private void listado()
        {
            gcDispensarios.DataSource = new ListaDispensarioVol();

            gcDispensarios.UseEmbeddedNavigator = false;
            gvDispensarios.OptionsCustomization.AllowFilter = false;
            gvDispensarios.OptionsCustomization.AllowSort = false;
            gvDispensarios.OptionsMenu.EnableColumnMenu = false;
            gvDispensarios.OptionsMenu.EnableFooterMenu = false;
            gvDispensarios.OptionsMenu.EnableGroupPanelMenu = false;
            gvDispensarios.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gvDispensarios.OptionsBehavior.Editable = true;
            gvDispensarios.OptionsView.ShowGroupPanel = false;
            gvDispensarios.OptionsView.ShowFooter = false;
            gvDispensarios.OptionsView.ColumnAutoWidth = false;
            gvDispensarios.OptionsCustomization.AllowColumnMoving = false;
            gvDispensarios.OptionsCustomization.AllowColumnResizing = false;
            gvDispensarios.OptionsCustomization.AllowRowSizing = false;

            gvDispensarios.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvDispensarios.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvDispensarios.Columns[0].OptionsColumn.AllowEdit = false;
            gvDispensarios.Columns[0].Caption = "Isla";
            gvDispensarios.Columns[0].Width = 87;

            gvDispensarios.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvDispensarios.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvDispensarios.Columns[1].OptionsColumn.AllowEdit = false;
            gvDispensarios.Columns[1].Caption = "Dispensario";
            gvDispensarios.Columns[1].Width = 88;

            gvDispensarios.Columns[2].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvDispensarios.Columns[2].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvDispensarios.Columns[2].ColumnEdit = new RepositoryItemSpinEdit();
            (gvDispensarios.Columns[2].ColumnEdit as RepositoryItemSpinEdit).IsFloatValue = false;
            (gvDispensarios.Columns[2].ColumnEdit as RepositoryItemSpinEdit).MinValue = 1;
            (gvDispensarios.Columns[2].ColumnEdit as RepositoryItemSpinEdit).MaxValue = 2;
            (gvDispensarios.Columns[2].ColumnEdit as RepositoryItemSpinEdit).TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            gvDispensarios.Columns[2].Caption = "Posiciones de Carga";
            gvDispensarios.Columns[2].Width = 120;
        }

        private void mostrar()
        {
            ListaIslaVol islas = WorkItem.Objetos<ListaIslaVol>.Get();
            ListaDispensarioVol l = (ListaDispensarioVol)gcDispensarios.DataSource;
            l.Clear();

            int clave = 0;

            islas.ForEach(i =>
            {
                for (int x = 0; x < i.NumeroDispensarios; x++)
                {
                    l.Add(new DispensarioVol()
                    {
                        Isla = i.Clave,
                        Clave = ++clave,
                        NumeroPosiciones = 2
                    });
                }
            });

            gcDispensarios.DataSource = l;
            gcDispensarios.Refresh();
            gvDispensarios.RefreshData();
        }

        #endregion

        #region Eventos

        void btnAplicar_Click(object sender, EventArgs e)
        {
            ListaDispensarioVol l = (ListaDispensarioVol)gcDispensarios.DataSource;

            l.ForEach(i =>
                {
                    i.NumeroPosiciones = Convert.ToInt32(txtPosiciones.Value);
                });

            gcDispensarios.DataSource = l;
            gcDispensarios.Refresh();
            gvDispensarios.RefreshData();
        }

        #endregion

        #region IUserPages Members

        public void DoInit(DevExpress.XtraWizard.BaseWizardPage parent)
        {
            parent.Text = _c.DISPENSARIOS.TITULO;
            mostrar();
        }

        public void NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            ListaDispensarioVol l = (ListaDispensarioVol)gcDispensarios.DataSource;
            WorkItem.Objetos<ListaDispensarioVol>.Add(l);
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
