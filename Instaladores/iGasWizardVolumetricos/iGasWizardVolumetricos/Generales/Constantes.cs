using System.IO;

namespace iGasWizardVolumetricos.Generales
{
    public static class Constantes
    {
        public static string CodigoProducto { get; set; }
        public static string Root { get; set; }
        public static string RutaBDE { get { return Utilerias.CombinarRutas(Root, @"util\x86\BDE\setup.exe"); } }
        public static string RutaGasConsola { get { return Utilerias.CombinarRutas(Root, @"BD\GasConsola.fdb"); } }
        public static string RutaComPort { get { return Utilerias.CombinarRutas(RutaImagenCo, @"PrgGas\ComPort\ComPortIgas.exe"); } }
        public static string RutaFirebird64 { get { return Utilerias.CombinarRutas(Root, @"util\x64\Firebird\Firebird-2.5.2.26540_0_x64.exe"); } }
        public static string RutaFirebird32 { get { return Utilerias.CombinarRutas(Root, @"util\x86\Firebird\Firebird-2.5.2.26540_0_Win32.exe"); } }
        public static string RutaServicioDispensarios { get { return Utilerias.CombinarRutas(RutaVolumetricos, @"DISPENSARIOS\PDISPENSARIOS.exe"); } }
        public static string RutaIniDispensarios { get { return Utilerias.CombinarRutas(RutaVolumetricos, @"DISPENSARIOS\PDISPENSARIOS.ini"); } }

        //public static string RutaFirebird64 { get { return Path.Combine(Root, @"util\x64\Firebird\Firebird-2.5.2.26540_0_x64.exe"); } }
        //public static string RutaFirebird32 { get { return Path.Combine(Root, @"util\x86\Firebird\Firebird-2.5.2.26540_0_Win32.exe"); } }
        //public static string RutaBDE { get { return Path.Combine(Root, @"util\x86\BDE\setup.exe"); } }
        //public static string RutaMicroDog64 { get { return Path.Combine(Root, @"util\x64\DriverMicroDog\MicroDogInstdrv.exe"); } }
        //public static string RutaMicroDog32 { get { return Path.Combine(Root, @"util\x86\DriverMicroDog\MicroDogInstdrv.exe"); } }
        //public static string RutaGasConsola { get { return Path.Combine(Root, @"BD\GasConsola.fdb"); } }
        //public static string RutaComPort { get { return Path.Combine(RutaImagenCo, @"PrgGas\ComPort\ComPortIgas.exe"); } }

        /// <summary>
        /// Solo obtiene la ruta no el archivo
        /// </summary>
        public static string RutaScripts { get { return Utilerias.CombinarRutas(Root, @"Scripts"); } }

        public static string RutaImagenCo { get { return @"C:\ImagenCo"; } }
        public static string RutaImagenCoTmp { get { return Utilerias.CombinarRutas(RutaImagenCo, @"tmp"); } }
        public static string RutaVolumetricos { get { return Utilerias.CombinarRutas(RutaImagenCo, @"prgGas\Volumetricos"); } }

        //public static string RutaScripts { get { return Path.Combine(Root, @"Scripts"); } }
        //public static string RutaImagenCoTmp { get { return Path.Combine(RutaImagenCo, @"tmp"); } }
        //public static string RutaVolumetricos { get { return Path.Combine(RutaImagenCo, @"prgGas\Volumetricos"); } }

        #region Mensajes

        internal class MENSAJES
        {
            internal const string VALIDANDO = "Validando requerimientos para la instalación.\n\nPor favor espere, esto podría tardar unos minutos...";
            internal const string REQUERIMIENTOS_VALIDOS = "El ambiente cumple con todos los requerimientos solicitados para continuar con la configuración del sistema.\n\nPor favor de clic en Siguiente para continuar.";
            internal const string REQUERIMIENTOS_INVALIDOS = "El ambiente no cumple con todos los requerimientos solicitados para continuar con la configuración del sistema.\n\nPor favor de clic en Instalar para procesar los requerimientos faltantes.";
            internal const string CONFIRMACION_SALIR = "¿Desea salir del Asistente de Configuración?";
            internal const string NO_SE_HAN_GENERADO_TANQUES = "No se han generado tanques.";
            internal const string NO_SE_HAN_GENERADO_ISLAS = "No se han generado islas.";
            internal const string CON_POSICION_REPETIDO = "Una posición de carga no debe tener configurada la misma posición física en más de una manguera.";
            internal const string CAMPO_LECTURA_REPETIDO = "Una posición de carga no debe tener configurado el mismo totalizador en más de una manguera.";
            internal const string NO_SE_HAN_CONFIGURADO_TODAS_LAS_MANGUERAS = "No se han configurado todas las mangueras.";
            internal const string IMPRESORA_NO_DEBE_IR_VACIA = "Debe seleccionar impresora.";
            internal const string PUERTO_NO_DEBE_IR_VACIO = "Puerto no debe ir vacío.";
            internal const string NO_SE_HA_PROPORCIONADO_IP = "No se ha proporcionado la dirección ip del servidor remoto.";
            internal const string INSTALACION_CANELADA = "Instalación cancelada por el usuario.";
            internal const string NO_SE_PUEDE_PROBAR_MARCA_DISPENSARIO = "No es posible probar puerto con la marca de dispensario seleccionada.";
            internal const string NO_SE_PUEDE_PROBAR_MARCA_TANQUE = "No es posible probar puerto con la marca de tanque seleccionada.";
            internal const string PARA_INSTALAR_DRIVER_CONECTAR_SENTINEL = "Para instalar Driver MicroDog exitosamente, favor de verificar\nque el dispositivo sentinel esté conectado en el equipo.";
        }

        #endregion

        #region Requerimientos

        internal class REQUERIMIENTOS
        {
            internal const string TITULO = "Validación de Requerimientos";
            internal const string NETFRAMEWORK35 = "NET Framework 3.5";
            internal const string FIREBIRD25 = "Firebird 2.5";
            internal const string BDE = "Borland Database Engine";
            //internal const string DRIVERMICRODOG = "Driver MicroDog";
        }

        #endregion

        #region Marca Tanque

        internal class MARCATANQUE
        {
            internal const string TITULO = "Marca de Tanques";
        }

        #endregion

        #region Tanque

        internal class TANQUE
        {
            internal const string TITULO = "Tanques";
        }

        #endregion

        #region Marca Dispensario

        internal class MARCADISPENSARIO
        {
            internal const string TITULO = "Marca de Dispensarios";
        }

        #endregion

        #region Islas

        internal class ISLAS
        {
            internal const string TITULO = "Islas";
        }

        #endregion

        #region Dispensarios

        internal class DISPENSARIOS
        {
            internal const string TITULO = "Dispensarios";
        }

        #endregion

        #region Posiciones de Carga

        internal class POSICIONESCARGA
        {
            internal const string TITULO = "Posiciones de Carga";
        }

        #endregion

        #region Mangueras

        internal class MANGUERAS
        {
            internal const string TITULO = "Mangueras";
        }

        #endregion

        #region Impresora

        internal class IMPRESORA
        {
            internal const string TITULO = "Impresora";
        }

        #endregion

        #region Variables

        internal class VARIABLES
        {
            internal const string TITULOWAYNE = "Variables de Configuración Wayne";
            internal const string TITULOBENNETT = "Variables de Configuración Bennett";
            internal const string TITULOGILBARCO = "Variables de Configuración Gilbarco";
        }

        #endregion
    }
}
