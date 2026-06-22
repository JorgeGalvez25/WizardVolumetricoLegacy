using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Persistencia.Entidades;

namespace iGasWizardVolumetricos.Persistencia.Logica
{
    public class ProcesandoPersistencia
    {
        public bool ExcecutePathScript(string filePath, ref string msj)
        {
            var conn = WorkItem.Objetos<Conexion.Conexion>.Get();
            return conn.ExcecutePathScript(filePath, ref msj);
        }

        public bool ActualizaRutas(DiccionarioProcesando proc)
        {
            if (proc == null) { return false; }
            if (proc.Count <= 0) { return false; }

            var conn = WorkItem.Objetos<Conexion.Conexion>.Get();
            bool result = false;

            conn.ConectarBDConsola((comm) =>
                {
                    comm.CommandText = "UPDATE DPVGCONF SET " +
                                            "COMANDO1 = @COMM1, " +
                                            "COMANDO2 = @COMM2 ";
                    comm.Parameters.Clear();

                    comm.Parameters.Add("@COMM1", string.Format(@"{0} {1}", proc["I-Gas Consola"].RutaExe.ToUpper(), proc["I-Gas Consola"].Configuracion.ToString()));
                    comm.Parameters.Add("@COMM2", string.Format(@"{0} {1}", proc["I-Gas Tanques"].RutaExe.ToUpper(), proc["I-Gas Tanques"].Configuracion.ToString()));
                    
                    result = comm.ExecuteNonQuery() >= 1;
                });

            return result;
        }
    }
}