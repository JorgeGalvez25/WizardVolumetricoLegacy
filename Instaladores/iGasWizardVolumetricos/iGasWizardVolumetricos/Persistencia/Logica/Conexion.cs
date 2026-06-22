using System;
using System.Data;
using System.IO;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Isql;
using iGasWizardVolumetricos.Generales;

namespace iGasWizardVolumetricos.Persistencia.Logica.Conexion
{
    public class Conexion : IDisposable
    {
        public static void LimpiarConexiones()
        {
            FbConnection.ClearAllPools();
        }
        private string StrConexion
        {
            get
            {
                FbConnectionStringBuilder sb = new FbConnectionStringBuilder();
                string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"fbclient.dll");
                sb.Database = string.IsNullOrEmpty(this.RutaDB) ? @"C:\ImagenCo\DBI\Consola\GasConsola.FDB" : this.RutaDB;
                sb.Port = 3050;
                sb.UserID = "SYSDBA";
                sb.Password = "masterkey";
                sb.DataSource = "127.0.0.1";
                sb.Pooling = true;
                {
                    sb.Dialect = 1;
                    sb.Charset = "NONE";
                    sb.ServerType = FbServerType.Default;
                    sb.MinPoolSize = 1;
                    sb.MaxPoolSize = 10;
                    sb.ClientLibrary = path;
                }
                if (this.EsPrueba)
                {
                    sb.ConnectionTimeout = 5;
                }
                return sb.ToString();
            }
        }
        private string RutaDB { get; set; }

        public bool EsPrueba { get; set; }

        public Conexion(string rutaDB)
        {
            this.RutaDB = rutaDB;
            this.EsPrueba = false;
        }

        public string ConectarBDConsola(Action<FbCommand> callbak)
        {
            string msjError = string.Empty;
            this.ConectarBDConsola(callbak, ref msjError);
            return msjError;
        }
        private bool ConectarBDConsola(Action<FbCommand> callbak, ref string err)
        {
            bool result = false;
            using (FbConnection conn = new FbConnection(this.StrConexion))
            {
                try
                {
                    if (conn.State != ConnectionState.Open) { conn.Open(); }

                    using (FbCommand comm = conn.CreateCommand())
                    {
                        try
                        {
                            comm.Transaction = conn.BeginTransaction();
                            comm.Parameters.Clear();

                            try
                            {
                                callbak(comm);
                                comm.Transaction.Commit();
                                result = true;
                            }
                            catch (Exception e)
                            {
                                comm.Transaction.Rollback();
                                err = getExceptionLog(e);
                            }
                        }
                        finally
                        {
                            this.EsPrueba = false;
                            if (conn.State != ConnectionState.Closed) { conn.Close(); }
                        }
                    }
                }
                catch (Exception e)
                {
                    err += getExceptionLog(e);
                }
            }
            return result;
        }

        public string ExcecutePathScript(string fPath)
        {
            string msj = string.Empty;
            this.ExcecutePathScript(fPath, ref msj);
            return msj;
        }
        public bool ExcecutePathScript(string fPath, ref string msj)
        {
            bool result = false;
            bool flgError = false;
            StringBuilder sb = new StringBuilder();

            DirectoryInfo d = new DirectoryInfo(fPath);
            if (!d.Exists)
            {
                sb.AppendLine(string.Format("No existe la ruta '{0}'", fPath));
            }
            else
            {
                // Obitiene todos los archivos SQL
                var files = d.GetFiles("*.sql");

                // Si no existen archivos
                if (files.Length <= 0)
                {
                    sb.AppendLine("No existen archivos '.SQL'");
                }
                else
                {
                    using (FbConnection conn = new FbConnection(this.StrConexion))
                    {
                        try
                        {
                            conn.Open();
                            FbScript script = null;
                            FbBatchExecution execute = new FbBatchExecution(conn);

                            for (int i = 0; i < files.Length; i++)
                            {
                                try
                                {
                                    script = new FbScript(files[i].FullName);
                                    {
                                        script.Parse();

                                        execute.SqlStatements.Clear();
                                        execute.SqlStatements.AddRange(script.Results);
                                        execute.Execute();
                                    }
                                    script.Results.Clear();
                                }
                                catch (Exception e)
                                {
                                    flgError = true;
                                    sb.AppendLine(string.Format("[{0}]", files[i].Name))
                                      .AppendLine(Utilerias.LeerExcepcion(e))
                                      .AppendLine();
                                }
                            }
                        }
                        finally
                        {
                            if (conn.State != ConnectionState.Closed) { conn.Close(); }
                        }
                    }

                    result = !flgError;
                }
            }

            msj = sb.ToString().Clone().ToString();
            {
                sb.Remove(0, sb.Length);
                sb = null;
            }
            return result;
        }

        private string getExceptionLog(Exception e)
        {
            //if (e == null) { return string.Empty; }
            //StringBuilder sb = new StringBuilder(e.Message);
            //Exception tmp = e.InnerException;
            //while (tmp != null)
            //{
            //    sb.AppendLine(e.Message);
            //    tmp = tmp.InnerException;
            //}
            //return sb.ToString();
            return iGasWizardVolumetricos.Generales.Utilerias.LeerExcepcion(e);
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Conexion()
        {
            this.Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                FbConnection.ClearAllPools();
                this.EsPrueba = false;
                this.RutaDB = null;

                GC.Collect();
                GC.WaitForFullGCComplete();
            }
        }

        #endregion
    }
}
