using System.Collections.Generic;

namespace iGasWizardVolumetricos.Persistencia.Entidades
{
    public class CombustibleVol
    {
        public CombustibleVol()
        {
            this.Clave = 0;
            this.Nombre = string.Empty;
            this.ClavePEMEX = string.Empty;
            this.ConProductoPrecio = string.Empty;
        }

        public int Clave { get; set; }
        public string Nombre { get; set; }
        public string ClavePEMEX { get; set; }
        public string ConProductoPrecio { get; set; }
    }

    public class ListaCombustibleVol : List<CombustibleVol> { }
}
