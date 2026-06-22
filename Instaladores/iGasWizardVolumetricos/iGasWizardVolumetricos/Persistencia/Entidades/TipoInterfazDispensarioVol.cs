using System.Collections.Generic;

namespace iGasWizardVolumetricos.Persistencia.Entidades
{
    public class TipoInterfazDispensarioVol
    {
        public TipoInterfazDispensarioVol()
        {
            this.Dispensario = 0;
            this.Clave = 0;
            this.Nombre = string.Empty;
        }

        public int Dispensario { get; set; }
        public int Clave { get; set; }
        public string Nombre { get; set; }
    }

    public class ListaTipoInterfazDispensarioVol : List<TipoInterfazDispensarioVol> { }
}
