using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;

namespace iGasWizardVolumetricos.Generales
{
    public static class WorkItem
    {
        private static XtraUserControl[] pantallas = new XtraUserControl[]
            {
                new Pantallas.Requerimientos.PRequerimientos(),
                new Pantallas.UbicacionBD.PUbicarBD(),
                new Pantallas.Procesando.PProcesando(),
                new Pantallas.Licenciamiento.PLicenciamiento(),
                new Pantallas.DatosDeEmpresa.PDatosEmpresa(),
                new Pantallas.MarcaTanque.PMarcaTanque(),
                new Pantallas.Tanques.PTanques(),
                new Pantallas.MarcaDispensario.PMarcaDispensario(),
                new Pantallas.Wayne.PWayne(),                
                new Pantallas.Gilbarco.PGilbarco(),
                new Pantallas.Islas.PIslas(),
                new Pantallas.Dispensarios.PDispensarios(),
                new Pantallas.PosicionesCarga.PPosicionesCarga(),
                new Pantallas.Mangueras.PMangueras(),
                new Pantallas.Impresora.PImpresora(),
                new Pantallas.TurnosAdministrativos.PTurnosAdministrativos()
            };

        public static XtraUserControl[] Pantallas
        {
            get
            {
                return pantallas;
            }
        }

        public static InstallStatus Status { get; set; }

        #region Objetos

        public class Objetos<T> where T : class
        {
            private static Dictionary<Type, T> objetos = new Dictionary<Type, T>();

            public static void Add(T valor)
            {
                if (objetos.ContainsKey(typeof(T)))
                {
                    objetos.Remove(typeof(T));
                }

                objetos.Add(typeof(T), valor);
            }

            public static void Delete(T valor)
            {
                if (objetos.ContainsKey(typeof(T)))
                {
                    objetos.Remove(typeof(T));
                }
            }

            public static T Get()
            {
                if (objetos.ContainsKey(typeof(T)))
                {
                    return (T)objetos[typeof(T)];
                }

                return null;
            }

            public static bool Exist()
            {
                return objetos.ContainsKey(typeof(T));
            }
        }

        #endregion
    }
}
