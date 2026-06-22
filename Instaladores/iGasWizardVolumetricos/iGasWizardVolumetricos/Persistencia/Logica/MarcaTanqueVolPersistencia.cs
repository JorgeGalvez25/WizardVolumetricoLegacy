using System;
using System.Collections.Generic;
using System.Data;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Persistencia.Entidades;
using _c = iGasWizardVolumetricos.Generales.Constantes;
using _u = iGasWizardVolumetricos.Generales.Utilerias;

namespace iGasWizardVolumetricos.Persistencia.Logica
{
    public class MarcaTanqueVolPersistencia
    {
        public Dictionary<int, string> ObtenerMarcas()
        {
            Exception ex = null;
            Dictionary<int, string> r = new Dictionary<int, string>();
            UbicarBD db = WorkItem.Objetos<UbicarBD>.Get();
            Conexion.Conexion cxn = new iGasWizardVolumetricos.Persistencia.Logica.Conexion.Conexion(db.RutaBD);
            cxn.ConectarBDConsola((c) =>
            {
                try
                {
                    c.CommandText = "SELECT CLAVE,NOMBRE FROM DPVGMTAN";
                    using (IDataReader reader = c.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int clave = (reader[0] is DBNull) ? 0 : reader.GetInt32(0);
                            string nombre = (reader[1] is DBNull) ? string.Empty : reader.GetString(1);
                            r.Add(clave, nombre);
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

        public string Guardar(MarcaTanqueVol e)
        {
            string ex = null;
            UbicarBD db = WorkItem.Objetos<UbicarBD>.Get();
            Conexion.Conexion cxn = new iGasWizardVolumetricos.Persistencia.Logica.Conexion.Conexion(db.RutaBD);
            ex = cxn.ConectarBDConsola((c) =>
                {
                    c.CommandText = "UPDATE DPVGESTS SET TIPOTANQUES=@TIPOTANQUES";
                    c.Parameters.Add("@TIPOTANQUES", e.Marca);
                    c.ExecuteNonQuery();

                    c.CommandText = "SELECT COUNT(*) FROM DPVGPUER WHERE CLAVE=@CLAVE";
                    c.Parameters.Clear();
                    c.Parameters.Add("@CLAVE", e.Clave);
                    bool existe = Convert.ToInt32(c.ExecuteScalar()) == 1;

                    if (existe)
                    {
                        c.CommandText = "UPDATE DPVGPUER" +
                                        " SET NUMEROPUERTO=@NUMEROPUERTO" +
                                        ", VELOCIDAD=@VELOCIDAD" +
                                        ", PARIDAD=@PARIDAD" +
                                        ", BITSDATOS=@BITSDATOS" +
                                        ", BITSPARO=@BITSPARO" +
                                        " WHERE CLAVE=@CLAVE";
                    }
                    else
                    {
                        c.CommandText = "INSERT INTO DPVGPUER" +
                                        " (CLAVE" +
                                        " ,NUMEROPUERTO" +
                                        " ,VELOCIDAD" +
                                        " ,PARIDAD" +
                                        " ,BITSDATOS" +
                                        " ,BITSPARO)" +
                                        " VALUES" +
                                        " (@CLAVE" +
                                        " ,@NUMEROPUERTO" +
                                        " ,@VELOCIDAD" +
                                        " ,@PARIDAD" +
                                        " ,@BITSDATOS" +
                                        " ,@BITSPARO)";
                    }
                    c.Parameters.Clear();
                    c.Parameters.Add("@CLAVE", e.Clave);
                    c.Parameters.Add("@NUMEROPUERTO", e.Puerto);
                    c.Parameters.Add("@VELOCIDAD", e.Velocidad);
                    c.Parameters.Add("@PARIDAD", e.Paridad);
                    c.Parameters.Add("@BITSDATOS", e.BitsDatos);
                    c.Parameters.Add("@BITSPARO", e.BitsParada);
                    c.ExecuteNonQuery();
                });
            return ex;
        }

        internal void Probar(MarcaTanqueVol e)
        {
            int marca = 0;

            switch (e.Marca)
            {
                case 1: marca = 2; break;
                case 2:
                case 3: marca = 3; break;
                case 5: marca = 4; break;
                case 6: marca = 6; break;
                case 4:
                default: _u.Informacion(_c.MENSAJES.NO_SE_PUEDE_PROBAR_MARCA_TANQUE); return;
            }

            string parametros = string.Format("{0};{1};{2};{3};{4} {5}", e.Puerto, e.Velocidad, paridad(e.Paridad), e.BitsDatos, e.BitsParada, marca);

            _u.ComPortIgas(parametros);
        }

        private object paridad(string valor)
        {
            switch (valor)
            {
                case "Ninguna": return "N";
                case "Impar": return "O";
                case "Par": return "E";
                default: return string.Empty;
            }
        }
    }
}
