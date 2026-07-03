using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using _c = iGasWizardVolumetricos.Generales.Constantes;

namespace iGasWizardVolumetricos.Generales
{
    public static class Utilerias
    {
        #region Metodos

        public static void ComPortIgas(string parametros)
        {
            Process proceso = new Process();
            try
            {
                proceso.StartInfo = new ProcessStartInfo(_c.RutaComPort, parametros);
                proceso.Start();
                proceso.WaitForExit();
            }
            finally
            {
                proceso.Close();
                proceso.Dispose();
                proceso = null;
            }
        }

        public static int Arquitectura()
        {
            Process proceso = new Process();

            try
            {
                proceso.StartInfo = new ProcessStartInfo();
                proceso.StartInfo.FileName = Path.Combine(_c.Root, @"bits.exe"); ;
                proceso.StartInfo.CreateNoWindow = true;
                proceso.StartInfo.UseShellExecute = false;
                proceso.StartInfo.RedirectStandardOutput = true;
                proceso.Start();
                proceso.WaitForExit();

                string output = proceso.StandardOutput.ReadToEnd();

                int bits = 32;
                int.TryParse(output, out bits);
                return bits;
            }
            catch
            {
                return 32;
            }
            finally
            {
                proceso.Close();
                proceso.Dispose();
                proceso = null;
            }
        }

        public static int ManejaAlias(string opcion, string alias, string ruta)
        {
            Process proceso = new Process();

            try
            {
                proceso.StartInfo = new ProcessStartInfo();
                proceso.StartInfo.FileName = Path.Combine(_c.Root, @"CmdAlias.exe");
                proceso.StartInfo.Arguments = string.Format("{0} {1} {2}", opcion, alias, ruta).Trim();
                proceso.StartInfo.CreateNoWindow = true;
                proceso.StartInfo.UseShellExecute = false;
                proceso.StartInfo.RedirectStandardOutput = true;
                proceso.Start();
                proceso.WaitForExit();

                string output = proceso.StandardOutput.ReadToEnd();

                int bits = 32;
                int.TryParse(output, out bits);
                return bits;
            }
            catch
            {
                return 32;
            }
            finally
            {
                proceso.Close();
                proceso.Dispose();
                proceso = null;
            }
        }

        public static bool Confirmacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public static void Error(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Error(Exception ex)
        {
            MessageBox.Show(LeerExcepcion(ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Informacion(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static string LeerExcepcion(Exception ex)
        {
            StringBuilder text = new StringBuilder();

            text.AppendLine("[Exception.Message]")
                .AppendLine(ex.Message)
                .AppendLine("[Exception.StackTrace]")
                .AppendLine(ex.StackTrace);

            while (ex.InnerException != null)
            {
                text.AppendLine("[InnerException.Message]")
                    .AppendLine(ex.InnerException.Message)
                    .AppendLine("[InnerException.StackTrace]")
                    .AppendLine(ex.InnerException.StackTrace);

                ex = ex.InnerException;
            }

            return text.ToString();
        }

        public static void CancelarInstalacion()
        {
            if (!string.IsNullOrEmpty(_c.CodigoProducto))
            {
                Process p = new Process();
                try
                {
                    p.StartInfo = new ProcessStartInfo("msiexec", string.Concat("/passive /x ", _c.CodigoProducto));
                    p.Start();
                    p.WaitForExit(60000);
                }
                finally
                {
                    p.Close();
                    p.Dispose();
                    p = null;
                }
            }
        }

        public static string CombinarRutas(params string[] rutas)
        {
            return Utilerias.CombinarRutas('\\', rutas);
        }

        public static string CombinarRutas(char separator, params string[] rutas)
        {
            List<string> paths = new List<string>();
            char[] splt = new char[] { separator };

            for (int i = 0; i < rutas.Length; i++)
            {
                paths.AddRange(rutas[i].Split(splt, System.StringSplitOptions.RemoveEmptyEntries));
            }

            return paths.Aggregate((x, y) => { return x + separator.ToString() + y; });
        }

        public static bool InstalarServicioWindows(string rutaExe, string argumentos)
        {
            Process proceso = new Process();
            try
            {
                proceso.StartInfo = new ProcessStartInfo();
                proceso.StartInfo.FileName = rutaExe;
                proceso.StartInfo.Arguments = argumentos;
                proceso.StartInfo.CreateNoWindow = true;
                proceso.StartInfo.UseShellExecute = false;
                proceso.StartInfo.Verb = "runas"; // Requiere elevación de privilegios
                proceso.Start();
                proceso.WaitForExit();

                return proceso.ExitCode == 0;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                proceso.Close();
                proceso.Dispose();
            }
        }

        #endregion
    }

    /// <summary>
    /// Administra una dll a la vez, la carga y la descarga de memoria
    /// </summary>
    public class DllManager : IDisposable
    {
        public IntPtr Handle { get; private set; }

        public string FileName { get; private set; }

        #region DllImport Kernel32

        [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        private static extern bool FreeLibrary(IntPtr hModule);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        #endregion

        public void Unload()
        {
            if (this.Handle != IntPtr.Zero)
            {
                Application.DoEvents();
                try { FreeLibrary(this.Handle); }
                catch { }
            }
        }

        public IntPtr Load(string fName)
        {
            //Load
            this.FileName = fName;
            this.Handle = LoadLibrary(this.FileName);
            if (this.Handle == IntPtr.Zero)
            {
                int errorCode = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
                throw new Exception(string.Format("Failed to load library (ErrorCode: {0})", errorCode));
            }

            return this.Handle;
        }

        public T Method<T>(string fnName)
        {
            IntPtr funcaddr = GetProcAddress(this.Handle, fnName);
            return (T)System.Runtime.InteropServices.Marshal.GetDelegateForFunctionPointer(funcaddr, typeof(T)).Clone();
        }

        #region IDisposable Members

        ~DllManager()
        {
            this.Dispose(false);
            GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Unload();

                if (!string.IsNullOrEmpty(this.FileName))
                {
                    this.FileName = null;
                }

                if (this.Handle != IntPtr.Zero)
                {
                    this.Handle = IntPtr.Zero;
                }
            }
        }

        #endregion
    }

    public class IniItem
    {
        public string Section { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class IniParser
    {
        private string iniFilePath;
        private Hashtable keyPairs = new Hashtable();

        private struct SectionPair
        {
            public string Section;
            public string Key;
        }

        /// <summary>
        /// Opens the INI file at the given path and enumerates the values in the IniParser.
        /// </summary>
        /// <param name="iniPath">Full path to INI file.</param>
        public IniParser(string iniPath)
        {
            this.iniFilePath = iniPath;

            if (!File.Exists(iniPath)) { throw new FileNotFoundException(string.Format("La ruta {0} no existe", iniPath)); }

            using (TextReader iniFile = new StreamReader(iniPath))
            {
                string[] keyPair = null;
                string currentRoot = null;
                SectionPair sectionPair;
                string value = null;

                try
                {
                    string strLine = iniFile.ReadLine();

                    while (strLine != null)
                    {
                        strLine = strLine.Trim().ToUpper();

                        if (!string.IsNullOrEmpty(strLine))
                        {
                            if (strLine.StartsWith("[") && strLine.EndsWith("]"))
                            {
                                currentRoot = strLine.Substring(1, strLine.Length - 2);
                            }
                            else
                            {
                                keyPair = strLine.Split(new char[] { '=' }, 2);

                                if (currentRoot == null)
                                    currentRoot = "ROOT";

                                sectionPair.Section = currentRoot;
                                sectionPair.Key = keyPair[0];

                                if (keyPair.Length > 1)
                                    value = keyPair[1];

                                this.keyPairs.Add(sectionPair, value);
                            }
                        }

                        strLine = iniFile.ReadLine();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (iniFile != null)
                        iniFile.Close();

                    Array.Clear(keyPair, 0, keyPair.Length);
                    Array.Resize(ref keyPair, 0);
                    keyPair = null;
                }
            }

        }

        /// <summary>
        /// Returns the value for the given section, key pair.
        /// </summary>
        /// <param name="sectionName">Section name.</param>
        /// <param name="settingName">Key name.</param>
        public string GetSetting(string sectionName, string settingName)
        {
            SectionPair sectionPair;
            sectionPair.Section = sectionName.ToUpper();
            sectionPair.Key = settingName.ToUpper();

            return (string)this.keyPairs[sectionPair];
        }

        /// <summary>
        /// Enumerates all lines for given section.
        /// </summary>
        /// <param name="sectionName">Section to enum.</param>
        public string[] EnumSection(string sectionName)
        {
            ArrayList tmpArray = new ArrayList();

            foreach (SectionPair pair in keyPairs.Keys)
            {
                if (pair.Section == sectionName.ToUpper())
                    tmpArray.Add(pair.Key);
            }

            return (string[])tmpArray.ToArray(typeof(string));
        }

        /// <summary>
        /// Adds or replaces a setting to the table to be saved.
        /// </summary>
        /// <param name="sectionName">Section to add under.</param>
        /// <param name="settingName">Key name to add.</param>
        /// <param name="settingValue">Value of key.</param>
        public void AddSetting(string sectionName, string settingName, string settingValue)
        {
            SectionPair sectionPair;
            sectionPair.Section = sectionName.ToUpper();
            sectionPair.Key = settingName.ToUpper();

            if (this.keyPairs.ContainsKey(sectionPair))
                this.keyPairs.Remove(sectionPair);

            this.keyPairs.Add(sectionPair, settingValue);
        }

        /// <summary>
        /// Adds or replaces a setting to the table to be saved with a null value.
        /// </summary>
        /// <param name="sectionName">Section to add under.</param>
        /// <param name="settingName">Key name to add.</param>
        public void AddSetting(string sectionName, string settingName)
        {
            this.AddSetting(sectionName, settingName, null);
        }

        /// <summary>
        /// Remove a setting.
        /// </summary>
        /// <param name="sectionName">Section to add under.</param>
        /// <param name="settingName">Key name to add.</param>
        public void DeleteSetting(string sectionName, string settingName)
        {
            SectionPair sectionPair;
            sectionPair.Section = sectionName.ToUpper();
            sectionPair.Key = settingName.ToUpper();

            if (this.keyPairs.ContainsKey(sectionPair))
                this.keyPairs.Remove(sectionPair);
        }

        /// <summary>
        /// Edit a setting.
        /// </summary>
        /// <param name="sectionName">Section to add under.</param>
        /// <param name="settingName">Key name to add.</param>
        /// <param name="settingValue">Value of key.</param>
        public void EditSetting(string sectionName, string settingName, string value)
        {
            this.DeleteSetting(sectionName, settingName);
            this.AddSetting(sectionName, settingName, value);
        }

        /// <summary>
        /// Save settings to new file.
        /// </summary>
        /// <param name="newFilePath">New file path.</param>
        public void SaveSettings(string newFilePath)
        {
            ArrayList sections = new ArrayList();
            string tmpValue = string.Empty;
            StringBuilder strToSave = new StringBuilder();
            try
            {
                foreach (SectionPair sectionPair in keyPairs.Keys)
                {
                    if (!sections.Contains(sectionPair.Section))
                        sections.Add(sectionPair.Section);
                }

                foreach (string section in sections)
                {
                    strToSave.AppendLine(string.Format("[{0}]", section));

                    foreach (SectionPair sectionPair in this.keyPairs.Keys)
                    {
                        if (sectionPair.Section == section)
                        {
                            tmpValue = (String)this.keyPairs[sectionPair];

                            if (tmpValue != null)
                                tmpValue = string.Format("={0}", tmpValue);

                            strToSave.AppendLine(string.Format("{0}{1}", sectionPair.Key, tmpValue));
                        }
                    }

                    strToSave.AppendLine();
                }

                try
                {
                    using (TextWriter tw = new StreamWriter(newFilePath))
                    {
                        tw.Write(strToSave.ToString());
                        tw.Flush();
                        tw.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            finally
            {
                // Control de uso de memoria
                sections.Clear();
                tmpValue = null;
                strToSave.Remove(0, strToSave.Length);
                strToSave = null;
            }
        }

        /// <summary>
        /// Save settings back to ini file.
        /// </summary>
        public void SaveSettings()
        {
            this.SaveSettings(this.iniFilePath);
        }
    }
}
