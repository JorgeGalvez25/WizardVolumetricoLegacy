using System.Collections.Generic;

namespace iGasWizardVolumetricos.Persistencia.Entidades
{
    public class TanqueVol
    {
        public TanqueVol()
        {
            this.Clave = 0;
            this.TipoCombustible = string.Empty;
            this.VolumenFondo = 0D;
            this.Capacidad = 0D;
            this.Altura = 0D;
        }

        public int Clave { get; set; }
        public string TipoCombustible { get; set; }
        public double VolumenFondo { get; set; }
        public double Capacidad { get; set; }
        public double Altura { get; set; }
    }

    public class ListaTanqueVol : List<TanqueVol> { }
}
