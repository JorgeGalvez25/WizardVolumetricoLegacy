using System;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Persistencia.Entidades;
using _c = iGasWizardVolumetricos.Generales.Constantes;
using _u = iGasWizardVolumetricos.Generales.Utilerias;

namespace iGasWizardVolumetricos.Persistencia.Logica
{
    public class MarcaDispensarioVolPersistencia
    {
        public string Guardar(MarcaDispensarioVol e)
        {
            string ex = null;
            UbicarBD db = WorkItem.Objetos<UbicarBD>.Get();
            Conexion.Conexion cxn = new iGasWizardVolumetricos.Persistencia.Logica.Conexion.Conexion(db.RutaBD);
            ex = cxn.ConectarBDConsola((c) =>
            {
                c.CommandText = "UPDATE DPVGESTS SET TIPODISPENSARIO=@TIPODISPENSARIO";
                c.Parameters.Add("@TIPODISPENSARIO", e.Marca);
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

        public void Probar(MarcaDispensarioVol e)
        {
            int marca = 0;

            switch (e.Marca)
            {
                case 1:
                case 4:
                case 6:
                case 9: // Agregar Wayne 2w
                    marca = 0; break;
                case 2: marca = 1; break;
                case 3: marca = 5; break;
                case 5:
                default: _u.Informacion(_c.MENSAJES.NO_SE_PUEDE_PROBAR_MARCA_DISPENSARIO); return;
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
