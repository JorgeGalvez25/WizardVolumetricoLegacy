namespace iGasWizardVolumetricos.Persistencia.Entidades
{
    public class MarcaDispensarioVol
    {
        public MarcaDispensarioVol()
        {
            this.Clave = "DISP";
            this.Marca = 0;
            this.Interfaz = 0;
            this.ModoOperacion = string.Empty;
            this.ReconexionAros = 0;
            this.Puerto = 0;
            this.Velocidad = 0;
            this.Paridad = string.Empty;
            this.BitsDatos = 0;
            this.BitsParada = 0;
        }

        public string Clave { get; set; }
        public int Marca { get; set; }
        public int Interfaz { get; set; }
        public string ModoOperacion { get; set; }
        public int ReconexionAros { get; set; }
        public int Puerto { get; set; }
        public int Velocidad { get; set; }
        public string Paridad { get; set; }
        public int BitsDatos { get; set; }
        public int BitsParada { get; set; }
    }
}
