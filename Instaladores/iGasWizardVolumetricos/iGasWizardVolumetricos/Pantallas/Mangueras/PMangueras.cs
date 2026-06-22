using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Interfaces;
using iGasWizardVolumetricos.Persistencia.Entidades;
using iGasWizardVolumetricos.Persistencia.Logica;
using _c = iGasWizardVolumetricos.Generales.Constantes;
using _u = iGasWizardVolumetricos.Generales.Utilerias;

namespace iGasWizardVolumetricos.Pantallas.Mangueras
{
    public partial class PMangueras : DevExpress.XtraEditors.XtraUserControl, IUserPages
    {
        private MangueraVolPersistencia p = null;

        public PMangueras()
        {
            InitializeComponent();

            p = new MangueraVolPersistencia();

            listado();

            btnReplicar.Click += new EventHandler(btnReplicar_Click);
        }

        private void listado()
        {
            RepositoryItemComboBox combo = new RepositoryItemComboBox();
            combo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            combo.NullText = "Seleccione...";

            RepositoryItemComboBox comboCampoLectura = new RepositoryItemComboBox();
            comboCampoLectura.Items.AddRange(new string[] { "TOTAL01", "TOTAL02", "TOTAL03", "TOTAL04" });
            comboCampoLectura.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            comboCampoLectura.NullText = "Seleccione...";

            RepositoryItemSpinEdit numerico = new RepositoryItemSpinEdit();
            numerico.IsFloatValue = false;
            numerico.MinValue = 1;
            numerico.MaxValue = 4;
            numerico.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            numerico.NullText = "Seleccione...";

            gcMangueras.DataSource = new ListaMangueraVol();

            gcMangueras.UseEmbeddedNavigator = false;
            gvMangueras.OptionsCustomization.AllowFilter = false;
            gvMangueras.OptionsCustomization.AllowSort = false;
            gvMangueras.OptionsMenu.EnableColumnMenu = false;
            gvMangueras.OptionsMenu.EnableFooterMenu = false;
            gvMangueras.OptionsMenu.EnableGroupPanelMenu = false;
            gvMangueras.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gvMangueras.OptionsBehavior.Editable = true;
            gvMangueras.OptionsView.ShowGroupPanel = false;
            gvMangueras.OptionsView.ShowFooter = false;
            gvMangueras.OptionsView.ColumnAutoWidth = false;
            gvMangueras.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            gvMangueras.OptionsSelection.MultiSelect = true;
            gvMangueras.OptionsCustomization.AllowColumnMoving = false;
            gvMangueras.OptionsCustomization.AllowColumnResizing = false;
            gvMangueras.OptionsCustomization.AllowRowSizing = false;

            gvMangueras.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvMangueras.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvMangueras.Columns[0].OptionsColumn.AllowEdit = false;
            gvMangueras.Columns[0].Caption = "Isla";
            gvMangueras.Columns[0].Width = 45;

            gvMangueras.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvMangueras.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvMangueras.Columns[1].OptionsColumn.AllowEdit = false;
            gvMangueras.Columns[1].Caption = "Dispensario";
            gvMangueras.Columns[1].Width = 70;

            gvMangueras.Columns[2].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvMangueras.Columns[2].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvMangueras.Columns[2].OptionsColumn.AllowEdit = false;
            gvMangueras.Columns[2].Caption = "Posición de Carga";
            gvMangueras.Columns[2].Width = 100;

            gvMangueras.Columns[3].Visible = false;

            gvMangueras.Columns[4].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvMangueras.Columns[4].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvMangueras.Columns[4].OptionsColumn.AllowEdit = false;
            gvMangueras.Columns[4].Caption = "Manguera";
            gvMangueras.Columns[4].Width = 70;

            gvMangueras.Columns[5].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvMangueras.Columns[5].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvMangueras.Columns[5].OptionsColumn.AllowEdit = true;
            gvMangueras.Columns[5].ColumnEdit = numerico;
            gvMangueras.Columns[5].Caption = "Posición Física";
            gvMangueras.Columns[5].Width = 80;

            gvMangueras.Columns[6].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            gvMangueras.Columns[6].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            gvMangueras.Columns[6].ColumnEdit = combo;
            gvMangueras.Columns[6].Caption = "Combustible";
            gvMangueras.Columns[6].Width = 119;

            gvMangueras.Columns[7].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            gvMangueras.Columns[7].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            gvMangueras.Columns[7].ColumnEdit = comboCampoLectura;
            gvMangueras.Columns[7].Caption = "Totalizador";
            gvMangueras.Columns[7].Width = 84;
        }

        private void configurarCombustibles()
        {
            RepositoryItemComboBox combo = (RepositoryItemComboBox)gvMangueras.Columns[6].ColumnEdit;
            if (combo.Items.Count.Equals(0))
            {
                ListaCombustibleVol combustible = WorkItem.Objetos<ListaCombustibleVol>.Get();
                combo.Items.AddRange(combustible.Select(e => e.Nombre).ToArray());
            }
        }

        private void mostrar()
        {
            ListaPosicionCargaVol posiciones = WorkItem.Objetos<ListaPosicionCargaVol>.Get();
            ListaMangueraVol l = (ListaMangueraVol)gcMangueras.DataSource;
            l.Clear();

            int clave = 0;

            posiciones.ForEach(e =>
            {
                for (int x = 0; x < e.NumeroMangueras; x++)
                {
                    l.Add(new MangueraVol()
                    {
                        Isla = e.Isla,
                        Dispensario = e.Dispensario,
                        Posicion = e.Clave,
                        Indice = x + 1,
                        Clave = ++clave,
                        ConPosicion = null,
                        TipoCombustible = null,
                        CampoLectura = null
                    });
                }
            });

            gcMangueras.DataSource = l;
            gcMangueras.Refresh();
            gvMangueras.RefreshData();
        }

        private bool datosSonValidos(List<MangueraVol> p1)
        {
            var conposicion = p1.GroupBy(e => e.ConPosicion);

            foreach (var item in conposicion)
            {
                if (item.Count() > 1)
                {
                    _u.Informacion(_c.MENSAJES.CON_POSICION_REPETIDO);
                    return false;
                }
            }

            var campolectura = p1.GroupBy(e => e.CampoLectura);

            foreach (var item in campolectura)
            {
                if (item.Count() > 1)
                {
                    _u.Informacion(_c.MENSAJES.CAMPO_LECTURA_REPETIDO);
                    return false;
                }
            }

            return true;
        }

        private bool datosSonValidos()
        {
            ListaMangueraVol l = (ListaMangueraVol)gcMangueras.DataSource;

            foreach (MangueraVol item in l)
            {
                if (item.ConPosicion == null || item.TipoCombustible == null || item.CampoLectura == null)
                {
                    _u.Informacion(_c.MENSAJES.NO_SE_HAN_CONFIGURADO_TODAS_LAS_MANGUERAS);
                    return false;
                }
            }

            var posiciones = l.GroupBy(e => e.Posicion);

            foreach (var item in posiciones)
            {
                if (!datosSonValidos(item.ToList()))
                    return false;
            }

            return true;
        }

        void btnReplicar_Click(object sender, EventArgs e)
        {
            ListaMangueraVol l = (ListaMangueraVol)gcMangueras.DataSource;

            ListaMangueraVol s = new ListaMangueraVol();

            foreach (int i in gvMangueras.GetSelectedRows())
            {
                s.Add((MangueraVol)gvMangueras.GetRow(i));
            }

            l.ForEach(i =>
            {
                if (s.Exists(p => p.Indice.Equals(i.Indice)))
                {
                    i.ConPosicion = s.Find(p => p.Indice.Equals(i.Indice)).ConPosicion;
                    i.TipoCombustible = s.Find(p => p.Indice.Equals(i.Indice)).TipoCombustible;
                    i.CampoLectura = s.Find(p => p.Indice.Equals(i.Indice)).CampoLectura;
                }
            });

            gvMangueras.RefreshData();
        }

        #region IUserPages Members

        public void DoInit(DevExpress.XtraWizard.BaseWizardPage parent)
        {
            parent.Text = _c.MANGUERAS.TITULO;

            configurarCombustibles();
            mostrar();
        }

        public void NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            if (datosSonValidos())
            {
                try
                {
                    ListaMangueraVol l = (ListaMangueraVol)gcMangueras.DataSource;
                    string ex = p.Guardar(l);
                    if (string.IsNullOrEmpty(ex))
                    {
                        WorkItem.Objetos<ListaMangueraVol>.Add(l);
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
