using System;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraWizard;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Interfaces;
using iGasWizardVolumetricos.Persistencia.Entidades;

namespace iGasWizardVolumetricos.Pantallas.TurnosAdministrativos
{
    public partial class PTurnosAdministrativos : XtraUserControl, IUserPages
    {
        public Presenter Presenter { get; set; }

        public PTurnosAdministrativos()
        {
            this.InitializeComponent();
            this.Presenter = new Presenter();
            this.InicializaTabla();
            this.InicializaBotones();
        }

        private void InicializaTabla()
        {
            this.spnMinAntes.Properties.MaxValue =
                this.spnMinDespues.Properties.MaxValue = 60;
            this.spnMinAntes.Properties.MinValue =
                this.spnMinDespues.Properties.MinValue = 10;

            this.grcTurnos.DataSource = new ListaTurnosAdministrativos();
            this.grcTurnos.DataSourceChanged += new EventHandler(grcTurnos_DataSourceChanged);

            this.grvTurnos.BeginInit();
            {
                this.grvTurnos.OptionsPrint.AutoWidth =
                    this.grvTurnos.OptionsBehavior.AutoExpandAllGroups = true;
                this.grvTurnos.OptionsCustomization.AllowColumnMoving =
                    this.grvTurnos.OptionsCustomization.AllowColumnResizing =
                    this.grvTurnos.OptionsCustomization.AllowFilter =
                    this.grvTurnos.OptionsCustomization.AllowGroup =
                    this.grvTurnos.OptionsCustomization.AllowRowSizing =
                    this.grvTurnos.OptionsCustomization.AllowSort =
                    this.grvTurnos.OptionsView.ShowGroupPanel = false;

                this.grvTurnos.Columns[0].Caption = "Turno";
                this.grvTurnos.Columns[0].OptionsColumn.ReadOnly = true;

                this.grvTurnos.Columns[1].Caption = "Hora Inicio";
                this.grvTurnos.Columns[1].ColumnEdit = new RepositoryItemTimeEdit();
                {

                    (this.grvTurnos.Columns[1].ColumnEdit as RepositoryItemTimeEdit).DisplayFormat.FormatType = FormatType.DateTime;
                    (this.grvTurnos.Columns[1].ColumnEdit as RepositoryItemTimeEdit).DisplayFormat.FormatString = "HH:mm";

                    (this.grvTurnos.Columns[1].ColumnEdit as RepositoryItemTimeEdit).EditFormat.FormatType = FormatType.DateTime;
                    (this.grvTurnos.Columns[1].ColumnEdit as RepositoryItemTimeEdit).EditFormat.FormatString = "HH:mm";

                    (this.grvTurnos.Columns[1].ColumnEdit as RepositoryItemTimeEdit).EditMask = "HH:mm";

                    this.grvTurnos.Columns[1].DisplayFormat.FormatType = FormatType.Custom;
                    this.grvTurnos.Columns[1].DisplayFormat.FormatString = "HH:mm";

                    (this.grvTurnos.Columns[1].ColumnEdit as RepositoryItemTimeEdit).CustomDisplayText += this.PTurnosAdministrativos_CustomDisplayText;
                    (this.grvTurnos.Columns[1].ColumnEdit as RepositoryItemTimeEdit).Leave += this.PTurnosAdministrativos_EditValueChanged;//EditValueChanged += this.PTurnosAdministrativos_EditValueChanged;
                }

                this.grvTurnos.Columns[2].Caption = "Hora Fin";
                this.grvTurnos.Columns[2].ColumnEdit = new RepositoryItemTimeEdit();
                {

                    (this.grvTurnos.Columns[2].ColumnEdit as RepositoryItemTimeEdit).DisplayFormat.FormatType = FormatType.DateTime;
                    (this.grvTurnos.Columns[2].ColumnEdit as RepositoryItemTimeEdit).DisplayFormat.FormatString = "HH:mm";

                    (this.grvTurnos.Columns[2].ColumnEdit as RepositoryItemTimeEdit).EditFormat.FormatType = FormatType.DateTime;
                    (this.grvTurnos.Columns[2].ColumnEdit as RepositoryItemTimeEdit).EditFormat.FormatString = "HH:mm";

                    (this.grvTurnos.Columns[2].ColumnEdit as RepositoryItemTimeEdit).EditMask = "HH:mm";

                    (this.grvTurnos.Columns[2].ColumnEdit as RepositoryItemTimeEdit).CustomDisplayText += this.PTurnosAdministrativos_CustomDisplayText;
                    (this.grvTurnos.Columns[2].ColumnEdit as RepositoryItemTimeEdit).Leave += this.PTurnosAdministrativos_EditValueChanged;//EditValueChanged += this.PTurnosAdministrativos_EditValueChanged;
                }

                this.grvTurnos.Columns[3].Visible =
                    this.grvTurnos.Columns[4].Visible =
                    this.grvTurnos.Columns[5].Visible =
                    this.grvTurnos.Columns[6].Visible = false;

            }
            this.grvTurnos.EndInit();
        }
        private void InicializaBotones()
        {
            this.btnAgregar.Click += this.btnAgregar_Click;
            this.btnEliminar.Click += this.btnEliminar_Click;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var lista = (ListaTurnosAdministrativos)this.grcTurnos.DataSource;
            if (lista.Count < 4)
            {
                lista.Adds(new TurnoAdministrativo() { Turno = lista.Count + 1 });
                this.grcTurnos.Refresh();
                this.grcTurnos.Update();
                this.grcTurnos.RefreshDataSource();
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var lista = (ListaTurnosAdministrativos)this.grcTurnos.DataSource;
            if (lista.Count > 2)
            {
                lista.Removes(lista.Count - 1);
                this.grcTurnos.Refresh();
                this.grcTurnos.Update();
                this.grcTurnos.RefreshDataSource();
            }
        }

        private void grcTurnos_DataSourceChanged(object sender, EventArgs e)
        {
            this.grcTurnos.Refresh();
            this.grcTurnos.Update();
            this.grcTurnos.RefreshDataSource();
        }
        private void EsHoraDeAventura_ItemChanged(object sender, TurnosEvent e)
        {
            var item = (TurnoAdministrativo)sender;
            var lista = (ListaTurnosAdministrativos)this.grcTurnos.DataSource;
            int idx = item.Turno - 1;
            int lastItem = lista.Count - 1;
            TimeSpan aux = TimeSpan.MinValue;
            int iAnt = lista[idx].TurnoAnterior;
            int iPot = lista[idx].TurnoPosterior;

            try
            {
                switch (e.TurnoActual)
                {
                    case ETurnoAdministrativo.Inicio: // Se modifico el elemento Inicio de un registro

                        // Verifica que el renglon este validado correctamente
                        if (lista[idx].HoraInicio >= lista[idx].HoraFin)
                        {
                            // Si es el ultimo elemento
                            if (idx < lastItem)
                            {
                                // Se agrega 1 minuto al elemento ya que la fecha es de un dia posterior
                                lista[idx].HoraFin = TimeSpan.FromMinutes(lista[idx].HoraInicio.TotalMinutes + 1);
                            }
                        }

                        // Se obtiene el valor para el elemento anterior
                        aux = TimeSpan.FromMinutes(lista[idx].HoraInicio.TotalMinutes - 1);

                        // Si es el primer elemento
                        if (idx == 0)
                        {
                            // Si el elemento anterior es mayor al o igual a 1 dia
                            if (lista[iAnt].HoraFin.Days > 0)
                            {
                                // Se agregan los dias a la variable para el elemento anterior
                                aux = TimeSpan.FromMinutes(aux.TotalMinutes + (lista[iAnt].HoraFin.Days * 1440));
                            }
                        }

                        // Si el elemento anterior es diferente
                        if (lista[iAnt].HoraFin != aux)
                        {
                            // Se modifica el valor
                            lista[iAnt].HoraFin = aux;
                        }

                        break;
                    case ETurnoAdministrativo.Fin: // Se modifico el elemento Fin de un registro

                        // Verifica que el renglon este validado correctamente
                        if (lista[idx].HoraFin <= lista[idx].HoraInicio)
                        {
                            // Si es el ultimo elemento
                            if (idx < lastItem)
                            {
                                // Se resta 1 minuto al elemento ya que la fecha es de un dia posterior
                                lista[idx].HoraInicio = TimeSpan.FromMinutes(lista[idx].HoraFin.TotalMinutes - 1);
                            }
                        }

                        // Se obtiene el valor para el elemento posterior
                        aux = TimeSpan.FromMinutes(lista[idx].HoraFin.TotalMinutes + 1);

                        // Si es el ultimo elemento
                        if (idx == lastItem)
                        {
                            // Si el elemento posterior no tiene mas de 0 dias
                            if (lista[iPot].HoraInicio.Days <= 0)
                            {
                                // Se restan los dias a la variable para el elemento posterior
                                aux = TimeSpan.FromMinutes(aux.TotalMinutes - (aux.Days * 1440));
                            }
                        }

                        // Si el elemento posterior es diferente
                        if (lista[iPot].HoraInicio != aux)
                        {
                            // Se modifica el valor
                            lista[iPot].HoraInicio = aux;
                        }

                        break;
                    case ETurnoAdministrativo.Maximo:
                    case ETurnoAdministrativo.Minimo:
                    case ETurnoAdministrativo.none:
                    default:
                        break;
                }
            }
            catch
            {
            }
        }
        private void PTurnosAdministrativos_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.TimeEdit item = (sender as DevExpress.XtraEditors.TimeEdit);

            TurnoAdministrativo turno = (TurnoAdministrativo)this.grvTurnos.GetFocusedRow();

            var lista = (ListaTurnosAdministrativos)this.grcTurnos.DataSource;
            int idx = this.grvTurnos.FocusedRowHandle;

            switch (this.grvTurnos.FocusedColumn.FieldName)
            {
                case "HoraInicio":
                    lista[idx].HoraInicio = TimeSpan.FromTicks(item.Time.Ticks); // Dispara el evento lista_ItemChanged
                    break;
                case "HoraFin":
                    lista[idx].HoraFin = TimeSpan.FromTicks(item.Time.Ticks); // Dispara el evento lista_ItemChanged
                    break;
            }

            this.grcTurnos.Refresh();
            this.grcTurnos.Update();
            this.grcTurnos.RefreshDataSource();
        }
        private void PTurnosAdministrativos_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            /// En este punto el valor de e.DisplayText puede contener dias, "1.06:12:00" y para evitar que se muestren
            /// se realizo lo siguiente:

            // Se obtiene el texto de la celda actual
            TimeSpan aux = TimeSpan.MinValue;
            TimeSpan.TryParse(e.DisplayText, out aux);
            // Se da formato con solo la hora y el minuto
            e.DisplayText = string.Format("{0:D2}:{1:D2}", aux.Hours, aux.Minutes);
        }

        #region IUserPages Members

        public void DoInit(DevExpress.XtraWizard.BaseWizardPage parent)
        {
            parent.Text = "Turnos Administrativos";
            this.BeginInvoke(new MethodInvoker(() =>
                {
                    ListaTurnosAdministrativos lista = new ListaTurnosAdministrativos();
                    {
                        if (this.grcTurnos.DataSource != null)
                        {
                            ((ListaTurnosAdministrativos)this.grcTurnos.DataSource).Clear();
                            this.grcTurnos.DataSource = null;
                        }

                        if (WorkItem.Objetos<ListaTurnosAdministrativos>.Exist()) // ¿Esta en memoria?
                        {
                            lista = WorkItem.Objetos<ListaTurnosAdministrativos>.Get();
                            this.spnMinAntes.Value = lista.MinutosAntes;
                            this.spnMinDespues.Value = lista.MinutosDespues;
                            this.grcTurnos.DataSource = lista;
                        }
                        else
                        {
                            lista = Presenter.ObtenerTodosTurnos();

                            if (lista.Count > 0) // ¿Esta en Base de Datos?
                            {
                                this.grcTurnos.DataSource = lista;
                            }
                            else
                            {
                                lista = new ListaTurnosAdministrativos(); // Pues entonces lo creo :)
                                {
                                    this.grcTurnos.DataSource = lista;
                                    lista.Adds(new TurnoAdministrativo() { Turno = 1 });
                                    lista.Adds(new TurnoAdministrativo() { Turno = 2 });
                                    lista.Adds(new TurnoAdministrativo() { Turno = 3 });
                                }
                            }
                        }

                        lista.MinutosAntes = this.spnMinAntes.Value;
                        lista.MinutosDespues = this.spnMinDespues.Value;
                        lista.ItemChanged += this.EsHoraDeAventura_ItemChanged;
                        lista.SetEvents();
                    }

                    this.grcTurnos.Update();
                    this.grcTurnos.Refresh();
                    this.grcTurnos.RefreshDataSource();
                }));
        }

        public void NextClick(object sender, WizardCommandButtonClickEventArgs e)
        {
            var lista = new ListaTurnosAdministrativos();
            lista.AddRange(((ListaTurnosAdministrativos)this.grcTurnos.DataSource).ToArray());
            lista.SetLimits(this.spnMinAntes.Value, this.spnMinDespues.Value);

            if (WorkItem.Objetos<ListaTurnosAdministrativos>.Exist())
            {
                WorkItem.Objetos<ListaTurnosAdministrativos>.Get().Clear();
                WorkItem.Objetos<ListaTurnosAdministrativos>.Delete(lista);
            }

            e.Handled = !Presenter.InsertarTurnos(lista);

            if (e.Handled)
            {
                Utilerias.Error("Ocurrió un error al guardar en BD.");
                return;
            }
            WorkItem.Objetos<ListaTurnosAdministrativos>.Add(lista);
        }

        public void PrevClick(object sender, WizardCommandButtonClickEventArgs e)
        {
        }

        public void CancelClick(FormClosingEventArgs e)
        {
        }

        #endregion
    }
}
