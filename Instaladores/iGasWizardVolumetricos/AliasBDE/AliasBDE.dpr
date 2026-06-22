library AliasBDE;

{ Important note about DLL memory management: ShareMem must be the
  first unit in your library's USES clause AND your project's (select
  Project-View Source) USES clause if your DLL exports any procedures or
  functions that pass strings as parameters or function results. This
  applies to all strings passed to and from your DLL--even those that
  are nested in records and classes. ShareMem is the interface unit to
  the BORLNDMM.DLL shared memory manager, which must be deployed along
  with your DLL. To avoid using BORLNDMM.DLL, pass string information
  using PChar or ShortString parameters. }

uses
  SysUtils,
  DBTables,
  Dialogs,
  Classes,
  DECUtil,
  Cipher,
  Hash,
  ActiveX,
  ULibLicencias;

function CrearAliasBDE(var alias: PAnsiChar; var ruta: PAnsiChar): Boolean; stdcall;
var
  sesion     : TSession;
  parametros : TStringList;
begin
  sesion := TSession.Create(nil);
  parametros := TStringList.create;
  try
    try
      sesion.AutoSessionName := True;

      parametros.Values['SERVER NAME'] := ruta;
      parametros.Values['USER NAME'] := 'SYSDBA';
      sesion.AddAlias(alias, 'INTRBASE', parametros) ;
      sesion.SaveConfigFile;
      Result := True;
    except
      Result := False;
    end
  finally
    sesion.Free;
    parametros.Free;
  end;
end;

function ObtenerAliasBDE(var alias: PAnsiChar) : PAnsiChar; stdcall;
var
  sesion     : TSession;
  parametros : TStringList;
begin
  sesion := TSession.Create(nil);
  parametros := TStringList.Create;
  try
    try
      sesion.AutoSessionName := True;
      sesion.GetAliasParams(alias, parametros);
      Result := parametros.GetText;
    except
      Result := nil;
    end;
  finally
    sesion.Free;
    parametros.Free;
  end;
end;

function ExisteAliasBDE(var alias: PAnsiChar) : Boolean; stdcall;
var
  sesion : TSession;
  lista  : TStringList;
  x      : Integer;
begin
  sesion := TSession.Create(nil);
  lista  := TStringList.Create;
  Result := False;

  try
    sesion.AutoSessionName := True;
    sesion.GetAliasParams(alias, lista);

    for x := 0 to lista.Count - 1 do begin
      if alias = lista[x] then begin
        Result := True;
        Break;
      end;
    end;
  finally
    sesion.Free;
    lista.Free;
  end;
end;

function EliminarAliasBDE(var alias : PAnsiChar) : Boolean; stdcall;
var
  sesion : TSession;
begin
  sesion := TSession.Create(nil);
  try
    try
      sesion.AutoSessionName := True;
      sesion.DeleteAlias(alias);
      Result := True;
    except
      Result := False;
    end;
  finally
    sesion.Free;
  end;
end;

function ActualizarAliasBDE(var alias: PAnsiChar; var values: TStrings) : Boolean; stdcall;
var
   sesion : TSession;
begin
  sesion := TSession.Create(nil);
  try
    try
      sesion.AutoSessionName := True;
      sesion.ModifyAlias(alias, values);
      Result := True;
    except
      Result := False;
    end;
  finally
    sesion.Free;
  end;
end;

function EncriptaCadena(var APassword: PAnsiChar; var ATexto: PAnsiChar): PAnsiChar; stdcall; export;
begin
  with TCipher_Blowfish.Create(APassword, nil) do begin
    try
      Result := PAnsiChar(CodeString(ATexto, paEncode, TStringFormat_HEX.Format));
    finally
      Free;
    end;
  end;
end;

function DesencriptaCadena(var APassword: PAnsiChar; var ATexto: PAnsiChar): PAnsiChar; stdcall; export;
begin
  with TCipher_Blowfish.Create(APassword, nil) do begin
    try
      Result := PAnsiChar(CodeString(ATexto, paDecode, TStringFormat_HEX.Format));
    finally
      Free;
    end;
  end;
end;

function LicenciaValidaDLL(var RazonSocial: PAnsiChar;
                           var Sistema: PAnsiChar;
                           var Version: PAnsiChar;
                           var TIpoLicencia: PAnsiChar;
                           var Licencia: PAnsiChar;
                           var Usuarios: Integer;
                           var Limitada: Boolean;
                           var FechaVence: TDateTime): PAnsiChar; stdcall; export;
begin
  try
    if(LicenciaValida2(RazonSocial, Sistema, Version,TIpoLicencia, Licencia, Usuarios, Limitada, FechaVence))then begin
      Result := 'Valido';
    end else begin
      Result := 'No valido';
    end;
  except
    on e:Exception do begin
      Result := PAnsiChar('Error: ' + e.Message);
    end;
  end;
end;

function ValidaSentinel(var serie: Integer): PAnsiChar; stdcall; export;
begin
  try
    PlanMDog_Ver31_CVOL(serie);
    Result := 'Valida';
  except
    on e:Exception do begin
      Result := PAnsiChar('Error: ' + e.Message);
    end;
  end;
end;

{$R *.res}

exports
       CrearAliasBDE,
       ObtenerAliasBDE,
       ExisteAliasBDE,
       EliminarAliasBDE,
       ActualizarAliasBDE,
       EncriptaCadena,
       DesencriptaCadena,
       ValidaSentinel,
       LicenciaValidaDLL;
begin
end.

