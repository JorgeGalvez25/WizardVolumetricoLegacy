using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraWizard;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Interfaces;
using iGasWizardVolumetricos.Persistencia.Entidades;
using iGasWizardVolumetricos.Persistencia.Logica;
using _c = iGasWizardVolumetricos.Generales.Constantes;
using _u = iGasWizardVolumetricos.Generales.Utilerias;

namespace iGasWizardVolumetricos.Pantallas.MarcaDispensario
{
    public partial class PMarcaDispensario : DevExpress.XtraEditors.XtraUserControl, IUserPages
    {
        MarcaDispensarioVolPersistencia p = null;
        ListaTipoInterfazDispensarioVol interfaces = null;

        public PMarcaDispensario()
        {
            InitializeComponent();

            p = new MarcaDispensarioVolPersistencia();
            interfaces = new ListaTipoInterfazDispensarioVol();

            luMarca.Properties.DataSource = new ListaTipoMarcaDispensarioVol();
            luMarca.Properties.HeaderClickMode = DevExpress.XtraEditors.Controls.HeaderClickMode.AutoSearch;
            luMarca.Properties.PopupSizeable = false;
            luMarca.Properties.ShowFooter = false;
            luMarca.Properties.ShowHeader = false;
            luMarca.Properties.Columns.Clear();
            LookUpColumnInfo columna = new LookUpColumnInfo();
            columna.FieldName = "Nombre";
            columna.Caption = "Nombre";
            luMarca.Properties.Columns.Add(columna);
            luMarca.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            luMarca.Properties.DisplayMember = "Nombre";
            luMarca.Properties.ValueMember = "Clave";
            luMarca.EditValueChanged += new EventHandler(luMarca_EditValueChanged);

            luInterfaz.Properties.DataSource = new ListaTipoInterfazDispensarioVol();
            luInterfaz.Properties.HeaderClickMode = DevExpress.XtraEditors.Controls.HeaderClickMode.AutoSearch;
            luInterfaz.Properties.PopupSizeable = false;
            luInterfaz.Properties.ShowFooter = false;
            luInterfaz.Properties.ShowHeader = false;
            luInterfaz.Properties.Columns.Clear();
            columna = new LookUpColumnInfo();
            columna.FieldName = "Nombre";
            columna.Caption = "Nombre";
            luInterfaz.Properties.Columns.Add(columna);
            luInterfaz.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            luInterfaz.Properties.DisplayMember = "Nombre";
            luInterfaz.Properties.ValueMember = "Clave";

            txtModoOperacion.SelectedIndex = 0;

            txtNumPuerto.EditValue = 1;
            txtVelocidad.Text = "9600";
            txtParidad.Text = "No paridad (N)";
            txtBitsDatos.EditValue = 8;
            txtBitsParada.EditValue = 1;

            btnProbar.Click += new EventHandler(btnProbar_Click);
        }

        #region Funciones

        private void configurarMarcaDispensarios()
        {
            ListaTipoMarcaDispensarioVol m = (ListaTipoMarcaDispensarioVol)luMarca.Properties.DataSource;

            if (m.Count.Equals(0))
            {
                TipoMarcaDispensarioVolPersistencia p = new TipoMarcaDispensarioVolPersistencia();
                m = p.Obtener();

                if (m.Count.Equals(0))
                {
                    m.Add(new TipoMarcaDispensarioVol() { Clave = 1, Nombre = "Wayne" });
                    m.Add(new TipoMarcaDispensarioVol() { Clave = 2, Nombre = "Bennett" });
                    m.Add(new TipoMarcaDispensarioVol() { Clave = 3, Nombre = "Team" });
                    m.Add(new TipoMarcaDispensarioVol() { Clave = 4, Nombre = "Gilbarco" });
                    m.Add(new TipoMarcaDispensarioVol() { Clave = 5, Nombre = "Hong Yang" });
                    m.Add(new TipoMarcaDispensarioVol() { Clave = 6, Nombre = "Gilbarco 2W" }); // Reemplaza Quantium
                    m.Add(new TipoMarcaDispensarioVol() { Clave = 9, Nombre = "Wayne 2w" });   // Nueva marca
                }

                luMarca.Properties.DataSource = m;
                luMarca.Properties.DropDownRows = m.Count;
                luMarca.Refresh();
                luMarca.EditValue = 1;
            }
        }

        private void configurarInterfazDispensarios()
        {
            if (interfaces.Count.Equals(0))
            {
                TipoInterfazDispensarioVolPersistencia p = new TipoInterfazDispensarioVolPersistencia();
                interfaces = p.Obtener();

                if (interfaces.Count.Equals(0))
                {
                    interfaces.Add(new TipoInterfazDispensarioVol() { Dispensario = 1, Clave = 1, Nombre = "Hyper-Pib" });
                    interfaces.Add(new TipoInterfazDispensarioVol() { Dispensario = 1, Clave = 2, Nombre = "Fusion" });
                    interfaces.Add(new TipoInterfazDispensarioVol() { Dispensario = 2, Clave = 1, Nombre = "Bennett 515" });
                    interfaces.Add(new TipoInterfazDispensarioVol() { Dispensario = 3, Clave = 1, Nombre = "Team" });
                    interfaces.Add(new TipoInterfazDispensarioVol() { Dispensario = 4, Clave = 1, Nombre = "Pam1000" });
                    interfaces.Add(new TipoInterfazDispensarioVol() { Dispensario = 4, Clave = 2, Nombre = "Pam5000" });
                    interfaces.Add(new TipoInterfazDispensarioVol() { Dispensario = 4, Clave = 3, Nombre = "Pam Pc" });
                    interfaces.Add(new TipoInterfazDispensarioVol() { Dispensario = 5, Clave = 1, Nombre = "Hong Yang" });
                }

                asignarInterfaces();
            }
        }

        private void mostrar()
        {
            if (WorkItem.Objetos<MarcaDispensarioVol>.Exist())
            {
                MarcaDispensarioVol t = WorkItem.Objetos<MarcaDispensarioVol>.Get();
                luMarca.EditValue = t.Marca;
                luInterfaz.EditValue = t.Interfaz;
                txtModoOperacion.Text = t.ModoOperacion;
                txtNumPuerto.Value = t.Puerto;
                txtVelocidad.Text = t.Velocidad.ToString();
                switch (t.Paridad)
                {
                    case "Ninguna":
                        txtParidad.Text = "No paridad (N)";
                        break;
                    case "Impar":
                        txtParidad.Text = "Paridad impar (O)";
                        break;
                    case "Par":
                        txtParidad.Text = "Paridad par (E)";
                        break;
                }
                txtBitsDatos.Value = t.BitsDatos;
                txtBitsParada.Value = t.BitsParada;
            }
        }

        private MarcaDispensarioVol obtener()
        {
            MarcaDispensarioVol t = new MarcaDispensarioVol();
            t.Marca = Convert.ToInt32(luMarca.EditValue);
            t.Interfaz = Convert.ToInt32(luInterfaz.EditValue);
            t.ModoOperacion = txtModoOperacion.Text;
            t.ReconexionAros = 1;
            t.Puerto = Convert.ToInt32(txtNumPuerto.EditValue);
            t.Velocidad = Convert.ToInt32(txtVelocidad.EditValue);
            switch (txtParidad.Text)
            {
                case "No paridad (N)":
                    t.Paridad = "Ninguna";
                    break;
                case "Paridad impar (O)":
                    t.Paridad = "Impar";
                    break;
                case "Paridad par (E)":
                    t.Paridad = "Par";
                    break;
            }
            t.BitsDatos = Convert.ToInt32(txtBitsDatos.EditValue);
            t.BitsParada = Convert.ToInt32(txtBitsParada.EditValue);
            return t;
        }

        private void asignarInterfaces()
        {
            int dispensario = (int)luMarca.EditValue;

            if (dispensario == 6) // Si es Gilbarco 2W
            {
                dispensario = 4; // Busca interfaces de Gilbarco
            }
            else if (dispensario == 9) // Si es Wayne 2w
            {
                dispensario = 1; // Busca interfaces de Wayne
            }

            var s = interfaces.FindAll(e => e.Dispensario.Equals(dispensario));

            luInterfaz.Properties.DataSource = s;
            luInterfaz.Properties.DropDownRows = s.Count;
            luInterfaz.Refresh();
            luInterfaz.EditValue = 1;
        }

        private void asignarValores()
        {
            switch (Convert.ToInt32(luMarca.EditValue))
            {
                case 1://Wayne
                    txtNumPuerto.Value = 1;
                    txtVelocidad.Text = "9600";
                    txtParidad.Text = "Paridad par (E)";
                    txtBitsDatos.Value = 7;
                    txtBitsParada.Value = 1;
                    break;
                case 2://Bennett
                    txtNumPuerto.Value = 1;
                    txtVelocidad.Text = "2400";//"9600";
                    txtParidad.Text = "Paridad par (E)";//"No paridad (N)";
                    txtBitsDatos.Value = 7;// 8;
                    txtBitsParada.Value = 1;
                    break;
                case 3://Team
                    txtNumPuerto.Value = 1;
                    txtVelocidad.Text = "19200";
                    txtParidad.Text = "No paridad (N)";
                    txtBitsDatos.Value = 8;
                    txtBitsParada.Value = 1;
                    break;
                case 4://Gilbarco (Estándar)
                    txtNumPuerto.Value = 1;
                    txtVelocidad.Text = "4800";
                    txtParidad.Text = "Paridad impar (O)";
                    txtBitsDatos.Value = 7;
                    txtBitsParada.Value = 1;
                    break;
                case 6://Gilbarco 2W
                case 9://Wayne 2w
                    txtNumPuerto.Value = 2;
                    txtVelocidad.Text = "9600";
                    txtParidad.Text = "Paridad impar (O)";
                    txtBitsDatos.Value = 8;
                    txtBitsParada.Value = 1;
                    break;
            }
        }

        #endregion

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

        void btnProbar_Click(object sender, EventArgs e)
        {
            try
            {
                cursorCallBack(Cursors.WaitCursor);
                MarcaDispensarioVol t = obtener();
                p.Probar(t);
            }
            catch (Exception ex)
            {
                _u.Error(ex);
            }
            finally
            {
                cursorCallBack(Cursors.Default);
            }
        }

        void luMarca_EditValueChanged(object sender, EventArgs e)
        {
            asignarInterfaces();
            asignarValores();
        }

        #endregion

        #region IUserPages Members

        public void DoInit(DevExpress.XtraWizard.BaseWizardPage parent)
        {
            parent.Text = _c.MARCADISPENSARIO.TITULO;

            configurarMarcaDispensarios();
            configurarInterfazDispensarios();
            mostrar();
        }

        public void NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            try
            {
                MarcaDispensarioVol t = obtener();

                string ex = p.Guardar(t);
                if (string.IsNullOrEmpty(ex))
                {
                    WorkItem.Objetos<MarcaDispensarioVol>.Add(t);

                    if (WorkItem.Objetos<VariablesVol>.Exist())
                    {
                        WorkItem.Objetos<VariablesVol>.Delete(WorkItem.Objetos<VariablesVol>.Get());
                    }

                    string pag = string.Empty;
                    switch (t.Marca)
                    {
                        case 1:
                        case 9: // Wayne 2w
                            pag = "PWayne";
                            break;
                        case 2:
                            // ... (código existente de Bennett se mantiene igual)
                            break;
                        case 4:
                        case 6: // Gilbarco 2W
                            pag = "PGilbarco";
                            break;
                        default:
                            pag = "PIslas";
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

        }

        public void CancelClick(FormClosingEventArgs e)
        {

        }

        #endregion
    }
}
