using System;

namespace iGasWizardVolumetricos.Persistencia.Entidades
{
    public class EmpresaConfiguracion
    {
        public EmpresaConfiguracion()
        {
            this.EsTemporal = false;
            this.FechaVence = null;
            this.LicenciaVolumetrico =
                this.RazonSocial =
                this.ClavePEMEX =
                this.Direccion =
                this.NombreComercial =
                this.NombreEstacion =
                this.NumEstacion =
                this.Poblacion =
                this.RFC =
                this.Sistema =
                this.TipoLicencia =
                this.UsuarioPEMEX = string.Empty;
            this.Usuarios =
                this.NoSentinel = 0;
        }

        public string NombreComercial { get; set; }
        public string RFC { get; set; }
        public string Direccion { get; set; }
        public string Poblacion { get; set; }
        public string NumEstacion { get; set; }
        public string NombreEstacion { get; set; }
        public string UsuarioPEMEX { get; set; }
        public string ClavePEMEX { get; set; }

        public string RazonSocial { get; set; }
        public int NoSentinel { get; set; }
        public string LicenciaVolumetrico { get; set; }

        public bool EsTemporal { get; set; }
        public bool EsTemporalInocuo { get; set; }

        public DateTime? FechaVence { get; set; }

        public string Sistema { get; set; }
        public string Version { get; set; }
        public string TipoLicencia { get; set; }
        public int Usuarios { get; set; }

        public string LicienciaControlVersiones { get; set; }
        public DateTime? FechaVenceControlVersiones { get; set; }

        public string LicienciaInocua { get; set; }
        public DateTime? FechaVenceInocua { get; set; }

        public void SetFromOthers(Empresa emp, Licencia lic)
        {
            this.SetEmpresa(emp);
            this.SetLicencia(lic);
        }

        public Licencia GetLicencia()
        {
            Licencia lic = new Licencia();
            {
                lic.EsTemporal = this.EsTemporal;
                lic.EsTemporalInocuo = this.EsTemporalInocuo;

                lic.FechaVence = this.FechaVence;
                lic.FechaVenceInocua = this.FechaVenceInocua;
                lic.FechaVenceControlVersiones = this.FechaVenceControlVersiones;

                lic.LicienciaInocua = this.LicienciaInocua;
                lic.LicenciaVolumetrico = this.LicenciaVolumetrico;
                lic.LicenciaControlVersiones = this.LicienciaControlVersiones;

                lic.NoSentinel = this.NoSentinel;
                lic.RazonSocial = this.RazonSocial;
            }
            return lic;
        }
        public void SetLicencia(Licencia lic)
        {
            if (lic != null)
            {
                this.EsTemporal = lic.EsTemporal;
                this.EsTemporalInocuo = lic.EsTemporalInocuo;

                this.FechaVence = lic.FechaVence;
                this.FechaVenceInocua = lic.FechaVenceInocua;
                this.FechaVenceControlVersiones = lic.FechaVenceControlVersiones;

                this.LicienciaInocua = lic.LicienciaInocua;
                this.LicenciaVolumetrico = lic.LicenciaVolumetrico;
                this.LicienciaControlVersiones = lic.LicenciaControlVersiones;

                this.NoSentinel = lic.NoSentinel;
                this.RazonSocial = lic.RazonSocial;
                this.Sistema = lic.Sistema;
                this.TipoLicencia = lic.TipoLicencia;
                this.Usuarios = lic.Usuarios;
                this.Version = lic.Version;
            }
        }

        public Empresa GetEmpresa()
        {
            Empresa emp = new Empresa();
            {
                emp.ClavePEMEX = this.ClavePEMEX;
                emp.Direccion = this.Direccion;
                emp.NombreComercial = this.NombreComercial;
                emp.NombreEstacion = this.NombreEstacion;
                emp.NumEstacion = this.NumEstacion;
                emp.Poblacion = this.Poblacion;
                emp.RFC = this.RFC;
                emp.UsuarioPEMEX = this.UsuarioPEMEX;
            }
            return emp;
        }
        public void SetEmpresa(Empresa emp)
        {
            if (emp != null)
            {
                this.ClavePEMEX = emp.ClavePEMEX;
                this.Direccion = emp.Direccion;
                this.NombreComercial = emp.NombreComercial;
                this.NombreEstacion = emp.NombreEstacion;
                this.NumEstacion = emp.NumEstacion;
                this.Poblacion = emp.Poblacion;
                this.RFC = emp.RFC;
                this.UsuarioPEMEX = emp.UsuarioPEMEX;
            }
        }
    }
}