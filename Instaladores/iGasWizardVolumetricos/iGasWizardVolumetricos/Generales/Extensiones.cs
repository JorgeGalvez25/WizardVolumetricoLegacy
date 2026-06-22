namespace iGasWizardVolumetricos.Extensiones
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public static class FormExtender
    {
        /// <summary>
        /// Metodo con el que se evita excepciones con el CrossThreading de los controles WinForm
        /// </summary>
        /// <param name="this">Es el Control afectado NO SE UTILIZA EL PARAMETRO SE PASA AUTOMATIVAMENTE</param>
        /// <param name="code">Es el delegado que realizara la acción a realizar</param>
        /// <example> this.txtNombre.SetSafeThread(delegate { this.txtNombre.Text = "Fulanito"; }); </example>
        public static void BeginSafe(this Control @this, MethodInvoker code)
        {
            if (@this.InvokeRequired)
            {
                @this.BeginInvoke(code);
                return;
            }
            code.Invoke();
        }
    }

    public static class ObjectExtender
    {
        public static string CombinePaths(this string current, char separator, params string[] path)
        {
            char[] splt = new char[] { separator };
            string[] aux = current.Split(splt, StringSplitOptions.RemoveEmptyEntries);
            List<string> paths = new List<string>(aux.Length + path.Length);

            for (int i = 0; i < aux.Length; i++)
            {
                paths.AddRange(aux[i].Split(splt, System.StringSplitOptions.RemoveEmptyEntries));
            }

            for (int i = 0; i < path.Length; i++)
            {
                paths.AddRange(path[i].Split(splt, System.StringSplitOptions.RemoveEmptyEntries));
            }

            int count = 0;
            string doubleSeparator = string.Format("{0}{0}", separator);
            Regex reg = new Regex("^(http(s)?|ftp(s)?|file)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return paths.Aggregate((x, y) => { return x + ((count++ == 0 && reg.IsMatch(x)) ? doubleSeparator : separator.ToString()) + y; });
        }
    }
}
