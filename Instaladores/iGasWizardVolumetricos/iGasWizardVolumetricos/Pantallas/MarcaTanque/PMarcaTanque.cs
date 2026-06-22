using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Interfaces;
using iGasWizardVolumetricos.Persistencia.Entidades;
using iGasWizardVolumetricos.Persistencia.Logica;
using _c = iGasWizardVolumetricos.Generales.Constantes;
using _u = iGasWizardVolumetricos.Generales.Utilerias;

namespace iGasWizardVolumetricos.Pantallas.MarcaTanque
{
    public partial class PMarcaTanque : DevExpress.XtraEditors.XtraUserControl, IUserPages
    {
        MarcaTanqueVolPersistencia p = null;

        public PMarcaTanque()
        {
            InitializeComponent();

            p = new MarcaTanqueVolPersistencia();

            txtNumPuerto.EditValue = 1;
            txtVelocidad.Text = "9600";
            txtParidad.Text = "No paridad (N)";
            txtBitDatos.EditValue = 8;
            txtBitParada.EditValue = 1;

            btnProbar.Click += new EventHandler(btnProbar_Click);
        }

        #region Funciones

        private void configurarMarcas()
        {
            if (gpMarca.Properties.Items.Count.Equals(0))
            {
                Dictionary<int, string> m = p.ObtenerMarcas();
                if (m.Count.Equals(0))
                {
                    m.Add(1, "Veeder Root");
                    m.Add(2, "Eeco System");
                    m.Add(3, "AutoStick");
                    m.Add(4, "Red Jacket");
                    m.Add(5, "Petrovend");
                    m.Add(6, "Incon");
                }

                foreach (var item in m)
                {
                    gpMarca.Properties.Items.Add(new RadioGroupItem(item.Key, item.Value));
                }
                gpMarca.EditValue = 1;
            }
        }

        private void mostrar()
        {
            if (WorkItem.Objetos<MarcaTanqueVol>.Exist())
            {
                MarcaTanqueVol t = WorkItem.Objetos<MarcaTanqueVol>.Get();
                gpMarca.EditValue = t.Marca;
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
                txtBitDatos.Value = t.BitsDatos;
                txtBitParada.Value = t.BitsParada;
            }
        }

        private MarcaTanqueVol obtener()
        {
            MarcaTanqueVol t = new MarcaTanqueVol();
            t.Marca = Convert.ToInt32(gpMarca.EditValue);
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
            t.BitsDatos = Convert.ToInt32(txtBitDatos.EditValue);
            t.BitsParada = Convert.ToInt32(txtBitParada.EditValue);
            return t;
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
                MarcaTanqueVol t = obtener();
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

        #endregion

        #region IUserPages Members

        public void DoInit(DevExpress.XtraWizard.BaseWizardPage parent)
        {
            parent.Text = _c.MARCATANQUE.TITULO;

            configurarMarcas();
            mostrar();
        }

        public void NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            try
            {
                MarcaTanqueVol t = obtener();

                string ex = p.Guardar(t);
                if (string.IsNullOrEmpty(ex))
                {
                    WorkItem.Objetos<MarcaTanqueVol>.Add(t);
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

        public void PrevClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {

        }

        public void CancelClick(FormClosingEventArgs e)
        {

        }

        #endregion
    }
}
