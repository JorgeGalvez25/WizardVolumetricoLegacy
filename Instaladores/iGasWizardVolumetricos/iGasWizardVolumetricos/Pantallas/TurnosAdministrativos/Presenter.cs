using iGasWizardVolumetricos.Persistencia.Entidades;
using iGasWizardVolumetricos.Persistencia.Logica;

namespace iGasWizardVolumetricos.Pantallas.TurnosAdministrativos
{
    public class Presenter
    {
        public ListaTurnosAdministrativos ObtenerTodosTurnos()
        {
            TurnosAdministrativosPersistencia servicio = new TurnosAdministrativosPersistencia();
            return servicio.ObtenerTodos();
        }

        public bool InsertarTurnos(ListaTurnosAdministrativos turnos)
        {
            TurnosAdministrativosPersistencia servicio = new TurnosAdministrativosPersistencia();
            return servicio.InsertarLista(turnos);//InsertarActualizarLista(turnos);
        }
    }
}
