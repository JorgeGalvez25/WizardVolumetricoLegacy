using System.Diagnostics;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Persistencia.Entidades;

namespace iGasWizardVolumetricos.Persistencia.Logica
{
    public class ImpresoraVolPersistencia
    {
        public string Guardar(ImpresoraVol e)
        {
            string ex = string.Empty;

            if (e.Impresora == "Ninguna")
            {
                e = new ImpresoraVol();
                e.EjecutarNetUse = false;
                e.Impresora = "Impresora de texto";
                e.Puerto = @"C:\Imagenco\Tmp\Ticket.txt";
            }

            if (e.EjecutarNetUse)
            {
                ex = ejecutarNetUse(e);
                if (!string.IsNullOrEmpty(ex))
                {
                    return ex;
                }
            }

            UbicarBD db = WorkItem.Objetos<UbicarBD>.Get();
            Conexion.Conexion cxn = new iGasWizardVolumetricos.Persistencia.Logica.Conexion.Conexion(db.RutaBD);
            ex = cxn.ConectarBDConsola((c) =>
            {
                c.CommandText = "DELETE FROM DPVGIMPR";
                c.ExecuteNonQuery();

                c.CommandText = "INSERT INTO DPVGIMPR(CLAVE,NOMBRE,RUTA) VALUES(@CLAVE,@NOMBRE,@RUTA)";
                c.Parameters.Add("@CLAVE", 1);
                c.Parameters.Add("@NOMBRE", e.Impresora);
                c.Parameters.Add("@RUTA", e.Puerto);
                c.ExecuteNonQuery();

                c.CommandText = "UPDATE DPVGBOMB SET IMPRESORA=@IMPRESORA";
                c.Parameters.Clear();
                c.Parameters.Add("@IMPRESORA", 1);
                c.ExecuteNonQuery();

                c.CommandText = "UPDATE DPVGCONF SET IMPRESORATICKETS=@IMPRESORATICKETS";
                c.Parameters.Clear();
                c.Parameters.Add("@IMPRESORATICKETS", e.Puerto);
                c.ExecuteNonQuery();

            });

            return ex;
        }

        private string ejecutarNetUse(ImpresoraVol e)
        {
            string arg = string.Format(@"USE {0}{1} ""\\{2}\{3}"" /PERSISTENT:YES", e.TipoPuerto, e.Puerto, e.IP, e.Impresora);
            Process proceso = new Process();
            try
            {
                proceso.StartInfo = new ProcessStartInfo("NET", arg);
                proceso.StartInfo.CreateNoWindow = true;
                proceso.StartInfo.UseShellExecute = false;
                proceso.StartInfo.RedirectStandardOutput = true;
                proceso.StartInfo.RedirectStandardInput = true;
                proceso.StartInfo.RedirectStandardError = true;
                proceso.Start();
                proceso.WaitForExit();

                string output = proceso.StandardOutput.ReadToEnd();
                string result = proceso.StandardError.ReadToEnd();

                if (output.ToUpper().Contains("ERROR"))
                {
                    return output;
                }

                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }
            }
            finally
            {
                proceso.Close();
                proceso.Dispose();
                proceso = null;
            }

            return string.Empty;
        }
    }
}
