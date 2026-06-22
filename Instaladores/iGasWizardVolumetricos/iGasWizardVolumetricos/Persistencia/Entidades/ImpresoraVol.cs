namespace iGasWizardVolumetricos.Persistencia.Entidades
{
    public class ImpresoraVol
    {
        public ImpresoraVol()
        {
            this.Remoto = false;
            this.IP = string.Empty;
            this.Impresora = string.Empty;
            this.Puerto = string.Empty;
            this.TipoPuerto = string.Empty;
            this.EjecutarNetUse = false;
        }

        public bool Remoto { get; set; }
        public string IP { get; set; }
        public string Impresora { get; set; }
        public string Puerto { get; set; }
        public string TipoPuerto { get; set; }
        public bool EjecutarNetUse { get; set; }
    }
}
