using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using iGasWizardVolumetricos.Interfaces;
using iGasWizardVolumetricos.Persistencia.Entidades;
using iGasWizardVolumetricos.Persistencia.Logica;
using _c = iGasWizardVolumetricos.Generales.Constantes;
using _u = iGasWizardVolumetricos.Generales.Utilerias;

namespace iGasWizardVolumetricos.Pantallas.Requerimientos
{
    public partial class PRequerimientos : DevExpress.XtraEditors.XtraUserControl, IUserPages
    {
        #region Variables

        DevExpress.XtraWizard.BaseWizardPage parent = null;
        BackgroundWorker hilo = null;
        RequerimientoPersistencia _p = null;
        bool isCancelable = false;
        ListaRequerimiento pendientes = null;

        #endregion

        public PRequerimientos()
        {
            InitializeComponent();

            lbMensaje.Text = _c.MENSAJES.VALIDANDO;
            picAnimacion.Visible = true;
            gcRequerimientos.Visible = false;
            btnInstalar.Visible = false;

            isCancelable = false;

            listado();

            btnInstalar.Click += new EventHandler(btnInstalar_Click);
        }

        #region Funciones

        private void listado()
        {
            gcRequerimientos.DataSource = new ListaRequerimiento();

            gcRequerimientos.UseEmbeddedNavigator = false;
            gvRequerimientos.OptionsCustomization.AllowFilter = false;
            gvRequerimientos.OptionsCustomization.AllowSort = false;
            gvRequerimientos.OptionsMenu.EnableColumnMenu = false;
            gvRequerimientos.OptionsMenu.EnableFooterMenu = false;
            gvRequerimientos.OptionsMenu.EnableGroupPanelMenu = false;
            gvRequerimientos.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gvRequerimientos.OptionsBehavior.Editable = false;
            gvRequerimientos.OptionsView.ShowGroupPanel = false;
            gvRequerimientos.OptionsView.ShowFooter = false;
            gvRequerimientos.OptionsView.ColumnAutoWidth = false;
            gvRequerimientos.OptionsCustomization.AllowColumnMoving = false;
            gvRequerimientos.OptionsCustomization.AllowColumnResizing = false;
            gvRequerimientos.OptionsCustomization.AllowRowSizing = false;

            gvRequerimientos.Columns[0].Caption = "Requerimiento";
            gvRequerimientos.Columns[0].Width = 309;
            gvRequerimientos.Columns[0].OptionsColumn.AllowFocus = false;

            gvRequerimientos.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvRequerimientos.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvRequerimientos.Columns[1].ColumnEdit = new RepositoryItemPictureEdit();
            gvRequerimientos.Columns[1].Caption = "Instalado";
            gvRequerimientos.Columns[1].Width = 70;

            gvRequerimientos.Columns[2].Visible = false;

            //gcRequerimientos.ContextMenuStrip = new ContextMenuStrip();
            //gcRequerimientos.ContextMenuStrip.Items.Add("Instalar", iGasWizardVolumetricos.Properties.Resources.check, PRequerimientos_Click);
            //gcRequerimientos.ContextMenuStrip.Opening += new CancelEventHandler(ContextMenuStrip_Opening);
        }

        private void instalar()
        {
            if (pendientes.Count > 0)
            {
                Requerimiento t = pendientes.First();
                pendientes.Remove(t);

                t.Icono = iGasWizardVolumetricos.Properties.Resources.wait_16x16;

                ListaRequerimiento lista = (ListaRequerimiento)gcRequerimientos.DataSource;
                Requerimiento s = lista.Find(i => i.Descripcion.Equals(t.Descripcion));
                s.Instalado = t.Instalado;
                s.Icono = t.Icono;

                gvRequerimientos.RefreshData();
                gcRequerimientos.Refresh();
                gvRequerimientos.FocusedRowHandle = gvRequerimientos.GetRowHandle(lista.IndexOf(s));

                hilo = new BackgroundWorker();
                hilo.DoWork += new DoWorkEventHandler(hiloInstalar_DoWork);
                hilo.RunWorkerCompleted += new RunWorkerCompletedEventHandler(hiloInstalar_RunWorkerCompleted);
                hilo.RunWorkerAsync(t);
            }
            else
            {
                if ((gcRequerimientos.DataSource as ListaRequerimiento).SonValidos)
                {
                    lbMensaje.Text = _c.MENSAJES.REQUERIMIENTOS_VALIDOS;

                    parent.AllowBack = false;
                    parent.AllowNext = true;
                    parent.AllowCancel = true;

                    btnInstalar.Visible = false;
                }
                else
                {
                    lbMensaje.Text = _c.MENSAJES.REQUERIMIENTOS_INVALIDOS;

                    parent.AllowBack = false;
                    parent.AllowNext = false;
                    parent.AllowCancel = true;

                    btnInstalar.Visible = true;
                }

                isCancelable = true;
                cursorCallBack(Cursors.Default);
            }
        }

        #endregion

        #region Eventos

        //void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        //{
        //    Requerimiento r = (Requerimiento)gvRequerimientos.GetFocusedRow();
        //    gcRequerimientos.ContextMenuStrip.Items[0].Enabled = !r.Instalado;
        //}

        //void PRequerimientos_Click(object sender, EventArgs e)
        //{
        //    isCancelable = false;

        //    Requerimiento r = (Requerimiento)gvRequerimientos.GetFocusedRow();

        //    hilo = new BackgroundWorker();
        //    hilo.DoWork += new DoWorkEventHandler(hiloInstalar_DoWork);
        //    hilo.RunWorkerCompleted += new RunWorkerCompletedEventHandler(hiloInstalar_RunWorkerCompleted);
        //    hilo.RunWorkerAsync(r);
        //}

        void btnInstalar_Click(object sender, EventArgs e)
        {
            cursorCallBack(Cursors.WaitCursor);
            isCancelable = false;

            btnInstalar.Visible = false;

            ListaRequerimiento r = (ListaRequerimiento)gcRequerimientos.DataSource;
            pendientes = new ListaRequerimiento(r.FindAll(i => !i.Instalado));

            instalar();
        }

        void hiloInstalar_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _p = new RequerimientoPersistencia();
                e.Result = _p.Ejecutar((Requerimiento)e.Argument);

                Thread.Sleep(1000);
            }
            finally
            {
                _p = null;
            }
        }

        void hiloInstalar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    _u.Error(e.Error);
                }
                else
                {
                    ListaRequerimiento lista = (ListaRequerimiento)gcRequerimientos.DataSource;
                    Requerimiento r = (Requerimiento)e.Result;
                    Requerimiento s = lista.Find(i => i.Descripcion.Equals(r.Descripcion));
                    s.Instalado = r.Instalado;
                    s.Icono = r.Icono;

                    gvRequerimientos.RefreshData();
                    gcRequerimientos.Refresh();
                }
            }
            finally
            {
                hilo.DoWork -= new DoWorkEventHandler(hiloInstalar_DoWork);
                hilo.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(hiloInstalar_RunWorkerCompleted);
                hilo.Dispose();
                hilo = null;

                instalar();
            }
        }

        void hilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                cursorCallBack(Cursors.WaitCursor);

                _p = new RequerimientoPersistencia();
                e.Result = _p.Ejecutar();

                Thread.Sleep(2000);
            }
            finally
            {
                _p = null;
            }
        }

        void hilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                picAnimacion.Visible = false;
                ListaRequerimiento r = null;

                if (e.Error != null)
                {
                    r = new ListaRequerimiento();
                    r.Add(new Requerimiento(_c.REQUERIMIENTOS.NETFRAMEWORK35));
                    r.Add(new Requerimiento(_c.REQUERIMIENTOS.FIREBIRD25));
                    r.Add(new Requerimiento(_c.REQUERIMIENTOS.BDE));
                    //r.Add(new Requerimiento(_c.REQUERIMIENTOS.DRIVERMICRODOG));

                    _u.Error(e.Error);
                }
                else
                {
                    r = (ListaRequerimiento)e.Result;
                }

                if (r.SonValidos)
                {
                    lbMensaje.Text = _c.MENSAJES.REQUERIMIENTOS_VALIDOS;

                    parent.AllowBack = false;
                    parent.AllowNext = true;
                    parent.AllowCancel = true;

                    btnInstalar.Visible = false;
                }
                else
                {
                    lbMensaje.Text = _c.MENSAJES.REQUERIMIENTOS_INVALIDOS;

                    parent.AllowBack = false;
                    parent.AllowNext = false;
                    parent.AllowCancel = true;

                    btnInstalar.Visible = true;
                }

                gcRequerimientos.DataSource = r;
                gcRequerimientos.Visible = true;
                gcRequerimientos.Refresh();
            }
            finally
            {
                hilo.DoWork -= new DoWorkEventHandler(hilo_DoWork);
                hilo.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(hilo_RunWorkerCompleted);
                hilo.Dispose();
                hilo = null;

                isCancelable = true;
                cursorCallBack(Cursors.Default);
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

        #region IUserPages Members

        public void DoInit(DevExpress.XtraWizard.BaseWizardPage parent)
        {
            parent.Text = _c.REQUERIMIENTOS.TITULO;
            parent.AllowBack = false;
            parent.AllowNext = false;
            parent.AllowCancel = false;

            this.parent = parent;

            hilo = new BackgroundWorker();
            hilo.DoWork += new DoWorkEventHandler(hilo_DoWork);
            hilo.RunWorkerCompleted += new RunWorkerCompletedEventHandler(hilo_RunWorkerCompleted);
            hilo.RunWorkerAsync();
        }

        public void NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {

        }

        public void PrevClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            e.Handled = true;
        }

        public void CancelClick(FormClosingEventArgs e)
        {
            e.Cancel = !isCancelable;
        }

        #endregion
    }
}
