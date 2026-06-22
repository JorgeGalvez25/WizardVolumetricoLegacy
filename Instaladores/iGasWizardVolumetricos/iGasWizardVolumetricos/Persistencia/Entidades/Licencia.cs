using System;

namespace iGasWizardVolumetricos.Persistencia.Entidades
{
    public class Licencia
    {
        public Licencia()
        {
            this.EsTemporal = false;
            this.FechaVence = null;
            this.LicenciaVolumetrico =
                this.RazonSocial = string.Empty;
            this.NoSentinel = 0;
            this.NoEstacion = 0;
        }

        public string RazonSocial { get; set; }
        public int NoSentinel { get; set; }
        public int NoEstacion { get; set; }

        public string LicienciaInocua { get; set; }
        public string LicenciaVolumetrico { get; set; }
        public string LicenciaControlVersiones { get; set; }

        public bool EsTemporal { get; set; }
        public bool EsTemporalInocuo { get; set; }

        public DateTime? FechaVence { get; set; }
        public DateTime? FechaVenceInocua { get; set; }
        public DateTime? FechaVenceControlVersiones { get; set; }

        public string Sistema { get { return "CVOL"; } }
        public string Version { get { return "3.1"; } }
        public string TipoLicencia { get { return "Abierta"; } }
        public int Usuarios { get { return 1; } }
    }

    public class FiltroLicencia
    {
        public string Sistema { get { return "CVOL"; } }
    }
}
