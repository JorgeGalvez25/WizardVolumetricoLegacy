using FirebirdSql.Data.FirebirdClient;
using iGasWizardVolumetricos.Generales;

namespace iGasWizardVolumetricos.Persistencia.Logica
{
    public class UbicarBDPersistencia
    {
        public void LiberarConexiones()
        {
            if (WorkItem.Objetos<Conexion.Conexion>.Exist())
            {
                var conn = WorkItem.Objetos<Conexion.Conexion>.Get();
                conn.Dispose();
                WorkItem.Objetos<Conexion.Conexion>.Delete(conn);
                conn = null;
            }

            Conexion.Conexion.LimpiarConexiones();
        }

        public bool Prueba(ref string msj)
        {
            var conn = WorkItem.Objetos<Conexion.Conexion>.Get();
            conn.EsPrueba = true;

            bool result = false;

            msj = conn.ConectarBDConsola((comm) =>
            {
                comm.CommandText = "SELECT * FROM DPVGTCMB";

                using (FbDataReader reader = comm.ExecuteReader())
                {
                    try
                    {
                        reader.Read();
                        result = true;
                    }
                    finally
                    {
                        if (!reader.IsClosed) { reader.Close(); }
                    }
                }
            });

            return result;
        }

        public bool BDValida()
        {
            var conn = WorkItem.Objetos<Conexion.Conexion>.Get();
            bool result = false;

            conn.ConectarBDConsola((comm) =>
                {
                    comm.CommandText = "SELECT " +
                                            "COUNT(RDB$RELATION_NAME) " +
                                       "FROM " +
                                            "RDB$RELATIONS " +
                                       "WHERE " +
                                            "RDB$SYSTEM_FLAG = 0 AND " +
                                            "RDB$RELATION_NAME = 'DPVGCONF'";
                    using (FbDataReader reader = comm.ExecuteReader())
                    {
                        try
                        {
                            if (reader.Read())
                            {
                                result = reader.IsDBNull(0) ? false : (reader.GetInt16(0) > 0);
                            }
                        }
                        finally
                        {
                            if (!reader.IsClosed) { reader.Close(); }
                        }
                    }
                });
            return result;
        }
    }
}
