namespace iGasWizardVolumetricos.Persistencia.Entidades
{
    public class Empresa
    {
        public Empresa()
        {
            this.ClavePEMEX =
                this.Direccion =
                this.NombreComercial =
                this.NombreEstacion =
                this.NumEstacion =
                this.Poblacion =
                this.RFC =
                this.UsuarioPEMEX = string.Empty;
        }
        public string NombreComercial { get; set; }
        public string RFC { get; set; }
        public string Direccion { get; set; }
        public string Poblacion { get; set; }
        public string NumEstacion { get; set; }
        public string NombreEstacion { get; set; }
        public string UsuarioPEMEX { get; set; }
        public string ClavePEMEX { get; set; }
    }
}
