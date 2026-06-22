using System.Collections.Generic;
using System.Drawing;
using iGasWizardVolumetricos.Properties;

namespace iGasWizardVolumetricos.Persistencia.Entidades
{
    public class Requerimiento
    {
        public Requerimiento()
        {
            this.Descripcion = string.Empty;
            this.Icono = Resources.delete;
            this.Instalado = false;
        }

        public Requerimiento(string descripcion)
        {
            this.Descripcion = descripcion;
            this.Icono = Resources.delete;
            this.Instalado = false;
        }

        public string Descripcion { get; set; }
        public Image Icono { get; set; }
        public bool Instalado { get; set; }
    }

    public class ListaRequerimiento : List<Requerimiento>
    {
        public ListaRequerimiento() : base() { }

        public ListaRequerimiento(IEnumerable<Requerimiento> collection) : base(collection) { }

        public bool SonValidos { get { return !this.Exists(i => !i.Instalado); } }
    }
}
