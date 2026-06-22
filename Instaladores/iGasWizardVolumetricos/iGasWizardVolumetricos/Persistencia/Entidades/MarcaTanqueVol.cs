namespace iGasWizardVolumetricos.Persistencia.Entidades
{
    public class MarcaTanqueVol
    {
        public MarcaTanqueVol()
        {
            this.Clave = "TANQ";
            this.Marca = 0;
            this.Puerto = 0;
            this.Velocidad = 0;
            this.Paridad = string.Empty;
            this.BitsDatos = 0;
            this.BitsParada = 0;
        }

        public string Clave { get; set; }
        public int Marca { get; set; }
        public int Puerto { get; set; }
        public int Velocidad { get; set; }
        public string Paridad { get; set; }
        public int BitsDatos { get; set; }
        public int BitsParada { get; set; }
    }
}
