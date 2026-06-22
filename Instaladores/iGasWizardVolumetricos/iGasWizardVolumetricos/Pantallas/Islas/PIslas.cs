using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraWizard;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Interfaces;
using iGasWizardVolumetricos.Persistencia.Entidades;
using _c = iGasWizardVolumetricos.Generales.Constantes;
using _u = iGasWizardVolumetricos.Generales.Utilerias;

namespace iGasWizardVolumetricos.Pantallas.Islas
{
    public partial class PIslas : DevExpress.XtraEditors.XtraUserControl, IUserPages
    {
        public PIslas()
        {
            InitializeComponent();

            listado();

            btnGenerar.Click += new EventHandler(btnGenerar_Click);
        }

        #region Funciones

        private void listado()
        {
            gcIslas.DataSource = new ListaIslaVol();

            gcIslas.UseEmbeddedNavigator = false;
            gvIslas.OptionsCustomization.AllowFilter = false;
            gvIslas.OptionsCustomization.AllowSort = false;
            gvIslas.OptionsMenu.EnableColumnMenu = false;
            gvIslas.OptionsMenu.EnableFooterMenu = false;
            gvIslas.OptionsMenu.EnableGroupPanelMenu = false;
            gvIslas.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gvIslas.OptionsBehavior.Editable = true;
            gvIslas.OptionsView.ShowGroupPanel = false;
            gvIslas.OptionsView.ShowFooter = false;
            gvIslas.OptionsView.ColumnAutoWidth = false;
            gvIslas.OptionsCustomization.AllowColumnMoving = false;
            gvIslas.OptionsCustomization.AllowColumnResizing = false;
            gvIslas.OptionsCustomization.AllowRowSizing = false;

            gvIslas.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvIslas.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvIslas.Columns[0].OptionsColumn.AllowEdit = false;
            gvIslas.Columns[0].Caption = "Isla";
            gvIslas.Columns[0].Width = 87;

            gvIslas.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvIslas.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvIslas.Columns[1].ColumnEdit = new RepositoryItemSpinEdit();
            (gvIslas.Columns[1].ColumnEdit as RepositoryItemSpinEdit).IsFloatValue = false;
            (gvIslas.Columns[1].ColumnEdit as RepositoryItemSpinEdit).MinValue = 1;
            (gvIslas.Columns[1].ColumnEdit as RepositoryItemSpinEdit).MaxValue = 5;
            (gvIslas.Columns[1].ColumnEdit as RepositoryItemSpinEdit).TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            gvIslas.Columns[1].Caption = "Dispensarios";
            gvIslas.Columns[1].Width = 88;
        }

        private void mostrar()
        {
            if (WorkItem.Objetos<ListaIslaVol>.Exist())
            {
                gcIslas.DataSource = WorkItem.Objetos<ListaIslaVol>.Get();
                gcIslas.Refresh();
                gvIslas.RefreshData();
            }
        }

        private bool datosSonValidos()
        {
            ListaIslaVol l = (ListaIslaVol)gcIslas.DataSource;
            if (l.Count.Equals(0))
            {
                _u.Informacion(_c.MENSAJES.NO_SE_HAN_GENERADO_ISLAS);
                btnGenerar.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region Eventos

        void btnGenerar_Click(object sender, EventArgs e)
        {
            ListaIslaVol l = (ListaIslaVol)gcIslas.DataSource;
            l.Clear();

            for (int i = 0; i < txtIslas.Value; i++)
            {
                l.Add(new IslaVol()
                {
                    Clave = i + 1,
                    NumeroDispensarios = 1
                });
            }

            gcIslas.DataSource = l;
            gcIslas.Refresh();
            gvIslas.RefreshData();
        }

        #endregion

        #region IUserPages Members

        public void DoInit(DevExpress.XtraWizard.BaseWizardPage parent)
        {
            parent.Text = _c.ISLAS.TITULO;
            mostrar();
        }

        public void NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            if (datosSonValidos())
            {
                ListaIslaVol l = (ListaIslaVol)gcIslas.DataSource;
                WorkItem.Objetos<ListaIslaVol>.Add(l);
            }
            else
            {
                e.Handled = true;
            }
        }

        public void PrevClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            MarcaDispensarioVol t = WorkItem.Objetos<MarcaDispensarioVol>.Get();

            string pag = string.Empty;
            switch (t.Marca)
            {
                case 1:
                    pag = "PWayne";
                    break;
                case 4:
                    pag = "PGilbarco";
                    break;
                default:
                    pag = "PMarcaDispensario";
                    break;
            }

            foreach (BaseWizardPage item in e.Page.Owner.Pages)
            {
                var c = item.Controls.OfType<XtraUserControl>();
                if (c.Count() > 0 && c.First().Name.Equals(pag))
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
