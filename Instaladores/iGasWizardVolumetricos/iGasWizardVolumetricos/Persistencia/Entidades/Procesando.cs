using System.Collections.Generic;

namespace iGasWizardVolumetricos.Persistencia.Entidades
{
    public enum TipoArchivo
    {
        ModificaLnk,
        ModificaArch
    }
    public class OpcionesArchivos
    {
        public string Nombre { get; set; }
        /// <summary>
        /// Permite especificar el valor o un objeto para la configuración
        /// </summary>
        public object Configuracion { get; set; }
        public string FilePath { get; set; }
        public string RutaExe { get; set; }
        public TipoArchivo Tipo { get; set; }
    }

    public class Procesando
    {
        public Procesando()
        {
            this.Nombre =
                this.Parametro =
                this.Ruta = string.Empty;
        }
        public string Ruta { get; set; }
        public string Parametro { get; set; }
        public string Nombre { get; set; }
    }

    public class DiccionarioProcesando : Dictionary<string, OpcionesArchivos>
    {
        ~DiccionarioProcesando()
        {
            if (this != null)
            {
                this.Clear();
            }
        }
    }

    public class ListaProcesando : List<Procesando>
    {
        ~ListaProcesando()
        {
            if (this != null)
            {
                this.Clear();
            }
        }
    }
}
