using System;
using System.Data;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Persistencia.Entidades;

namespace iGasWizardVolumetricos.Persistencia.Logica
{
    public class CombustibleVolPersistencia
    {
        public ListaCombustibleVol Obtener()
        {
            Exception ex = null;
            ListaCombustibleVol r = new ListaCombustibleVol();
            UbicarBD db = WorkItem.Objetos<UbicarBD>.Get();
            Conexion.Conexion cxn = new iGasWizardVolumetricos.Persistencia.Logica.Conexion.Conexion(db.RutaBD);
            cxn.ConectarBDConsola((c) =>
            {
                try
                {
                    c.CommandText = "SELECT CLAVE,NOMBRE,CLAVEPEMEX,CON_PRODUCTOPRECIO FROM DPVGTCMB";
                    using (IDataReader reader = c.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            r.Add(readerToEntidad(reader));
                        }
                    }
                }
                catch (Exception e)
                {
                    ex = e;
                    throw;
                }

            });

            if (ex != null)
            {
                throw ex;
            }

            return r;
        }

        private CombustibleVol readerToEntidad(IDataReader reader)
        {
            CombustibleVol e = new CombustibleVol();
            e.Clave = (reader[0] is DBNull) ? 0 : reader.GetInt32(0);
            e.Nombre = (reader[1] is DBNull) ? string.Empty : reader.GetString(1);
            e.ClavePEMEX = (reader[2] is DBNull) ? string.Empty : reader.GetString(2);
            e.ConProductoPrecio = (reader[3] is DBNull) ? string.Empty : reader.GetString(3);
            return e;
        }
    }
}
