using System.Collections.Generic;

namespace iGasWizardVolumetricos.Persistencia.Entidades
{
    public class PosicionCargaVol
    {
        public PosicionCargaVol()
        {
            this.Isla = 0;
            this.Dispensario = 0;
            this.Clave = 0;
            this.NumeroMangueras = 0;
        }

        public int Isla { get; set; }
        public int Dispensario { get; set; }
        public int Clave { get; set; }
        public int NumeroMangueras { get; set; }
    }

    public class ListaPosicionCargaVol : List<PosicionCargaVol> { }
}
