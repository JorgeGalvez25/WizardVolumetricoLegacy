using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Persistencia.Entidades;
using iGasWizardVolumetricos.Persistencia.Logica;
using IWshRuntimeLibrary;

namespace iGasWizardVolumetricos.Pantallas.Procesando
{
    public class Presenter
    {
        public List<OpcionesArchivos> ConfiguracionArchivos { get; set; }

        public Presenter()
        {
            var conf = WorkItem.Objetos<UbicarBD>.Get();
            this.ConfiguracionArchivos = new List<OpcionesArchivos>();
            this.ConfiguracionArchivos.Add(new OpcionesArchivos()
                {
                    Nombre = "I-Gas Controlador",
                    Tipo = TipoArchivo.ModificaArch,
                    RutaExe = Path.Combine(Constantes.RutaVolumetricos, @"PANEL 2.0\PPANMENU.EXE"),
                    FilePath = Path.Combine(Constantes.RutaVolumetricos, @"PANEL 2.0\Panel.ini"),
                    Configuracion = new IniItem()
                    {
                        Section = "config",
                        Key = "db",
                        Value = conf.RutaBD
                    }
                });

            this.ConfiguracionArchivos.Add(new OpcionesArchivos()
            {
                Nombre = "I-Gas Cliente",
                Tipo = TipoArchivo.ModificaLnk,
                RutaExe = Path.Combine(Constantes.RutaVolumetricos, @"Panel Cliente\PPANMENUC.exe"),
                Configuracion = conf.Alias
            });

            this.ConfiguracionArchivos.Add(new OpcionesArchivos()
                {
                    Nombre = "I-Gas Consola",
                    Tipo = TipoArchivo.ModificaLnk,
                    RutaExe = Path.Combine(Constantes.RutaVolumetricos, @"DISPENSARIOS\PDISMENU.exe"),
                    Configuracion = conf.Alias
                });
            this.ConfiguracionArchivos.Add(new OpcionesArchivos()
            {
                Nombre = "I-Gas IgasPanel",
                Tipo = TipoArchivo.ModificaLnk,
                RutaExe = Path.Combine(Constantes.RutaVolumetricos, @"IGASPANEL\IGASPANEL.exe"),
                Configuracion = conf.Alias
            });
            this.ConfiguracionArchivos.Add(new OpcionesArchivos()
                {
                    Nombre = "I-Gas Tanques",
                    Tipo = TipoArchivo.ModificaLnk,
                    RutaExe = Path.Combine(Constantes.RutaVolumetricos, @"TANQUES\PTANMENU.EXE"),
                    Configuracion = conf.Alias
                });
        }

        public bool ExcecutePathScript(ref string msj)
        {
            try
            {
                ProcesandoPersistencia servicio = new ProcesandoPersistencia();
                return servicio.ExcecutePathScript(Constantes.RutaScripts, ref msj);
            }
            catch (Exception e)
            {
                msj += Utilerias.LeerExcepcion(e);
                return false;
            }
        }

        public bool EditShortcut(ref string msj)
        {
            bool flgError = false;
            DiccionarioProcesando _dic = null;
            StringBuilder sb = new StringBuilder();

            WshShell shell = new WshShell();
            {
                IWshShortcut shortcut = null;
                string shortcutAddress = string.Empty;
                _dic = new DiccionarioProcesando();
                string deskPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string menuPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Programs), "iGas Volumetricos");

                try
                {
                    Action<OpcionesArchivos, string> fn = new Action<OpcionesArchivos, string>((item, path) =>
                        {
                            shortcutAddress = Path.Combine(path, item.Nombre + ".lnk");
                            if (System.IO.File.Exists(shortcutAddress)) { System.IO.File.Delete(shortcutAddress); }

                            shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
                            {
                                shortcut.TargetPath = item.RutaExe;
                                if (item.Tipo == TipoArchivo.ModificaLnk)
                                {
                                    shortcut.Arguments = item.Configuracion.ToString();
                                }
                                shortcut.Save();
                            }

                            if (!_dic.ContainsKey(item.Nombre))
                            {
                                _dic.Add(item.Nombre, item);
                            }
                        });

                    if (Directory.Exists(menuPath)) // Existe?
                    {
                        /// Borra y crea la carpeta
                        // Elimina la ruta
                        Directory.Delete(menuPath, true);
                        // Se crea de nuevo la ruta pero vacia
                        Directory.CreateDirectory(menuPath);
                    }
                    else
                    {
                        // Se crea de nuevo la ruta pero vacia
                        Directory.CreateDirectory(menuPath);
                    }

                    foreach (var item in this.ConfiguracionArchivos)
                    {
                        // Modifica los links del escritorio
                        fn(item, deskPath);
                        // Modifica los links del menu inicio
                        fn(item, menuPath);
                    }
                }
                catch (Exception e)
                {
                    sb.AppendLine("[ENLACE ARCHIVO DE CONFIGURACION]")
                      .AppendLine(Utilerias.LeerExcepcion(e))
                      .AppendLine();
                    flgError = true;
                }
                finally
                {
                    if (shortcut != null) { shortcut = null; }
                    shortcutAddress =
                        deskPath =
                        menuPath = string.Empty;
                    shortcutAddress =
                        deskPath =
                        menuPath = null;
                }
            }

            IniItem aux = null;
            IniParser parser = null;
            // Para modificar un archivo INI
            foreach (var item in this.ConfiguracionArchivos.Where(p => p.Tipo == TipoArchivo.ModificaArch))
            {
                try
                {
                    // Lee y levanta el archivo de configuración INI
                    parser = new IniParser(item.FilePath);
                    aux = (IniItem)item.Configuracion;

                    parser.EditSetting(aux.Section, aux.Key, aux.Value);
                    parser.SaveSettings();
                }
                catch (Exception e)
                {
                    sb.AppendLine("[MODIFICAR ARCHIVO DE CONFIGURACION]")
                      .AppendLine(Utilerias.LeerExcepcion(e))
                      .AppendLine();
                    flgError = true;
                    break;
                }
            }

            if (!flgError)
            {
                ProcesandoPersistencia servicio = new ProcesandoPersistencia();
                {
                    if (!servicio.ActualizaRutas(_dic))
                    {
                        sb.AppendLine("No fue posible actualizar las rutas de Base de Datos.");
                        flgError = true;
                    }
                }
            }

            msj = (sb.Length <= 0 ? null : sb.ToString().Trim().Clone().ToString());
            sb.Remove(0, sb.Length);
            sb = null;

            return !flgError;
        }
    }
}
