using System;
using System.Data;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Persistencia.Entidades;

namespace iGasWizardVolumetricos.Persistencia.Logica
{
    public class TipoInterfazDispensarioVolPersistencia
    {
        public ListaTipoInterfazDispensarioVol Obtener()
        {
            Exception ex = null;
            ListaTipoInterfazDispensarioVol r = new ListaTipoInterfazDispensarioVol();
            UbicarBD db = WorkItem.Objetos<UbicarBD>.Get();
            Conexion.Conexion cxn = new iGasWizardVolumetricos.Persistencia.Logica.Conexion.Conexion(db.RutaBD);
            cxn.ConectarBDConsola((c) =>
            {
                try
                {
                    c.CommandText = "SELECT MDIS, MINT, NOMBRE FROM DPVGMINT";
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

        private TipoInterfazDispensarioVol readerToEntidad(IDataReader reader)
        {
            TipoInterfazDispensarioVol e = new TipoInterfazDispensarioVol();
            e.Dispensario = (reader[0] is DBNull) ? 0 : reader.GetInt32(0);
            e.Clave = (reader[1] is DBNull) ? 0 : reader.GetInt32(1);
            e.Nombre = (reader[2] is DBNull) ? string.Empty : reader.GetString(2);
            return e;
        }
    }
}
