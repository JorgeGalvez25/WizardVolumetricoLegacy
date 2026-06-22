using System.Collections.Generic;

namespace iGasWizardVolumetricos.Persistencia.Entidades
{
    public class DispensarioVol
    {
        public DispensarioVol()
        {
            this.Isla = 0;
            this.Clave = 0;
            this.NumeroPosiciones = 0;
        }

        public int Isla { get; set; }
        public int Clave { get; set; }
        public int NumeroPosiciones { get; set; }
    }

    public class ListaDispensarioVol : List<DispensarioVol> { }
}
