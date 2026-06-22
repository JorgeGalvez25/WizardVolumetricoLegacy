using System;
using System.Drawing.Printing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Interfaces;
using iGasWizardVolumetricos.Persistencia.Entidades;
using iGasWizardVolumetricos.Persistencia.Logica;
using _c = iGasWizardVolumetricos.Generales.Constantes;
using _u = iGasWizardVolumetricos.Generales.Utilerias;

namespace iGasWizardVolumetricos.Pantallas.Impresora
{
    public partial class PImpresora : DevExpress.XtraEditors.XtraUserControl, IUserPages
    {
        ImpresoraVolPersistencia p = null;

        public PImpresora()
        {
            InitializeComponent();

            p = new ImpresoraVolPersistencia();

            ckRemoto.Checked = false;

            txtIP.Text = "127.0.0.1";
            txtIP.Properties.ReadOnly = true;
            txtIP.InvalidValue += new InvalidValueExceptionEventHandler(noAction_InvalidValue);

            foreach (string item in PrinterSettings.InstalledPrinters)
            {
                txtImpresora.Properties.Items.Add(item);
            }
            txtImpresora.Properties.Items.Insert(0, "Ninguna");
            txtImpresora.EditValueChanged += new EventHandler(txtImpresora_EditValueChanged);

            txtPuerto.Text = string.Empty;
            txtPuerto.InvalidValue += new InvalidValueExceptionEventHandler(noAction_InvalidValue);

            ckEjecutarNETUSE.Checked = false;

            ckRemoto.EditValueChanged += new EventHandler(ckRemoto_EditValueChanged);
        }

        #region Funciones

        private void mostrar()
        {
            if (WorkItem.Objetos<ImpresoraVol>.Exist())
            {
                ImpresoraVol t = WorkItem.Objetos<ImpresoraVol>.Get();
                ckRemoto.Checked = t.Remoto;
                txtIP.Text = t.IP;
                txtImpresora.Text = t.Impresora;
                txtTipoPuerto.Text = t.TipoPuerto;
                txtPuerto.Text = t.Puerto;
                ckEjecutarNETUSE.Checked = t.EjecutarNetUse;
            }
        }

        private ImpresoraVol obtener()
        {
            ImpresoraVol t = new ImpresoraVol();
            t.Remoto = ckRemoto.Checked;
            t.IP = txtIP.Text;
            t.Impresora = txtImpresora.Text;
            t.TipoPuerto = txtTipoPuerto.Text;
            t.Puerto = txtPuerto.Text;
            t.EjecutarNetUse = ckEjecutarNETUSE.Checked;
            return t;
        }

        private bool datosSonValidos()
        {
            if (string.IsNullOrEmpty(txtImpresora.Text))
            {
                _u.Informacion(_c.MENSAJES.IMPRESORA_NO_DEBE_IR_VACIA);
                txtImpresora.Focus();
                return false;
            }

            if (txtImpresora.Text == "Ninguna") { return true; }

            if (ckRemoto.Checked && string.IsNullOrEmpty(txtIP.Text))
            {
                _u.Informacion(_c.MENSAJES.NO_SE_HA_PROPORCIONADO_IP);
                txtIP.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPuerto.Text))
            {
                _u.Informacion(_c.MENSAJES.PUERTO_NO_DEBE_IR_VACIO);
                txtPuerto.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region Eventos

        void txtImpresora_EditValueChanged(object sender, EventArgs e)
        {
            if (txtImpresora.Text == "Ninguna")
            {
                ckRemoto.Checked = false;
                ckRemoto.Properties.ReadOnly = true;
                txtIP.Text = "127.0.0.1";
                txtIP.Properties.ReadOnly = true;
                txtTipoPuerto.Properties.ReadOnly = true;
                txtPuerto.Text = string.Empty;
                txtPuerto.Properties.ReadOnly = true;
                ckEjecutarNETUSE.Checked = false;
                ckEjecutarNETUSE.Properties.ReadOnly = true;
            }
            else
            {
                ckRemoto.Properties.ReadOnly = false;
                txtIP.Properties.ReadOnly = !ckRemoto.Checked;
                txtTipoPuerto.Properties.ReadOnly = false;
                txtPuerto.Properties.ReadOnly = false;
                ckEjecutarNETUSE.Properties.ReadOnly = false;
            }
        }

        void ckRemoto_EditValueChanged(object sender, EventArgs e)
        {
            txtIP.Properties.ReadOnly = !ckRemoto.Checked;
            txtIP.Text = ckRemoto.Checked ? txtIP.Text : "127.0.0.1";
        }

        void noAction_InvalidValue(object sender, InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        #endregion

        #region IUserPages Members

        public void DoInit(DevExpress.XtraWizard.BaseWizardPage parent)
        {
            parent.Text = _c.IMPRESORA.TITULO;
            mostrar();
        }

        public void NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            if (datosSonValidos())
            {
                try
                {
                    ImpresoraVol t = obtener();

                    string ex = p.Guardar(t);
                    if (string.IsNullOrEmpty(ex))
                    {
                        WorkItem.Objetos<ImpresoraVol>.Add(t);
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
