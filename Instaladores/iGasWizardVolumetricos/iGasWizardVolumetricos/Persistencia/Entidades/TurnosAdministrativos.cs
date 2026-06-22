using System;
using System.Collections.Generic;

namespace iGasWizardVolumetricos.Persistencia.Entidades
{
    public enum ETurnoAdministrativo
    {
        none = 0,
        Inicio,
        Fin,
        Maximo,
        Minimo,
        Change
    }
    public class TurnoAdministrativo
    {
        public event ChangedEventHandler ItemChanged;

        protected virtual void OnItemChanged(TurnosEvent e)
        {
            if (this.ItemChanged != null)
            {
                this.ItemChanged(this, e);
            }
        }

        private TimeSpan _horaInicio;
        private TimeSpan _horaFin;

        public TurnoAdministrativo()
        {
            this.Turno = 0;
            this._horaFin =
                this._horaInicio =
                this.HoraMax =
                this.HoraMin = DateTime.Now.Date.TimeOfDay;
        }

        public int Turno { get; set; }

        public TimeSpan HoraInicio
        {
            get
            {
                return this._horaInicio;
            }
            set
            {
                if (this._horaInicio != value)
                {
                    //if (value.Days > this._horaFin.Days)
                    //{
                    //    this._horaInicio = TimeSpan.FromMinutes(value.TotalMinutes - 1440);
                    //}
                    //else
                    //{
                    this._horaInicio = value;
                    //}
                    this.OnItemChanged(new TurnosEvent() { TurnoActual = ETurnoAdministrativo.Inicio });
                }
            }
        }

        public TimeSpan HoraFin
        {
            get
            {
                return this._horaFin;
            }
            set
            {
                if (this._horaFin != value)
                {
                    //if (value.Days > this._horaInicio.Days)
                    //{
                    //    this._horaFin = TimeSpan.FromMinutes(value.TotalMinutes - 1440);
                    //}
                    //else
                    //{
                    this._horaFin = value;
                    //}
                    this.OnItemChanged(new TurnosEvent() { TurnoActual = ETurnoAdministrativo.Fin });
                }
            }
        }

        public TimeSpan HoraMax { get; set; }

        public TimeSpan HoraMin { get; set; }

        public int TurnoAnterior { get; set; }

        public int TurnoPosterior { get; set; }

        ~TurnoAdministrativo()
        {
            this.ItemChanged = null;
        }
    }

    public delegate void ChangedEventHandler(object sender, TurnosEvent e);

    public class TurnosEvent : EventArgs
    {
        public TurnosEvent()
        {
            this.TurnoActual = ETurnoAdministrativo.none;
        }

        public ETurnoAdministrativo TurnoActual { get; set; }
    }

    public class ListaTurnosAdministrativos : List<TurnoAdministrativo>
    {
        ~ListaTurnosAdministrativos()
        {
            if (this != null)
            {
                this.RemoveEvents();

                this.ListChanged = null;
                this.ItemChanged = null;
                this.MinutosDespuesChanged = null;
                this.MinutosAntesChanged = null;

                this.Clear();
            }
        }

        public event ChangedEventHandler ListChanged;
        public event ChangedEventHandler ItemChanged;
        public event ChangedEventHandler MinutosDespuesChanged;
        public event ChangedEventHandler MinutosAntesChanged;

        protected virtual void OnListChanged(TurnosEvent e)
        {
            if (this.ListChanged != null)
            {
                this.ListChanged(this, e);
            }
            this.SetLimits();
        }
        protected virtual void OnMinutosDespuesChanged(TurnosEvent e)
        {
            if (this.MinutosDespuesChanged != null)
            {
                this.MinutosDespuesChanged(this, e);
            }
        }
        protected virtual void OnMinutosAntesChanged(TurnosEvent e)
        {
            if (this.MinutosAntesChanged != null)
            {
                this.MinutosAntesChanged(this, e);
            }
        }

        private decimal _minutosMaximos;
        private decimal _minutosMinimos;

        public decimal MinutosDespues
        {
            get
            {
                return this._minutosMaximos;
            }
            set
            {
                this._minutosMaximos = value;
                this.OnMinutosDespuesChanged(new TurnosEvent());
            }
        }
        public decimal MinutosAntes
        {
            get
            {
                return this._minutosMinimos;
            }
            set
            {
                this._minutosMinimos = value;
                this.OnMinutosAntesChanged(new TurnosEvent());
            }
        }

        private TimeSpan RepartoHoras
        {
            get
            {
                return TimeSpan.FromHours(DateTime.MaxValue.TimeOfDay.TotalHours / ((this.Count <= 0) ? 1 : this.Count));
            }
        }
        private TimeSpan HoraInicioDefault
        {
            get
            {
                return TimeSpan.FromMinutes(TimeSpan.FromHours(6).TotalMinutes + 1); // 06:01
            }
        }

        private void SetAntNextValues()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].TurnoAnterior = ((i - 1) <= -1) ? (this.Count - 1) : (i - 1);
                this[i].TurnoPosterior = ((i + 1) >= this.Count) ? 0 : (i + 1);
            }
        }
        private void Calcular(TimeSpan inicio)
        {
            var fInicio = inicio;
            var fFin = TimeSpan.FromMinutes(fInicio.TotalMinutes + this.RepartoHoras.TotalMinutes - 1);
            this.SetLimits();
            int antIdx = 0;
            int nextIdx = 0;

            for (int i = 0; i < this.Count; i++)
            {
                antIdx = ((i - 1) <= -1) ? (this.Count - 1) : (i - 1);
                nextIdx = ((i + 1) >= this.Count) ? 0 : (i + 1);
                this[i].Turno = i + 1;
                this[i].TurnoAnterior = antIdx;
                this[i].TurnoPosterior = nextIdx;
                this[i].HoraInicio = fInicio;
                this[i].HoraFin = fFin;
                fInicio = TimeSpan.FromMinutes(fFin.TotalMinutes + 1);
                fFin = TimeSpan.FromMinutes(fInicio.TotalMinutes + this.RepartoHoras.TotalMinutes - 1);
            }
        }

        internal void Adds(TurnoAdministrativo turno)
        {
            this.RemoveEvents();
            this.Add(turno);
            var fInicio = this.HoraInicioDefault;
            var fFin = TimeSpan.FromMinutes(fInicio.TotalMinutes + this.RepartoHoras.TotalMinutes - 1);
            turno.HoraInicio = fInicio;
            turno.HoraFin = fFin;

            this.SetAntNextValues();
            this.Calcular(this[0].HoraInicio);

            this.OnListChanged(new TurnosEvent() { TurnoActual = ETurnoAdministrativo.Change });
            this.SetEvents();
        }
        internal void Removes(int idx)
        {
            this.RemoveEvents();
            this.RemoveAt(idx);
            this.Calcular(this[0].HoraInicio);
            this.OnListChanged(new TurnosEvent() { TurnoActual = ETurnoAdministrativo.Change });
            this.SetEvents();
        }
        internal void SetEvents()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].ItemChanged += this.ItemChanged;
            }
        }
        internal void RemoveEvents()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].ItemChanged -= this.ItemChanged;
            }
        }

        internal void SetLimits()
        {
            this.SetLimits(this.MinutosAntes, this.MinutosDespues);
        }
        internal void SetLimits(decimal antes, decimal despues)
        {
            this.MinutosDespues = despues;
            this.MinutosAntes = antes;

            for (int i = 0; i < this.Count; i++)
            {
                this[i].HoraMax = TimeSpan.FromMinutes(this[i].HoraFin.TotalMinutes + ((int)despues));
                this[i].HoraMin = TimeSpan.FromMinutes(this[i].HoraFin.TotalMinutes - ((int)antes));
            }
        }
    }
}
