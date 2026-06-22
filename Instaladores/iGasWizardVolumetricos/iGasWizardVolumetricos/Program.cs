using System;
using System.Windows.Forms;
using _c = iGasWizardVolumetricos.Generales.Constantes;

namespace iGasWizardVolumetricos
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                _c.CodigoProducto = args[0];
            }

            _c.Root = Application.StartupPath;

            Application.CurrentCulture = new System.Globalization.CultureInfo("es-MX");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Wizard());
        }
    }
}
