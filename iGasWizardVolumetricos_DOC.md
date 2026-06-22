# iGasWizardVolumetricos — Documentación Técnica Completa

> **Propósito de este documento:** Sirve como mapa completo del proyecto para que, al recibir un archivo aislado del mismo junto con este MD, se entienda de inmediato su rol, cómo encaja con el resto, qué impacto tienen los cambios y qué tablas/procesos externos involucra.

---

## 1. ¿Qué es este proyecto?

`iGasWizardVolumetricos` es un **asistente de configuración (setup wizard)** para Windows, escrito en **C# / .NET Framework / WinForms**, que configura el sistema de control volumétrico de combustible **I-Gas** fabricado por la empresa **I-Gas (ImagenCo)** para gasolineras en México.

El wizard instala/verifica dependencias del sistema operativo (Firebird, BDE, .NET 3.5), copia y configura una base de datos Firebird 2.5 (`GasConsola.fdb`), registra licencias, configura la topología física de la gasolinera (tanques, islas, dispensarios, mangueras), asigna la impresora de tickets y ajusta los turnos administrativos. Al terminar, todas las aplicaciones I-Gas del módulo Volumétricos quedan apuntando a la nueva BD y listas para operar.

---

## 2. Estructura de la Solución

```
iGasWizardVolumetricos/              ← Carpeta raíz de la solución
│
├── iGasWizardVolumetricos.sln       ← Solución Visual Studio 2022
│
├── bits/                            ← Sub-proyecto auxiliar C# (consola)
│   ├── bits.csproj
│   └── Program.cs                   ← Escribe en stdout IntPtr.Size*8 ("32" ó "64")
│
├── AliasBDE/                        ← Sub-proyecto Delphi LEGACY (no se usa en producción)
│   ├── AliasBDE.dpr                 ← DLL Delphi que manejaba alias BDE (código histórico)
│   └── AliasBDE.cfg
│
└── iGasWizardVolumetricos/          ← Proyecto principal C# WinForms
    ├── iGasWizardVolumetricos.csproj
    ├── Program.cs                   ← Entry point: acepta CodigoProducto como arg[0]
    ├── Wizard.cs + Wizard.Designer.cs  ← Formulario contenedor del wizard (DevExpress XtraForm)
    │
    ├── Generales/                   ← Infraestructura transversal
    │   ├── Constantes.cs            ← Todas las rutas y mensajes como constantes/propiedades estáticas
    │   ├── Utilerias.cs             ← Helpers: ejecutar procesos externos, IniParser, DllManager
    │   ├── WorkItem.cs              ← Contenedor de estado global del wizard (DI manual)
    │   ├── InstallStatus.cs         ← Enum: Successful | Cancelled
    │   ├── Read64bitRegistryFrom32bitApp.cs  ← P/Invoke a Advapi32 para leer claves 64-bit
    │   └── Extensiones.cs           ← Métodos de extensión (BeginSafe, CombinePaths)
    │
    ├── Interfaces/
    │   └── IUserPages.cs            ← Contrato que implementa cada pantalla del wizard
    │
    ├── Pantallas/                   ← Una carpeta por cada paso del wizard
    │   ├── Requerimientos/          ← Paso 1: Validar/instalar .NET 3.5, Firebird 2.5, BDE
    │   ├── UbicacionBD/             ← Paso 2: Seleccionar ruta y alias para GasConsola.fdb
    │   ├── Procesando/              ← Paso 3: Ejecutar scripts SQL, crear accesos directos
    │   ├── Licenciamiento/          ← Paso 4: Ingresar y validar licencias CVOL/CVOL02/CVAC
    │   ├── DatosDeEmpresa/          ← Paso 5: RFC, PEMEX, nombre comercial, estación
    │   ├── MarcaTanque/             ← Paso 6: Seleccionar marca de tanques
    │   ├── Tanques/                 ← Paso 7: Generar tanques
    │   ├── MarcaDispensario/        ← Paso 8: Marca, interfaz, puerto COM de dispensarios
    │   ├── Wayne/                   ← Paso 9: Variables específicas Wayne
    │   ├── Gilbarco/                ← Paso 10: Variables específicas Gilbarco/PAM
    │   ├── Islas/                   ← Paso 11: Generar islas y número de dispensarios por isla
    │   ├── Dispensarios/            ← Paso 12: Generar dispensarios por isla, posiciones de carga
    │   ├── PosicionesCarga/         ← Paso 13: Generar posiciones de carga, número de mangueras
    │   ├── Mangueras/               ← Paso 14: Asignar combustible/totalizador/posición por manguera → escribe DPVGBOMB
    │   ├── Impresora/               ← Paso 15: Seleccionar impresora de tickets
    │   └── TurnosAdministrativos/   ← Paso 16: Configurar turnos (DPVGTURC)
    │
    ├── Persistencia/
    │   ├── Entidades/               ← POCOs (modelos de datos)
    │   └── Logica/                  ← Clases de acceso a BD (Firebird vía FirebirdSql.Data)
    │
    ├── Properties/
    │   ├── Resources.resx + Resources.Designer.cs  ← Imágenes embebidas (iconos, fotos de guía)
    │   └── AssemblyInfo.cs
    │
    └── Resources/                   ← Archivos de imagen fuente (Base.png, Sentinel-Volumetrico.png, etc.)
```

---

## 3. Stack Tecnológico

| Componente | Detalle |
|---|---|
| Lenguaje | C# (.NET Framework — la versión exacta no está fijada en código pero usa `FbConnection` y DevExpress) |
| UI framework | **DevExpress WinForms** (XtraForm, XtraUserControl, XtraWizard, XtraGrid, XtraEditors) |
| Base de datos | **Firebird 2.5** vía `FirebirdSql.Data.FirebirdClient` (DLL incluida en bin/Debug) |
| COM shortcuts | `IWshRuntimeLibrary` (Windows Script Host, vía COM interop — `Interop.IWshRuntimeLibrary.dll`) |
| Arquitectura | **MVP** (Model-View-Presenter) con contenedor de estado global `WorkItem` |
| Cultura | `es-MX` (fijada en `Program.cs`) |

---

## 4. Arquitectura General — MVP + WorkItem

### 4.1 Patrón por pantalla

Cada paso del wizard sigue exactamente este patrón:

```
PXxx.cs          ← View: XtraUserControl + IUserPages
PXxx.Designer.cs ← Código generado por el diseñador
Presenter.cs     ← Presenter: lógica de negocio (cuando existe; algunas pantallas la tienen inline)
Entidad.cs       ← Model (en Persistencia/Entidades/)
XxxPersistencia.cs ← Acceso a BD (en Persistencia/Logica/)
```

No todas las pantallas tienen Presenter separado; algunas pantallas simples (como `PIslas`, `PDispensarios`) tienen la lógica directamente en el `.cs`.

### 4.2 WorkItem — Estado Global del Wizard

`WorkItem` (en `Generales/WorkItem.cs`) es un contenedor de estado **estático global** que actúa como inyector de dependencias manual. Es el único mecanismo para pasar datos entre pantallas.

```csharp
// Guardar algo en WorkItem
WorkItem.Objetos<UbicarBD>.Add(instancia);

// Leer algo
UbicarBD conf = WorkItem.Objetos<UbicarBD>.Get();

// Verificar existencia antes de leer
if (WorkItem.Objetos<EmpresaConfiguracion>.Exist()) { ... }
```

El diccionario interno usa `typeof(T)` como clave → **solo puede existir una instancia de cada tipo a la vez**. Al hacer `Add`, sobreescribe la anterior.

**`WorkItem` también contiene el array `Pantallas`** que define el orden completo del wizard:

```csharp
private static XtraUserControl[] pantallas = new XtraUserControl[]
{
    new PRequerimientos(),      // Paso 1
    new PUbicarBD(),            // Paso 2
    new PProcesando(),          // Paso 3
    new PLicenciamiento(),      // Paso 4
    new PDatosEmpresa(),        // Paso 5
    new PMarcaTanque(),         // Paso 6
    new PTanques(),             // Paso 7
    new PMarcaDispensario(),    // Paso 8
    new PWayne(),               // Paso 9
    new PGilbarco(),            // Paso 10
    new PIslas(),               // Paso 11
    new PDispensarios(),        // Paso 12
    new PPosicionesCarga(),     // Paso 13
    new PMangueras(),           // Paso 14
    new PImpresora(),           // Paso 15
    new PTurnosAdministrativos()// Paso 16
};
```

> ⚠️ **Importante:** El orden aquí es el orden en que aparecen al usuario. Para agregar/quitar un paso basta con agregar/quitar la entrada en este array; el `Wizard.cs` lo itera dinámicamente.

### 4.3 Interfaz IUserPages

Todas las pantallas implementan esta interfaz:

```csharp
public interface IUserPages
{
    void DoInit(BaseWizardPage parent);   // Llamado al ENTRAR a la página (forward)
    void NextClick(object sender, WizardCommandButtonClickEventArgs e);  // Botón "Siguiente"
    void PrevClick(object sender, WizardCommandButtonClickEventArgs e);  // Botón "Anterior"
    void CancelClick(FormClosingEventArgs e);  // Botón "Cancelar" / cierre del form
}
```

El `Wizard.cs` los invoca mediante casting en los eventos del `wcVolumetricos` (DevExpress XtraWizard). `DoInit` se llama en `SelectedPageChanging` cuando la dirección es `Forward`.

### 4.4 Wizard.cs — Contenedor Principal

- Es un `XtraForm` con un control `wcVolumetricos` (XtraWizard).
- En `Wizard_Load` construye dinámicamente las `WizardPage`s, añade `welcomeWizardPage1` al inicio y `completionWizardPage1` al final.
- El evento `Wizard_Disposed` cierra y libera la conexión Firebird activa del `WorkItem`.
- `wcVolumetricos_FinishClick` cierra el form sin disparar el evento `FormClosing` (desuscribe primero).
- Al cancelar, llama a `Utilerias.CancelarInstalacion()` que ejecuta `msiexec /passive /x {CodigoProducto}` si fue invocado como parte de un instalador MSI.

---

## 5. Flujo Completo del Wizard — Cada Paso en Detalle

### Paso 0: Program.cs — Inicio

```csharp
static void Main(string[] args)
{
    if (args.Length > 0)
        _c.CodigoProducto = args[0];  // GUID del producto MSI (para poder desinstalar si se cancela)
    _c.Root = Application.StartupPath;
    Application.Run(new Wizard());
}
```

`Root` es el directorio donde vive el `.exe`. De aquí se derivan TODAS las rutas de herramientas.

---

### Paso 1: PRequerimientos — Validación e Instalación de Requisitos

**Archivo:** `Pantallas/Requerimientos/PRequerimientos.cs`  
**Persistencia:** `Persistencia/Logica/RequerimientoPersistencia.cs`

**¿Qué hace?**  
Al entrar (`DoInit`) lanza un `BackgroundWorker` que llama a `RequerimientoPersistencia.Ejecutar()`. Valida tres requisitos:

| Requisito | Cómo se verifica |
|---|---|
| .NET Framework 3.5 | Clave de registro: `HKLM\SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5` → campo `Install=1` |
| Firebird 2.5 | Clave de registro: `HKLM\SOFTWARE\Firebird Project\Firebird Server\Instances` (lee 64-bit o 32-bit según arquitectura detectada) |
| BDE | `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall` → busca cualquier entrada cuyo `DisplayName` contenga "BDE" |

Si alguno falta, aparece el botón **"Instalar"**. Al hacer clic, instala uno por uno en `BackgroundWorker` llamando al instalador correspondiente con `Process.Start()`.

> ⚠️ **Bug conocido — Detección de arquitectura frágil:**  
> `Utilerias.Arquitectura()` ejecuta `bits.exe` desde `Root`. Si el archivo no está (o no se copió junto al wizard), el `catch` silencioso retorna `32` → se consulta la clave de registro de 32 bits → en un OS de 64 bits, Firebird 64-bit no se encontrará → falso negativo "Firebird no instalado".  
> **Solución:** Verificar que `bits.exe` esté en el mismo directorio que el `.exe` principal. Es el binario del sub-proyecto `bits/`.

**Notas:**
- El comentado `// MicroDog` muestra que el driver Sentinel fue desacoplado de este wizard.
- Mientras valida/instala, `parent.AllowNext = false` y `parent.AllowCancel = false`. La cancelación es bloqueada por `CancelClick` mientras `isCancelable = false`.

---

### Paso 2: PUbicarBD — Configuración de la Base de Datos

**Archivo:** `Pantallas/UbicacionBD/PUbicarBD.cs` + `Presenter.cs`  
**Persistencia:** `Persistencia/Logica/UbicarBDPersistencia.cs`

**¿Qué hace?**  
El usuario especifica:
- **Alias BDE** (máx. 30 chars) — nombre con el que las apps Delphi acceden a la BD.
- **Ruta de la BD** (máx. 255 chars, limitada además a 239 chars de directorio) — se selecciona con `FolderBrowserDialog`; el archivo siempre se llama `GasConsola.fdb`.

**Valor por defecto:** `Alias = "GasConsolaImagen"`, `Ruta = C:\ImagenCo\DBI\Consola\GasConsola.fdb`

**En `NextClick` del Presenter:**

1. **Operaciones de archivo (`OperacionsIO`):**
   - Crea el directorio destino si no existe.
   - Si ya existe un `.fdb`, libera conexiones (`LiberarConexiones` + espera de lock), borra, y copia la plantilla desde `Constantes.RutaGasConsola` = `{Root}\BD\GasConsola.fdb`.

2. **Operaciones BDE (`OperacionesBDE`):**
   - Llama a `CmdAlias.exe Existe {alias}` → si retorna 1, llama `CmdAlias.exe Actualizar {alias} {path}`.
   - Si no existe, llama `CmdAlias.exe Crear {alias} {path}`.
   - Nota: Las líneas `[DllImport("AliasBDE.dll")]` están **comentadas** — se usó antes una DLL Delphi; ahora se usa el proceso externo `CmdAlias.exe`.

3. **Prueba de conexión (`PruebaDeConexion`):**
   - Crea instancia de `Conexion` con la nueva ruta.
   - La guarda en `WorkItem.Objetos<Conexion>.Add(conn)`.
   - Ejecuta `SELECT * FROM DPVGTCMB` → si tiene éxito, la BD es válida (es una GasConsola real).

4. **Valida que la BD es GasConsola** (cuando se selecciona una carpeta con BD existente):
   - `SELECT COUNT(RDB$RELATION_NAME) FROM RDB$RELATIONS WHERE ... RDB$RELATION_NAME = 'DPVGCONF'` — si existe la tabla `DPVGCONF`, es válida.

**Objeto que se guarda en WorkItem:** `UbicarBD { Alias, RutaBD }` — usado en prácticamente todas las pantallas posteriores que crean conexiones directas (Mangueras, Variables, Impresora).

---

### Paso 3: PProcesando — Ejecución de Scripts y Accesos Directos

**Archivo:** `Pantallas/Procesando/PProcesando.cs` + `Presenter.cs`  
**Persistencia:** `Persistencia/Logica/ProcesandoPersistencia.cs`

**¿Qué hace?** Dos sub-tareas en secuencia:

#### 3.1 `ExcecutePathScript` — Scripts SQL
- Lee todos los archivos `*.sql` de `{Root}\Scripts\`.
- Los ejecuta contra la BD con `FbScript` + `FbBatchExecution`.
- Si alguno falla, retorna el error y bloquea el avance.
- En caso de error, si el mensaje contiene `"EXECUTE PROCEDURE EMPR_REVI"`, informa al usuario que debe actualizar la BD Consola a una versión específica.

#### 3.2 `EditShortcut` — Accesos Directos y Panel.ini

Crea/recrea la carpeta `%APPDATA%\...\Programs\iGas Volumetricos` (menú inicio) y crea `.lnk` en el escritorio y en esa carpeta para cada uno de estos programas:

| Nombre | Ejecutable | Tipo |
|---|---|---|
| I-Gas Controlador | `C:\ImagenCo\prgGas\Volumetricos\PANEL 2.0\PPANMENU.EXE` | `ModificaArch` → edita Panel.ini |
| I-Gas Cliente | `C:\ImagenCo\prgGas\Volumetricos\Panel Cliente\PPANMENUC.exe {alias}` | `ModificaLnk` |
| I-Gas Consola | `C:\ImagenCo\prgGas\Volumetricos\DISPENSARIOS\PDISMENU.exe {alias}` | `ModificaLnk` |
| I-Gas IgasPanel | `C:\ImagenCo\prgGas\Volumetricos\IGASPANEL\IGASPANEL.exe {alias}` | `ModificaLnk` |
| I-Gas Tanques | `C:\ImagenCo\prgGas\Volumetricos\TANQUES\PTANMENU.EXE {alias}` | `ModificaLnk` |

**Para I-Gas Controlador** (único `ModificaArch`):
- Abre `C:\ImagenCo\prgGas\Volumetricos\PANEL 2.0\Panel.ini` con `IniParser`.
- Edita la sección `[config]`, clave `db` → pone la ruta de la BD.
- Guarda el archivo.

> ⚠️ **Bug conocido — Panel.ini debe existir:**  
> `IniParser` en su constructor lanza `FileNotFoundException` si el archivo no existe. Si I-Gas no está instalado (o `Panel.ini` aún no fue creado), el `EditShortcut` entero falla. Solución: validar existencia antes de intentar parsear, o crear el archivo con valores por defecto.

**Finalmente**, llama a `ProcesandoPersistencia.ActualizaRutas()`:
```sql
UPDATE DPVGCONF SET COMANDO1 = '{ruta_consola} {alias}', COMANDO2 = '{ruta_tanques} {alias}'
```

**En `NextClick`** (al avanzar de este paso):
- Carga combustibles desde la BD: `SELECT * FROM DPVGTCMB` vía `CombustibleVolPersistencia.Obtener()`.
- Los guarda en `WorkItem.Objetos<ListaCombustibleVol>.Add(l)` — usados después en Mangueras y Wayne.
- Llama a `Conexion.LimpiarConexiones()` (limpia pool de Firebird).

---

### Paso 4: PLicenciamiento — Licencias del Software

**Archivo:** `Pantallas/Licenciamiento/PLicenciamiento.cs` + `Presenter.cs`

**¿Qué hace?** Captura y valida tres tipos de licencia:

| Campo | Licencia | Validación |
|---|---|---|
| Licencia Volumétrico (`txtLicenciaVolumetrico`, 8 chars) | CVOL | `licenciavalidacvol "RazonSocial\|CVOL\|3.1\|Abierta\|{lic}\|1\|{temporal}\|{fecha}"` → debe retornar `"Valido"` |
| Licencia Inocua (`txtLicenciaInocua`, 8 chars) | CVOL02 | Igual con sistema `CVOL02` |
| Licencia Control Versiones (`txtLicenciaControlVersiones`, 8 chars) | CVAC | `licenciacongruente "RazonSocial+NoEstacion\|CVAC\|3.1\|Abierta\|{lic}\|1\|true\|{fecha}"` → debe retornar `"1"` |

**Validación externa:** Las tres llaman a `LibsDelphi.exe` (proceso externo) pasando el comando como argumento. Se lee la salida estándar.

**Campos adicionales:**
- Razón Social (máx. 80 chars, mayúsculas)
- No. Sentinel (máx. 5 chars, solo dígitos y `&`)
- No. Estación — formato `E#####` (letra E + 5 dígitos). Si se teclea solo números, auto-completa con `E` al inicio. Regex: `E[0-9]{5}`.
- Check `¿Es Temporal?` → habilita fecha de vencimiento (auto-propone +1 mes).

**UI contextual:** Cada campo al recibir foco muestra una imagen de ayuda en `picExample` (imagen de donde se ubica el dato en el sentinel físico, etc.).

**Verificaciones temporales:**
- Si `FechaVence <= hoy` → error "licencia caducada".
- Si `FechaVence >= hoy + 5 días` → confirmación de que está por vencer.

**Objetos guardados en WorkItem:**
- `WorkItem.Objetos<Licencia>.Add(this.Licenciamiento)`
- `WorkItem.Objetos<EmpresaConfiguracion>.Add(emp)` (parcial, solo datos de licencia + NumEstacion)

---

### Paso 5: PDatosEmpresa — Datos de la Empresa

**Archivo:** `Pantallas/DatosDeEmpresa/PDatosEmpresa.cs` + `Presenter.cs`  
**Persistencia:** `Persistencia/Logica/EmpresaConfiguracionPersistencia.cs`

**¿Qué hace?** Captura datos de la empresa y los persiste en BD:

| Campo | Tabla/Campo BD | Validación |
|---|---|---|
| Usuario PEMEX | `DPVGCONF.USUARIOPEMEX` | Requerido |
| Clave PEMEX | `DPVGCONF.CLAVEPEMEX` | Requerido |
| Nombre Comercial | `DPVGCONF.NOMBRECOMERCIAL` + `DGENEMPR.NOMCOMERCIAL` | Requerido, mayúsculas |
| RFC | `DGENEMPR.RFC` | Regex `((\\s)?(&\|[A-Z]){3,4})([0-9]{6})((&\|[A-Z0-9]){3})` |
| Dirección | `DGENEMPR.DIRECCION` | Requerido, máx 50 |
| Población | `DGENEMPR.POBLACION` | Requerido, máx 50 |
| No. Estación | `DPVGESTS.NUMEROESTACION` | Solo lectura (viene de Licenciamiento) |
| Nombre Estación | `DPVGESTS.NOMBRE` | Requerido, máx 40 |

**En `NextClick`**, llama a `EmpresaConfiguracionPersistencia.ActualizarOInsertar()`:
- Si `DPVGCONF` tiene 0 filas → `InsertarLicencia` (INSERT en DPVGCONF).
- Si `DGENEMPR` tiene 0 filas → `InsertarEmpresa` (INSERT en DGENEMPR).
- Si `DPVGESTS` tiene 0 filas → `InsertarEstacion` (INSERT en DPVGESTS).
- Luego siempre hace UPDATE de las tres tablas.

**Campo CONSOLA en DPVGESTS:** Es un blob de texto con pares clave-valor separados por CRLF. Se preservan las claves existentes y se inyectan/actualizan:
```
Inocuidad31Licencia={lic_inocua}
Inocuidad31FechaVence={fecha_inocua_dd/MM/yyyy}
ManejaServicios=Si
PuertoServicio=http://127.0.0.1:8199/bin
```

**Campo INOCUIDAD en DPVGESTS:** Se almacena una cadena ofuscada fija en INSERT, y otra en UPDATE.

---

### Paso 6: PMarcaTanque — Marca de Tanques

**Archivo:** `Pantallas/MarcaTanque/PMarcaTanque.cs`  
(No se leyó en detalle, pero sigue el patrón: selecciona la marca de sonda de tanque, guarda en WorkItem, persiste en BD.)

---

### Paso 7: PTanques — Configuración de Tanques

**Archivo:** `Pantallas/Tanques/PTanques.cs`  
(Genera la lista de tanques según el número indicado. Persiste en BD vía `TanqueVolPersistencia`.)

---

### Paso 8: PMarcaDispensario — Marca y Puerto de Dispensarios

**Archivo:** `Pantallas/MarcaDispensario/PMarcaDispensario.cs`  
**Persistencia:** `Persistencia/Logica/MarcaDispensarioVolPersistencia.cs`

**¿Qué hace?** Configura la interfaz de comunicación con los dispensarios:

**Marcas disponibles** (clave → nombre):

| Clave | Nombre |
|---|---|
| 1 | Wayne |
| 2 | Bennett |
| 3 | Team |
| 4 | Gilbarco |
| 5 | Hong Yang |
| 6 | Quantium |

Al seleccionar marca, se cargan los tipos de interfaz disponibles para esa marca (de BD o tabla predeterminada).

**Parámetros de puerto COM:**
- Número de puerto (`txtNumPuerto`, desde 1)
- Velocidad (baud rate), ej: `9600`
- Paridad, bits de datos, bits de parada
- Modo de operación (`txtModoOperacion`, combo)
- Botón **"Probar"**: llama a `Utilerias.ComPortIgas(params)` → ejecuta `ComPortIgas.exe` con los parámetros. Solo funciona con marcas que lo soportan (Wayne y Gilbarco sí; Team y otros no).

**Objeto guardado en WorkItem:** `MarcaDispensarioVol` — contiene `Marca` (int clave), `ModoOperacion`, configuración de COM, `ReconexionAros`.

> Esta pantalla condiciona la visibilidad de los pasos Wayne (9) y Gilbarco (10): si la marca es Wayne (1) → se muestra PWayne; si es Gilbarco (4) → se muestra PGilbarco; de lo contrario, esas pantallas pueden mostrarse pero sus datos no se usan.

---

### Paso 9: PWayne — Variables de Configuración Wayne

**Archivo:** `Pantallas/Wayne/PWayne.cs`  
**Persistencia:** `Persistencia/Logica/VariablesVolPersistencia.cs`

**¿Qué hace?** Solo aplica cuando la marca de dispensario es **Wayne (clave 1)**.

Configura:
- **Decimales Preset** (importe): 0-3 (`00000000`, `0000000.0`, `000000.00`, `00000.000`)
- **Decimales Preset Litros**: ídem
- **Con Dígito de Ajuste**: formato `00000.000`, `000000.00`, `0000000.0`
- **Nivel de Precio Wayne**: Contado o Crédito
- **Con Producto/Precio** (grid): por cada combustible de la BD → asigna "Id Físico del Producto" (1-3)
- **Soporta Selección de Producto**: checkbox

**En NextClick:** Guarda `VariablesVol` en WorkItem y llama a `VariablesVolPersistencia.Guardar()` → escribe en el campo `CONSOLA` de `DPVGESTS` con los parámetros Wayne.

---

### Paso 10: PGilbarco — Variables de Configuración Gilbarco/PAM

**Archivo:** `Pantallas/Gilbarco/PGilbarco.cs`  
**Persistencia:** `Persistencia/Logica/VariablesVolPersistencia.cs`

**¿Qué hace?** Solo aplica cuando la marca es **Gilbarco (clave 4)**.

Configura:
- **SetUpPam1000**: número de setup del PAM
- **Decimales Preset PAM** (importe)
- **Decimales Preset PAM Litros**
- **Máximo Preset PAM**
- **Dígitos Gilbarco**: `5` = `000.00`, `6` = `0000.00`
- **Modo Autoriza PAM**: Sin límite / Por Importe
- **Ajuste PAM**: checkbox
- **Reconexiones AROS**
- **Dígito Ajuste Vol**, **Con Dígito Ajuste**

Igual que Wayne, escribe en el campo `CONSOLA` de `DPVGESTS` añadiendo las claves específicas de Gilbarco.

---

### Paso 11: PIslas — Generación de Islas

**Archivo:** `Pantallas/Islas/PIslas.cs`

**¿Qué hace?** El usuario especifica cuántas islas quiere (`txtIslas`, spinner) y presiona "Generar". Se crea una `ListaIslaVol` con `N` registros, cada uno con:
- `Clave` = índice (1, 2, 3...)
- `NumeroDispensarios` = editable en grid (1-5)

Al hacer NextClick, valida que haya al menos una isla. Guarda `ListaIslaVol` en WorkItem.

**Entidad:** `IslaVol { Clave, NumeroDispensarios }`

---

### Paso 12: PDispensarios — Generación de Dispensarios

**Archivo:** `Pantallas/Dispensarios/PDispensarios.cs`

**¿Qué hace?** Genera automáticamente los dispensarios desde la lista de islas del WorkItem. Cada isla produce `NumeroDispensarios` registros. El usuario ajusta cuántas **posiciones de carga** tiene cada dispensario (1-2).

Grid de solo lectura para Isla/Dispensario; editable para `NumeroPosiciones`.

`DoInit` → llama a `mostrar()` que cruza `ListaIslaVol` del WorkItem:
```csharp
islas.ForEach(isla => {
    for (x=0; x < isla.NumeroDispensarios; x++) {
        l.Add(new DispensarioVol { Isla = isla.Clave, Clave = ++clave, NumeroPosiciones = 2 });
    }
});
```

Al avanzar, guarda `ListaDispensarioVol` en WorkItem.

---

### Paso 13: PPosicionesCarga — Generación de Posiciones de Carga

**Archivo:** `Pantallas/PosicionesCarga/PPosicionesCarga.cs`

**¿Qué hace?** Mismo patrón que Dispensarios pero un nivel más abajo. Cruza `ListaDispensarioVol` del WorkItem para generar posiciones de carga. El usuario ajusta cuántas **mangueras** tiene cada posición (1-3, default 2).

Grid: Isla | Dispensario | Posición de Carga | Número de Mangueras (editable).

Al avanzar, guarda `ListaPosicionCargaVol` en WorkItem.

---

### Paso 14: PMangueras — Asignación de Mangueras

**Archivo:** `Pantallas/Mangueras/PMangueras.cs`  
**Persistencia:** `Persistencia/Logica/MangueraVolPersistencia.cs`

**¿Qué hace?** El paso más crítico: asigna las propiedades de cada manguera física y las escribe en la BD.

**Grid generado** cruzando `ListaPosicionCargaVol` del WorkItem:

| Columna | Tipo | Descripción |
|---|---|---|
| Isla | solo lectura | |
| Dispensario | solo lectura | |
| Posición de Carga | solo lectura | |
| Manguera (clave) | solo lectura | número correlativo |
| Posición Física (`CON_POSICION`) | SpinEdit 1-4 | posición física en el dispensario |
| Combustible | ComboBox (de `ListaCombustibleVol`) | tipo de combustible asignado |
| Totalizador (`CampoLectura`) | ComboBox | `TOTAL01`, `TOTAL02`, `TOTAL03`, `TOTAL04` |

**Validaciones al avanzar (`datosSonValidos`):**
- Todas las mangueras deben tener combustible y totalizador asignado.
- Dentro de una misma posición de carga: sin `CON_POSICION` duplicado.
- Dentro de una misma posición de carga: sin `CAMPOLECTURA` duplicado.

**Botón "Replicar":** Copia la configuración de la primera manguera a todas las demás de la misma posición de carga.

**En `NextClick`:** Llama a `MangueraVolPersistencia.Guardar(lista)`:
```sql
DELETE FROM DPVGBOMB;
INSERT INTO DPVGBOMB (MANGUERA, POSCARGA, COMBUSTIBLE, ISLA, CON_PRECIO, CON_POSICION,
    CON_DIGITOAJUSTE, IMPRESORA, ACTIVO, IMPRIMEAUTOM, DIGITOAJUSTEPRECIO, CAMPOLECTURA,
    MODOOPERACION, TANQUE, IMPRETARJETAS, DIGITOSGILBARCO, DIGITOAJUSTEVOL, RFID,
    CLIENTE, VEHICULO, CONTROL_AROS, ERROR, MENSAJE_ERROR, DIGITOAJUSTEPRESET)
VALUES (...) -- una fila por manguera
```

- `CON_DIGITOAJUSTE` se toma de `VariablesVol` si la marca es Wayne (1) o Gilbarco (4).
- `DIGITOAJUSTEVOL` y `DIGITOSGILBARCO` solo para marca 4.
- `MODOOPERACION` = null para Team (marca 3); de `MarcaDispensarioVol` para otras.
- Crea conexión directa con `new Conexion(db.RutaBD)` (no usa la del WorkItem).

---

### Paso 15: PImpresora — Configuración de la Impresora de Tickets

**Archivo:** `Pantallas/Impresora/PImpresora.cs`  
**Persistencia:** `Persistencia/Logica/ImpresoraVolPersistencia.cs`

**¿Qué hace?** Configura la impresora de tickets:

- **Local:** selecciona de `PrinterSettings.InstalledPrinters` o "Ninguna".
- **Remoto:** habilita campo IP y ejecuta `NET USE {TipoPuerto}{Puerto} "\\{IP}\{Impresora}" /PERSISTENT:YES`.
- **Ninguna** → guarda `{ Impresora = "Impresora de texto", Puerto = C:\ImagenCo\Tmp\Ticket.txt }` (imprime a archivo).

**Escribe en BD:**
```sql
DELETE FROM DPVGIMPR;
INSERT INTO DPVGIMPR (CLAVE, NOMBRE, RUTA) VALUES (1, '{nombre}', '{puerto}');
UPDATE DPVGBOMB SET IMPRESORA = 1;
UPDATE DPVGCONF SET IMPRESORATICKETS = '{puerto}';
```

---

### Paso 16: PTurnosAdministrativos — Turnos Administrativos

**Archivo:** `Pantallas/TurnosAdministrativos/PTurnosAdministrativos.cs` + `Presenter.cs`  
**Persistencia:** `Persistencia/Logica/TurnosAdministrativosPersistencia.cs`

**¿Qué hace?** Configura los turnos del día (típicamente 3 turnos de 8 horas). Cada turno tiene:
- `Turno` (número: 1, 2, 3...)
- `HoraInicio`, `HoraFin` (TimeSpan)
- `HoraMin`, `HoraMax` (para tolerancia de inicio/fin)

La entidad `TurnoAdministrativo` implementa `INotifyPropertyChanged` con evento `ItemChanged` (con `ETurnoAdministrativo` para saber qué campo cambió).

**Lee de BD:** `SELECT TURNO, HORAINICIAL, HORAFINAL, HORAMIN, HORAMAX FROM DPVGTURC`

**Escribe:** `TurnosAdministrativosPersistencia.InsertarActualizar()` → INSERT o UPDATE en `DPVGTURC` dependiendo de si ya existen registros.

---

## 6. Módulo de Infraestructura — Generales/

### 6.1 Constantes.cs

Todas las rutas del sistema se definen aquí como propiedades estáticas que construyen el path concatenando `Root` o rutas fijas:

```csharp
static string Root { get; set; }                         // = Application.StartupPath
static string RutaBDE         → {Root}\util\x86\BDE\setup.exe
static string RutaFirebird64  → {Root}\util\x64\Firebird\Firebird-2.5.2.26540_0_x64.exe
static string RutaFirebird32  → {Root}\util\x86\Firebird\Firebird-2.5.2.26540_0_Win32.exe
static string RutaGasConsola  → {Root}\BD\GasConsola.fdb
static string RutaComPort     → C:\ImagenCo\PrgGas\ComPort\ComPortIgas.exe
static string RutaScripts     → {Root}\Scripts\
static string RutaImagenCo    → C:\ImagenCo   (HARDCODED)
static string RutaImagenCoTmp → C:\ImagenCo\tmp
static string RutaVolumetricos → C:\ImagenCo\prgGas\Volumetricos
```

> ⚠️ `RutaImagenCo` está **hardcodeada** como `C:\ImagenCo`. Todos los paths de los programas I-Gas dependen de esto. Si el cliente tiene I-Gas instalado en otra unidad, el wizard fallará silenciosamente (los accesos directos apuntarán a rutas inexistentes).

**Mensajes y Constantes de UI** están en clases internas anidadas: `MENSAJES`, `REQUERIMIENTOS`, `MARCATANQUE`, `TANQUE`, etc.

### 6.2 Utilerias.cs

Métodos estáticos clave:

| Método | Descripción |
|---|---|
| `Arquitectura()` | Ejecuta `{Root}\bits.exe`, lee stdout, retorna `32` o `64` |
| `ManejaAlias(opcion, alias, ruta)` | Ejecuta `{Root}\CmdAlias.exe {opcion} {alias} {ruta}`, retorna el código de salida como int |
| `ComPortIgas(params)` | Ejecuta `ComPortIgas.exe {params}` para probar puerto COM |
| `CancelarInstalacion()` | `msiexec /passive /x {CodigoProducto}` — solo si se lanzó como parte de MSI |
| `CombinarRutas(...)` | Concatena paths dividiendo por `\` y re-uniendo sin dobles separadores |
| `Confirmacion(msg)` | `MessageBox` con Sí/No |
| `Error(msg/ex)` | `MessageBox` con ícono de error, muestra stack completo si es excepción |

**`IniParser`**: Lee/escribe archivos `.ini` (secciones `[Section]`, pares `Key=Value`). Las claves se guardan en `Hashtable` usando un struct `SectionPair` como clave. **Importante:** todo se convierte a MAYÚSCULAS internamente.

**`DllManager`**: Carga/descarga DLLs nativas vía `LoadLibrary`/`FreeLibrary` del kernel32. Era usado para `AliasBDE.dll` y `AliasBDE.dll` con funciones de licencia. Los comentarios muestran que este enfoque fue reemplazado por procesos externos.

### 6.3 WorkItem.cs

Ver sección 4.2. Adicionalmente:

- `WorkItem.Status` (InstallStatus): se pone `Successful` al inicio, `Cancelled` si el usuario cierra/cancela. El MSI puede leer este estado.
- `WorkItem.Pantallas` es el array de instancias de UserControl creadas **en el constructor** (al arrancar). Esto significa que los 16 `UserControl` se instancian a la vez en startup.

### 6.4 Read64bitRegistryFrom32bitApp.cs

Cuando el proceso del wizard corre como 32-bit en un OS de 64-bit, el registro redirige `HKLM\SOFTWARE` al hive WOW6432Node. Para leer claves del hive de 64-bit (donde vive Firebird 64-bit), se usa P/Invoke directo a `Advapi32.dll`:

```csharp
RegOpenKeyEx(hKey, keyName, 0, RegSAM.QueryValue | RegSAM.WOW64_64Key, out hkey)
RegOpenKeyEx(hKey, keyName, 0, RegSAM.QueryValue | RegSAM.WOW64_32Key, out hkey)
```

Esto explica por qué `validarFirebird25(bits)` usa `RegistryWOW6432.ExistRegKey64` o `ExistRegKey32` según `bits`.

---

## 7. Capa de Persistencia

### 7.1 Conexion.cs

`Conexion` es la clase wrapper de Firebird. Se guarda en WorkItem y se reutiliza entre pantallas.

**Connection string:**
```
Database = {RutaBD}
DataSource = 127.0.0.1
Port = 3050
UserID = SYSDBA
Password = masterkey
Dialect = 1
Charset = NONE
Pooling = true
MinPoolSize = 1
MaxPoolSize = 10
ClientLibrary = {CurrentDirectory}\fbclient.dll
```

> **Importante:** `ServerType = FbServerType.Default` (servidor clásico/super), **no Embedded**. La instancia de Firebird debe estar corriendo como servicio.

**`ConectarBDConsola(Action<FbCommand> callback)`**: Abre conexión, inicia transacción, ejecuta el callback, hace commit. Si falla, rollback. Retorna string de error vacío si todo salió bien.

**`ExcecutePathScript(string fPath)`**: Ejecuta todos los `.sql` de una carpeta con `FbScript` + `FbBatchExecution`.

**`Dispose()`**: Llama `FbConnection.ClearAllPools()` y `GC.Collect()`.

**`LimpiarConexiones()`** (static): `FbConnection.ClearAllPools()` — útil al cambiar de BD.

### 7.2 Tablas de BD usadas

| Tabla | Descripción | Operaciones |
|---|---|---|
| `DPVGTCMB` | Combustibles (fuels) — datos maestros | SELECT (validar BD + cargar lista) |
| `DPVGCONF` | Configuración volumétrica | SELECT, INSERT, UPDATE |
| `DGENEMPR` | Empresa | SELECT, INSERT, UPDATE |
| `DPVGESTS` | Estación + campo CONSOLA (key-value blob) | SELECT, INSERT, UPDATE |
| `DPVGBOMB` | Mangueras/bombas — configuración física | DELETE + INSERT (reemplaza todo) |
| `DPVGIMPR` | Impresora de tickets | DELETE + INSERT |
| `DPVGTURC` | Turnos administrativos | SELECT, INSERT, UPDATE |
| `RDB$RELATIONS` | Sistema Firebird | SELECT (verificar que BD es GasConsola) |

### 7.3 Entidades (POCOs)

| Clase | Descripción |
|---|---|
| `UbicarBD` | `{Alias, RutaBD}` — configuración de ubicación de BD |
| `Empresa` | RFC, Dirección, Población, NombreComercial, NombreEstacion, PEMEX |
| `Licencia` | RazonSocial, NoSentinel, licencias CVOL/CVOL02/CVAC, fechas |
| `Estacion` | Clave, Nombre, NumeroEstacion |
| `EmpresaConfiguracion` | Clase "fat" que agrega Empresa + Licencia; usada como DTO entre pantallas |
| `Requerimiento` | Descripcion, Instalado, Icono (Image) |
| `ListaRequerimiento` | `List<Requerimiento>` con prop `SonValidos` |
| `MarcaDispensarioVol` | Marca (int), TipoInterfaz, ModoOperacion, COM settings, ReconexionAros |
| `TipoMarcaDispensarioVol` | Clave, Nombre (catálogo de marcas) |
| `TipoInterfazDispensarioVol` | Clave, Nombre (catálogo de interfaces) |
| `VariablesVol` | Todos los parámetros numéricos de Wayne y Gilbarco |
| `CombustibleVol` | Clave, Nombre (combustible) |
| `ListaCombustibleVol` | `List<CombustibleVol>` |
| `IslaVol` | Clave, NumeroDispensarios |
| `DispensarioVol` | Clave, Isla, NumeroPosiciones |
| `PosicionCargaVol` | Clave, Isla, Dispensario, NumeroMangueras |
| `MangueraVol` | Isla, Dispensario, PosicionCarga, Clave, ConPosicion, CampoLectura, Combustible |
| `ImpresoraVol` | Impresora, Puerto, TipoPuerto, IP, Remoto, EjecutarNetUse |
| `TurnoAdministrativo` | Turno, HoraInicio, HoraFin, HoraMin, HoraMax; implementa INotifyPropertyChanged |
| `OpcionesArchivos` | Usado por PProcesando: Nombre, RutaExe, FilePath, Configuracion, Tipo |
| `DiccionarioProcesando` | `Dictionary<string, OpcionesArchivos>` — shortcut names → configuración |

---

## 8. Herramientas Externas Requeridas

El wizard **no es standalone**. Requiere los siguientes ejecutables junto a él o en rutas fijas:

| Ejecutable | Ubicación | Propósito | Retorna |
|---|---|---|---|
| `bits.exe` | `{Root}\bits.exe` | Detecta arquitectura del OS | `"32"` ó `"64"` en stdout |
| `CmdAlias.exe` | `{Root}\CmdAlias.exe` | Gestiona aliases BDE | `0` = fallo, `1` = éxito |
| `LibsDelphi.exe` | `{Root}\LibsDelphi.exe` | Valida licencias CVOL/CVAC | `"Valido"` ó `"1"` en stdout |
| `ComPortIgas.exe` | `C:\ImagenCo\PrgGas\ComPort\ComPortIgas.exe` | Prueba puertos COM | (no se captura salida) |
| `fbclient.dll` | `{CurrentDirectory}\fbclient.dll` | Cliente Firebird nativo | — |
| `Firebird-2.5.2.26540_0_Win32.exe` | `{Root}\util\x86\Firebird\` | Instalador Firebird 32-bit | (bloqueante) |
| `Firebird-2.5.2.26540_0_x64.exe` | `{Root}\util\x64\Firebird\` | Instalador Firebird 64-bit | (bloqueante) |
| `util\x86\BDE\setup.exe` | `{Root}\util\x86\BDE\` | Instalador BDE | (bloqueante) |
| `GasConsola.fdb` | `{Root}\BD\GasConsola.fdb` | Plantilla de BD vacía | — |
| `Scripts\*.sql` | `{Root}\Scripts\` | Scripts de actualización | — |

> `bits.exe` es el proyecto `bits/` de esta misma solución. Se compila aparte y se copia manualmente a la carpeta de distribución del wizard.

---

## 9. Dependencias entre Pantallas — Flujo de Datos en WorkItem

```
Paso 2 (UbicarBD) → WorkItem<UbicarBD>
    ↓ usa: Paso 3, 14, 15, Wayne, Gilbarco (conexiones directas)

Paso 3 (Procesando) NextClick → WorkItem<ListaCombustibleVol>
    ↓ usa: Paso 9 (Wayne) — selección de producto por combustible
    ↓ usa: Paso 14 (Mangueras) — asignación de combustible

Paso 4 (Licenciamiento) → WorkItem<Licencia>, WorkItem<EmpresaConfiguracion> (parcial)
    ↓ usa: Paso 5 (DatosEmpresa) — pre-carga licencia existente

Paso 5 (DatosEmpresa) → WorkItem<EmpresaConfiguracion> (completo) + BD escrita
    ↓ usa: ninguno posterior (datos ya en BD)

Paso 8 (MarcaDispensario) → WorkItem<MarcaDispensarioVol>
    ↓ usa: Paso 9, 10 — parámetros específicos de marca
    ↓ usa: Paso 14 (Mangueras) — CON_DIGITOAJUSTE, MODOOPERACION, DIGITOSGILBARCO

Paso 9 (Wayne) / Paso 10 (Gilbarco) → WorkItem<VariablesVol>
    ↓ usa: Paso 14 (Mangueras) — digitosGilbarco, digitoAjusteVol, con_DigitoAjuste

Paso 11 (Islas) → WorkItem<ListaIslaVol>
    ↓ usa: Paso 12 (Dispensarios) — genera tabla de dispensarios

Paso 12 (Dispensarios) → WorkItem<ListaDispensarioVol>
    ↓ usa: Paso 13 (PosicionesCarga) — genera posiciones

Paso 13 (PosicionesCarga) → WorkItem<ListaPosicionCargaVol>
    ↓ usa: Paso 14 (Mangueras) — genera grid de mangueras

Paso 14 (Mangueras) → BD escrita (DPVGBOMB borrado + insertado)

Paso 15 (Impresora) → BD escrita (DPVGIMPR + DPVGBOMB.IMPRESORA + DPVGCONF.IMPRESORATICKETS)

Paso 16 (Turnos) → BD escrita (DPVGTURC)
```

---

## 10. Sub-proyecto `bits/`

Proyecto C# de consola minimalista:

```csharp
static void Main(string[] args)
{
    int bits = IntPtr.Size * 8;
    Console.Write(bits.ToString());  // "32" ó "64", sin salto de línea
}
```

**Criticidad:** Si no existe `bits.exe` en `Root`, `Utilerias.Arquitectura()` captura la excepción y retorna `32`. En un sistema de 64-bit, esto hace que se busque Firebird en la clave de registro de 32-bit → fallo de detección.

**Distribución:** Este ejecutable debe copiarse manualmente (o incluirse en el MSI) junto al `.exe` principal del wizard.

---

## 11. Sub-proyecto `AliasBDE/` (Legacy)

Proyecto Delphi que generaba una DLL (`AliasBDE.dll`) con funciones de manejo de aliases BDE y validación de licencias. **Ya no se usa directamente** — los `[DllImport]` están todos comentados en el código C#. Fue reemplazado por:
- `CmdAlias.exe` para aliases BDE.
- `LibsDelphi.exe` para validación de licencias.

Se mantiene en la solución como referencia histórica.

---

## 12. Bugs Conocidos y Fragilidades

### Bug 1 — `bits.exe` no encontrado → falso negativo en Firebird
**Síntoma:** El wizard dice "Firebird no está instalado" aunque sí lo esté (64-bit).  
**Causa:** `bits.exe` no está en `Root` → `Arquitectura()` retorna 32 → se busca clave 32-bit en registro → Firebird 64 no aparece ahí.  
**Fix:** Asegurar que `bits.exe` se distribuya junto al wizard. O cambiar `Arquitectura()` para usar `Environment.Is64BitOperatingSystem` como fallback.

### Bug 2 — `Panel.ini` no existe → falla `EditShortcut`
**Síntoma:** El paso 3 (Procesando) falla con error de archivo no encontrado.  
**Causa:** `IniParser` lanza `FileNotFoundException` si `{Volumetricos}\PANEL 2.0\Panel.ini` no existe. Esto ocurre cuando I-Gas Controlador no está instalado todavía.  
**Fix:** Verificar existencia del archivo antes de intentar parsear. Si no existe, crear con sección `[config]` y clave `db` vacía.

### Bug 3 — Rutas hardcodeadas a `C:\ImagenCo`
**Síntoma:** El wizard crea accesos directos apuntando a `C:\ImagenCo\...` aunque I-Gas esté instalado en otra unidad.  
**Causa:** `RutaImagenCo = "C:\\ImagenCo"` está literal en `Constantes.cs`.  
**Fix:** Leer la ruta de instalación desde el registro de Windows o desde un archivo de configuración.

### Fragilidad — WorkItem con instancias creadas en startup
Todos los `UserControl` se instancian en el constructor de `WorkItem` (y por ende al iniciar el wizard). Si alguno tiene código costoso o que falla en el constructor (ej: acceso a BD, recursos de sistema), el wizard ni siquiera arranca. Esto ocurrió con DevExpress CAS API en versiones antiguas.

### Fragilidad — Conexión Firebird en WorkItem
La `Conexion` del WorkItem no tiene timeout agresivo. Si Firebird no está corriendo, `UbicarBD.PruebaDeConexion()` puede bloquearse hasta el timeout por defecto (30s). Solo cuando `EsPrueba = true` se usa un timeout de 5s.

---

## 13. Recursos Embebidos (Properties/Resources)

Las imágenes de `Resources/` se embeben como recursos y se acceden vía `iGasWizardVolumetricos.Properties.Resources.*`:

| Recurso | Uso |
|---|---|
| `check.png` | Requisito instalado |
| `delete.png` | Requisito faltante |
| `wait_16x16.gif` | Instalando (animación) |
| `Sentinel-Volumetrico.png` | Guía visual en pantalla de licenciamiento |
| `FechaVencimiento.png` | Guía visual para fechas |
| `RazonSocial.png` | Guía visual para razón social |
| `Licencia.png` | Guía visual para código de licencia |
| `loading.gif` | Animación de carga en PRequerimientos |
| `logo.png` | Logo de la aplicación |
| `dispensario_192x332.png` | Imagen referencial dispensario |
| `database_server1.jpg` | Imagen referencial BD |

---

## 14. Cómo Agregar un Nuevo Paso al Wizard

1. Crear carpeta en `Pantallas/NuevoPaso/`.
2. Crear `PNuevoPaso.cs` (hereda de `XtraUserControl`, implementa `IUserPages`).
3. Crear `PNuevoPaso.Designer.cs` (generado por Visual Studio designer).
4. (Opcional) Crear `Presenter.cs` para lógica de negocio.
5. (Opcional) Crear entidad en `Persistencia/Entidades/` y clase de persistencia en `Persistencia/Logica/`.
6. **Registrar en `WorkItem.pantallas`:** Agregar `new PNuevoPaso()` en la posición correcta del array.
7. Si necesita datos de pasos anteriores, leerlos con `WorkItem.Objetos<T>.Get()`.
8. Si produce datos para pasos posteriores, guardarlos con `WorkItem.Objetos<T>.Add(inst)`.

---

## 15. Referencia Rápida de Archivos

Al recibir un archivo aislado, este mapa permite identificarlo de inmediato:

| Si el archivo es... | Es... | Afecta... |
|---|---|---|
| `Constantes.cs` | Todas las rutas/mensajes | TODO el proyecto — cambiar aquí cambia rutas en todos los pasos |
| `Utilerias.cs` | Helpers + IniParser + DllManager | Paso 1 (bits.exe), Paso 2 (CmdAlias.exe), Paso 3 (Panel.ini), Paso 4 (LibsDelphi.exe), Paso 8 (ComPort) |
| `WorkItem.cs` | Orden de pantallas + estado global | El orden del wizard y toda la comunicación entre pasos |
| `Wizard.cs` | Contenedor del wizard | Navegación Next/Prev/Finish/Cancel |
| `Conexion.cs` | Acceso Firebird | Todos los pasos que leen/escriben BD |
| `PRequerimientos.cs` | Paso 1 | Sin dependencias con otros pasos; lee registro |
| `PUbicarBD.cs` / `UbicarBDPersistencia.cs` | Paso 2 | Fuente del WorkItem<UbicarBD> que usan los pasos 3, 14, 15, Wayne, Gilbarco |
| `PProcesando.cs` / `ProcesandoPersistencia.cs` | Paso 3 | Ejecuta scripts SQL, crea accesos directos, actualiza DPVGCONF |
| `PLicenciamiento.cs` | Paso 4 | Llama LibsDelphi.exe, alimenta EmpresaConfiguracion para paso 5 |
| `PDatosEmpresa.cs` / `EmpresaConfiguracionPersistencia.cs` | Paso 5 | Escribe DGENEMPR, DPVGCONF, DPVGESTS |
| `PMarcaDispensario.cs` | Paso 8 | Define la marca que condiciona Wayne (9), Gilbarco (10) y la escritura en DPVGBOMB |
| `PWayne.cs` / `PGilbarco.cs` | Pasos 9/10 | Variables que afectan campos en DPVGBOMB al guardar mangueras |
| `PIslas.cs` | Paso 11 | Fuente de la jerarquía Isla→Dispensario→PosicionCarga→Manguera |
| `PMangueras.cs` / `MangueraVolPersistencia.cs` | Paso 14 | **Paso más crítico** — escribe DPVGBOMB con toda la config física |
| `PImpresora.cs` / `ImpresoraVolPersistencia.cs` | Paso 15 | Escribe DPVGIMPR, actualiza DPVGBOMB.IMPRESORA y DPVGCONF.IMPRESORATICKETS |
| `PTurnosAdministrativos.cs` / `TurnosAdministrativosPersistencia.cs` | Paso 16 | Escribe DPVGTURC |
| Cualquier archivo `*Persistencia.cs` en `Logica/` | DAO — solo BD | Su tabla correspondiente en Firebird |
| Cualquier archivo en `Persistencia/Entidades/` | POCO | La pantalla y la persistencia con el mismo nombre sin "Persistencia" |
