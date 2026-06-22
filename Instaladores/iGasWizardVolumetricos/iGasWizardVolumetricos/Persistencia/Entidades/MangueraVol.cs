using System.Collections.Generic;

namespace iGasWizardVolumetricos.Persistencia.Entidades
{
    public class MangueraVol
    {
        public MangueraVol()
        {
            this.Isla = 0;
            this.Dispensario = 0;
            this.Posicion = 0;
            this.Indice = 0;
            this.Clave = 0;
            this.ConPosicion = null;
            this.TipoCombustible = null;
            this.CampoLectura = null;
        }

        public int Isla { get; set; }
        public int Dispensario { get; set; }
        public int Posicion { get; set; }
        public int Indice { get; set; }
        public int Clave { get; set; }
        public int? ConPosicion { get; set; }
        public string TipoCombustible { get; set; }
        public string CampoLectura { get; set; }
    }

    public class ListaMangueraVol : List<MangueraVol> { }
}
