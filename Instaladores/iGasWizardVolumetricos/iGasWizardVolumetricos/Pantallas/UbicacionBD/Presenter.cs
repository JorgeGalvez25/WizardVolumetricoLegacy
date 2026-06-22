using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Persistencia.Entidades;
using iGasWizardVolumetricos.Persistencia.Logica;
using iGasWizardVolumetricos.Persistencia.Logica.Conexion;

namespace iGasWizardVolumetricos.Pantallas.UbicacionBD
{
    public class Presenter
    {
        public bool OperacionsIO(string path, ref string msj)
        {
            bool result = true;
            try
            {
                string _path = Path.GetDirectoryName(path);
                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }

                // Crea o copia la BD de consola
                if (File.Exists(path))
                {
                    this.LiberarBD(path);
                    File.Delete(path);
                }
                // Copia la BD a la nueva ruta
                File.Copy(Constantes.RutaGasConsola, path);
            }
            catch (Exception e)
            {
                result = false;
                msj = Utilerias.LeerExcepcion(e);
            }

            return result;
        }

        private void LiberarBD(string path)
        {
            UbicarBDPersistencia servicio = new UbicarBDPersistencia();
            {
                servicio.LiberarConexiones();
            }

            Application.DoEvents();
            int cont = 0;

            while (true)
            {
                ++cont;

                try
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None, 100))
                    {
                        fs.ReadByte();
                        break;
                    }
                }
                catch (Exception e)
                {
                    if (cont > 10) { throw e; }

                    System.Threading.Thread.Sleep(500);
                }
            }
        }

        public bool OperacionesBDE(string alias, string path, ref string msj)
        {
            if (Utilerias.ManejaAlias("Existe", alias, path) == 1)
            {
                if (Utilerias.ManejaAlias("Actualizar", alias, path) == 0)
                {
                    msj = string.Format("No fue posible actualizar el alias {0} con la ruta {1}", alias, path);
                    return false;
                }
            }
            else
            {
                if (Utilerias.ManejaAlias("Crear", alias, path) == 0)
                {
                    msj = string.Format("No fue posible crear el alias {0} con la ruta {1}", alias, path);
                    return false;
                }
            }

            return true;
        }

        public bool ExisteAlias(string alias)
        {
            return (Utilerias.ManejaAlias("Existe", alias, string.Empty) == 1);
        }

        public string ArrayAString<T>(T items) where T : IList
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < items.Count; i++)
            {
                sb.AppendLine(items[i].ToString());
            }
            return sb.ToString();
        }

        public bool PruebaDeConexion(UbicarBD prop, ref string msj)
        {
            if (!File.Exists(prop.RutaBD))
            {
                msj = "No fue posible crear la Base de Datos en la ruta especificada.";
                return false;
            }

            Conexion conn = null;
            {
                if (WorkItem.Objetos<Conexion>.Exist()) // Si existe una conexion 
                {
                    // Se obtiene la conexion existente
                    conn = WorkItem.Objetos<Conexion>.Get();
                    // Se liberan sus recursos
                    conn.Dispose();
                    // Se elimina del Workitem
                    WorkItem.Objetos<Conexion>.Delete(conn);
                }
                // Se crea una nueva conexion
                conn = new Conexion(prop.RutaBD);
            }
            // Se añade una conexion nueva
            WorkItem.Objetos<Conexion>.Add(conn);

            UbicarBDPersistencia servicio = new UbicarBDPersistencia();
            return servicio.Prueba(ref msj);
        }

        public bool BDValida(UbicarBD f)
        {
            if (File.Exists(f.RutaBD))
            {
                Conexion conn = null;
                {
                    if (WorkItem.Objetos<Conexion>.Exist()) // Si existe una conexion 
                    {
                        // Se obtiene la conexion existente
                        conn = WorkItem.Objetos<Conexion>.Get();
                        // Se liberan sus recursos
                        conn.Dispose();
                        // Se elimina del Workitem
                        WorkItem.Objetos<Conexion>.Delete(conn);
                    }
                }
                // Se crea una nueva conexion
                conn = new Conexion(f.RutaBD);
                // Se añade una conexion nueva
                WorkItem.Objetos<Conexion>.Add(conn);
                UbicarBDPersistencia servicio = new UbicarBDPersistencia();
                return servicio.BDValida();
            }
            return true;
        }

        public string[] BuscarYReemplazar(string[] values, string id, string value)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i].StartsWith(id))
                {
                    string[] item = values[i].Split(new char[] { '=' });
                    item[1] = value;

                    values[i] = string.Format("{0}={1}", item[0], item[1]);
                    break;
                }
            }

            return values;
        }

        #region Acceso a DLL

        //[DllImport(@"AliasBDE.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        //private static extern bool CrearAliasBDE(ref string alias, ref string ruta);

        //[DllImport(@"AliasBDE.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        //private static extern string ObtenerAliasBDE(ref string alias);

        //[DllImport(@"AliasBDE.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        //private static extern bool ExisteAliasBDE(ref string alias);

        //[DllImport(@"AliasBDE.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        //private static extern bool EliminarAliasBDE(ref string alias);

        //[DllImport(@"AliasBDE.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        //private static extern bool ActualizarAliasBDE(ref string alias, ref string items);

        #endregion
    }
}
