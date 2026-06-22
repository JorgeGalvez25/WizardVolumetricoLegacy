using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;

namespace iGasVolumetricosInstall
{
    [RunInstaller(true)]
    public partial class Instalador : Installer
    {
        public Instalador()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);

            FileInfo f = new FileInfo(this.Context.Parameters["assemblypath"]);
            string fileName = Path.Combine(f.DirectoryName, "iGasWizardVolumetricos.exe");

            Process p = new Process();
            try
            {
                p.StartInfo = new ProcessStartInfo(fileName, "{88FC5A96-EABA-4FF8-96B3-428D920A744C}");
                p.Start();
            }
            finally
            {
                p.Close();
                p.Dispose();
                p = null;
            }
        }
    }
}
