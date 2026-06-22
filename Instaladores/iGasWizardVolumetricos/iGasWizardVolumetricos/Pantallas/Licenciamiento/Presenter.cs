using System;
using System.Diagnostics;
using System.Globalization;
using iGasWizardVolumetricos.Extensiones;
using iGasWizardVolumetricos.Persistencia.Entidades;
using iGasWizardVolumetricos.Persistencia.Logica;

namespace iGasWizardVolumetricos.Pantallas.Licenciamiento
{
    public class Presenter
    {
        #region Delegados
        //[System.Runtime.InteropServices.DllImport("AliasBDE.dll", EntryPoint = "LicenciaValidaDLL")]//"ValidaLicencia_CVOL")]
        //public static extern string licenciaValida(ref string RazonSocial,
        //                                           ref string Sistema,
        //                                           ref string Version,
        //                                           ref string TipoLicencia,
        //                                           ref string ClaveAutor,
        //                                           ref int Usuarios,
        //                                           ref bool LicenciaTemporal,
        //                                           ref DateTime Fecha);

        //private delegate string ValidaMicroDog(ref int data);

        #endregion

        private static readonly CultureInfo cultura = CultureInfo.CreateSpecificCulture("es-MX");

        public Licencia Licencia()
        {
            EmpresaConfiguracionPersistencia servicio = new EmpresaConfiguracionPersistencia();
            var d = servicio.Obtener();
            return d == null ? new Licencia() : d.GetLicencia();
        }

        //public bool ValidaSentinel(string serie)
        //{
        //    string msj = string.Empty;
        //    return ValidaSentinel(serie, out msj);
        //}
        //public bool ValidaSentinel(string serie, out string msj)
        //{
        //    int data = 0;
        //    int.TryParse(serie, out data);

        //    using (DllManager dllSentinel = new DllManager())
        //    {
        //        dllSentinel.Load(System.IO.Path.Combine(Constantes.Root, "AliasBDE.dll"));
        //        ValidaMicroDog mthd = dllSentinel.Method<ValidaMicroDog>("ValidaSentinel");// ("MicroDogSerial_CVOL");
        //        msj = mthd(ref data);
        //    }

        //    return (msj.Equals("Valida", StringComparison.CurrentCultureIgnoreCase));
        //}

        public bool LicenciaValida(Licencia licencia, ref string msj)
        {
            string comandoLicencia = string.Format("licenciavalidacvol \"{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}\"",
                                                                        licencia.RazonSocial,
                                                                        "CVOL",//Sistema
                                                                        "3.1",//Version
                                                                        "Abierta",//TipoLicencia
                                                                        licencia.LicenciaVolumetrico,
                                                                        1,//Usuarios
                                                                        licencia.EsTemporal,//Temporal
                                                                        licencia.FechaVence.GetValueOrDefault(DateTime.MinValue).ToString("dd/MM/yyyy", cultura));

            return "Valido".Equals(proceso(comandoLicencia), StringComparison.OrdinalIgnoreCase);
        }

        public bool LicenciaInocua(Licencia licencia, ref string msj)
        {
            string comandoLicencia = string.Format("licenciavalidacvol \"{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}\"",
                                                                        licencia.RazonSocial,
                                                                        "CVOL02",//Sistema
                                                                        "3.1",//Version
                                                                        "Abierta",//TipoLicencia
                                                                        licencia.LicienciaInocua,
                                                                        1,//Usuarios
                                                                        licencia.EsTemporalInocuo,//Temporal
                                                                        licencia.FechaVenceInocua.GetValueOrDefault(DateTime.MinValue).ToString("dd/MM/yyyy", cultura));

            return "Valido".Equals(proceso(comandoLicencia));
        }

        public bool LicenciaControlVersiones(Licencia licencia, ref string msj)
        {
            string comandoLicencia = string.Format("licenciacongruente \"{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}\"",
                                                                        licencia.RazonSocial + licencia.NoEstacion.ToString(),
                                                                        "CVAC",//Sistema
                                                                        "3.1",//Version
                                                                        "Abierta",//TipoLicencia
                                                                        licencia.LicenciaControlVersiones,
                                                                        1,//Usuarios
                                                                        true,//Temporal
                                                                        licencia.FechaVenceControlVersiones.GetValueOrDefault(DateTime.MinValue).ToString("dd/MM/yyyy", cultura));

            return "1".Equals(proceso(comandoLicencia));
        }

        private string proceso(string argument)
        {
            Process p = new Process();

            try
            {
                p.StartInfo.FileName = AppDomain.CurrentDomain.SetupInformation.ApplicationBase.CombinePaths('\\', @"\LibsDelphi.exe");
                p.StartInfo.Arguments = argument;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;

                p.Start();
                p.WaitForExit();

                return p.StandardOutput.ReadToEnd();
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                p.Dispose();
                p = null;
            }
        }
    }
}
