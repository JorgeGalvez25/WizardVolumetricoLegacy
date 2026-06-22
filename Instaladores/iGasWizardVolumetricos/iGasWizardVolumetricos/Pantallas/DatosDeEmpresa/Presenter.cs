using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Persistencia.Entidades;
using iGasWizardVolumetricos.Persistencia.Logica;

namespace iGasWizardVolumetricos.Pantallas.DatosDeEmpresa
{
    public class Presenter
    {
        public EmpresaConfiguracion ObtenerConfiguracion(Empresa datosEmpresa)
        {
            EmpresaConfiguracion emp = WorkItem.Objetos<EmpresaConfiguracion>.Exist() ?
                                            WorkItem.Objetos<EmpresaConfiguracion>.Get() :
                                            new EmpresaConfiguracion();

            Licencia lic = WorkItem.Objetos<Licencia>.Exist() ?
                                WorkItem.Objetos<Licencia>.Get() :
                                new Licencia();

            emp.SetEmpresa(datosEmpresa);
            emp.SetLicencia(lic);

            return emp;
        }

        public bool Guardar(Empresa datosEmpresa, ref string msj)
        {
            var result = this.ObtenerConfiguracion(datosEmpresa);

            EmpresaConfiguracionPersistencia servicio = new EmpresaConfiguracionPersistencia();
            bool resp = servicio.ActualizarOInsertar(result) != null;
            if (!resp)
            {
                msj = servicio.MensajeError;
            }
            return resp;
        }

        public Empresa ObtenerEmpresa()
        {
            EmpresaConfiguracionPersistencia servicio = new EmpresaConfiguracionPersistencia();
            var d = servicio.Obtener();
            return d == null ? new Empresa() : d.GetEmpresa();
        }
    }
}
