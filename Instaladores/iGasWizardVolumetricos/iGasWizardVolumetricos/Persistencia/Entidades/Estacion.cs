namespace iGasWizardVolumetricos.Persistencia.Entidades
{
    public class Estacion
    {
        public Estacion()
        {
            this.Clave = 0;
            this.Consola =
                this.TipoDispensario =
                this.TipoTanques = null;
            this.Nombre =
                this.NumeroEstacion = string.Empty;
        }

        public int Clave { get; set; }
        public string Nombre { get; set; }
        public string Consola { get; set; }
        public string TipoDispensario { get; set; }
        public string TipoTanques { get; set; }
        public string NumeroEstacion { get; set; }
    }
}
