using System.Collections.Generic;

namespace iGasWizardVolumetricos.Persistencia.Entidades
{
    public class TipoMarcaDispensarioVol
    {
        public TipoMarcaDispensarioVol()
        {
            this.Clave = 0;
            this.Nombre = string.Empty;
        }

        public int Clave { get; set; }
        public string Nombre { get; set; }
    }

    public class ListaTipoMarcaDispensarioVol : List<TipoMarcaDispensarioVol> { }
}
