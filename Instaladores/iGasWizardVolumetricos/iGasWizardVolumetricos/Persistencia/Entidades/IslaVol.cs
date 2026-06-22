using System.Collections.Generic;

namespace iGasWizardVolumetricos.Persistencia.Entidades
{
    public class IslaVol
    {
        public IslaVol()
        {
            this.Clave = 0;
            this.NumeroDispensarios = 0;
        }

        public int Clave { get; set; }
        public int NumeroDispensarios { get; set; }
    }

    public class ListaIslaVol : List<IslaVol> { }
}
