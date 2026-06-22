using System;
using System.Diagnostics;
using iGasWizardVolumetricos.Persistencia.Entidades;
using iGasWizardVolumetricos.Properties;
using Microsoft.Win32;
using _c = iGasWizardVolumetricos.Generales.Constantes;
using _u = iGasWizardVolumetricos.Generales.Utilerias;

namespace iGasWizardVolumetricos.Persistencia.Logica
{
    public class RequerimientoPersistencia
    {
        internal Requerimiento Ejecutar(Requerimiento e)
        {
            int bits = _u.Arquitectura();

            switch (e.Descripcion)
            {
                case _c.REQUERIMIENTOS.FIREBIRD25:
                    e = validarFirebird25(bits);
                    if (!e.Instalado)
                    {
                        instalar(bits.Equals(64) ? _c.RutaFirebird64 : _c.RutaFirebird32);
                        e = validarFirebird25(bits);
                    }
                    break;
                case _c.REQUERIMIENTOS.BDE:
                    e = validarBDE();
                    if (!e.Instalado)
                    {
                        instalar(_c.RutaBDE);
                        e = validarBDE();
                    }
                    break;
                //case _c.REQUERIMIENTOS.DRIVERMICRODOG:
                //    e = validarMicroDog();
                //    if (!e.Instalado)
                //    {
                //        _u.Informacion(_c.MENSAJES.PARA_INSTALAR_DRIVER_CONECTAR_SENTINEL);
                //        instalar(bits.Equals(64) ? _c.RutaMicroDog64 : _c.RutaMicroDog32);
                //        e = validarMicroDog();
                //    }
                //    break;
            }

            return e;
        }

        internal ListaRequerimiento Ejecutar()
        {
            ListaRequerimiento r = new ListaRequerimiento();

            int bits = _u.Arquitectura();

            r.Add(validarNETFramework35());
            r.Add(validarFirebird25(bits));
            r.Add(validarBDE());
            //r.Add(validarMicroDog());

            return r;
        }

        private void instalar(string fileName)
        {
            Process p = new Process();
            try
            {
                p.StartInfo = new ProcessStartInfo(fileName);
                p.Start();
                p.WaitForExit();
            }
            finally
            {
                p.Close();
                p.Dispose();
                p = null;
            }
        }

        private Requerimiento validarNETFramework35()
        {
            Requerimiento r = new Requerimiento(_c.REQUERIMIENTOS.NETFRAMEWORK35);

            try
            {
                RegistryKey npdKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\");

                string[] npdSubKeys = npdKey.GetSubKeyNames();

                string keyName = Array.Find<string>(npdSubKeys, v => v.Contains("v3.5"));

                if (string.IsNullOrEmpty(keyName)) r.Instalado = false;

                RegistryKey subKey = npdKey.OpenSubKey(keyName);

                if (subKey == null) r.Instalado = false;

                string name = subKey.GetValue("Version", string.Empty).ToString();
                string sp = subKey.GetValue("SP", string.Empty).ToString();
                string install = subKey.GetValue("Install", string.Empty).ToString();

                if (string.IsNullOrEmpty(install) || !install.Equals("1")) r.Instalado = false;

                r.Instalado = true;
            }
            catch
            {
                r.Instalado = false;
            }

            r.Icono = r.Instalado ? Resources.check : Resources.delete;

            return r;
        }

        private Requerimiento validarFirebird25(int bits)
        {
            Requerimiento r = new Requerimiento(_c.REQUERIMIENTOS.FIREBIRD25);

            try
            {
                r.Instalado = bits.Equals(64)
                    ? iGasWizardVolumetricos.Generales.RegistryWOW6432.ExistRegKey64(iGasWizardVolumetricos.Generales.RegHive.HKEY_LOCAL_MACHINE, @"SOFTWARE\Firebird Project\Firebird Server\Instances")
                    : iGasWizardVolumetricos.Generales.RegistryWOW6432.ExistRegKey32(iGasWizardVolumetricos.Generales.RegHive.HKEY_LOCAL_MACHINE, @"SOFTWARE\Firebird Project\Firebird Server\Instances");
            }
            catch
            {
                r.Instalado = false;
            }

            r.Icono = r.Instalado ? Resources.check : Resources.delete;

            return r;
        }

        private Requerimiento validarBDE()
        {
            Requerimiento r = new Requerimiento(_c.REQUERIMIENTOS.BDE);

            try
            {
                RegistryKey npdKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");

                foreach (string key in npdKey.GetSubKeyNames())
                {
                    RegistryKey subKey = npdKey.OpenSubKey(key);
                    var displayName = subKey.GetValue("DisplayName");
                    if (displayName != null)
                    {
                        if (displayName.ToString().Contains("BDE"))
                        {
                            r.Instalado = true;
                            break;
                        }
                    }
                }
            }
            catch
            {
                r.Instalado = false;
            }

            r.Icono = r.Instalado ? Resources.check : Resources.delete;

            return r;
        }

        //private Requerimiento validarMicroDog()
        //{
        //    Requerimiento r = new Requerimiento(_c.REQUERIMIENTOS.DRIVERMICRODOG);

        //    try
        //    {
        //        RegistryKey npdKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Class\{36FC9E60-C465-11CF-8056-444553540000}");

        //        if (npdKey != null)
        //        {
        //            foreach (string i in npdKey.GetSubKeyNames())
        //            {
        //                RegistryKey subKey = npdKey.OpenSubKey(i);
        //                var driver = subKey.GetValue("DriverDesc");
        //                if (driver != null && driver.ToString().Contains("MicroDog USB Device"))
        //                {
        //                    r.Instalado = true;
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        r.Instalado = false;
        //    }

        //    r.Instalado = true;

        //    r.Icono = r.Instalado ? Resources.check : Resources.delete;

        //    return r;
        //}
    }
}
