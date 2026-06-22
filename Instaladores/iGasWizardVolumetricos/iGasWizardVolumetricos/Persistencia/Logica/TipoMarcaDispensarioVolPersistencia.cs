using System;
using System.Data;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Persistencia.Entidades;

namespace iGasWizardVolumetricos.Persistencia.Logica
{
    public class TipoMarcaDispensarioVolPersistencia
    {
        public ListaTipoMarcaDispensarioVol Obtener()
        {
            Exception ex = null;
            ListaTipoMarcaDispensarioVol r = new ListaTipoMarcaDispensarioVol();
            UbicarBD db = WorkItem.Objetos<UbicarBD>.Get();
            Conexion.Conexion cxn = new iGasWizardVolumetricos.Persistencia.Logica.Conexion.Conexion(db.RutaBD);
            cxn.ConectarBDConsola((c) =>
            {
                try
                {
                    c.CommandText = "SELECT CLAVE,NOMBRE FROM DPVGMDIS";
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

        private TipoMarcaDispensarioVol readerToEntidad(IDataReader reader)
        {
            TipoMarcaDispensarioVol e = new TipoMarcaDispensarioVol();
            e.Clave = (reader[0] is DBNull) ? 0 : reader.GetInt32(0);
            e.Nombre = (reader[1] is DBNull) ? string.Empty : reader.GetString(1);
            return e;
        }
    }
}
