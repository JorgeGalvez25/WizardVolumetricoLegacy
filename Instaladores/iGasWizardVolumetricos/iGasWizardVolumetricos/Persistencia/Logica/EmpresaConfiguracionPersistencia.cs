using System;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Persistencia.Entidades;

namespace iGasWizardVolumetricos.Persistencia.Logica
{
    public class EmpresaConfiguracionPersistencia
    {
        private int MaxEstacion;
        private int MaxEmpresa;
        private StringBuilder _sbError;
        public string MensajeError { get { return _sbError.ToString(); } }

        public EmpresaConfiguracionPersistencia()
        {
            this.MaxEmpresa =
                this.MaxEstacion = 0;
            this._sbError = new StringBuilder();
        }

        public EmpresaConfiguracion Obtener()
        {
            EmpresaConfiguracion result = null;
            {
                var conn = WorkItem.Objetos<Conexion.Conexion>.Get();
                this._sbError.AppendLine(conn.ConectarBDConsola((comm) =>
                    {
                        comm.CommandText = "SELECT EM.RFC, " +
                                                  "EM.DIRECCION, " +
                                                  "EM.POBLACION, " +
                                                  "EM.ESTACION_IGAS, " +
                                                  "EM.SERIEKEY, " +
                                                  "CF.NOMBRECOMERCIAL, " +
                                                  "CF.CLAVEPEMEX, " +
                                                  "CF.USUARIOPEMEX, " +
                                                  "CF.ESTEMPORAL, " +
                                                  "CF.FECHAVENCE, " +
                                                  "CF.LICENCIA, " +
                                                  "CF.RAZONSOCIAL, " +
                                                  "ES.NOMBRE " +
                                             "FROM DGENEMPR EM, " +
                                                  "DPVGCONF CF, " +
                                                  "DPVGESTS ES ";
                        comm.Parameters.Clear();

                        using (var reader = comm.ExecuteReader())
                        {
                            try
                            {
                                if (reader.Read())
                                {
                                    result = ReaderToentidad(reader);
                                }
                            }
                            finally
                            {
                                if (!reader.IsClosed) { reader.Close(); }
                            }
                        }
                    }));
            }
            return result;
        }
        private EmpresaConfiguracion ReaderToentidad(FbDataReader reader)
        {
            EmpresaConfiguracion conf = new EmpresaConfiguracion();
            {
                conf.RFC = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                conf.Direccion = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                conf.Poblacion = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                conf.NumEstacion = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                conf.NoSentinel = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                conf.NombreComercial = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                conf.ClavePEMEX = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                conf.UsuarioPEMEX = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                conf.EsTemporal = reader.IsDBNull(8) ? false : reader.GetString(8).Equals("Si", StringComparison.CurrentCultureIgnoreCase);
                conf.FechaVence = reader.IsDBNull(9) ? null : (DateTime?)reader.GetDateTime(9);
                conf.LicenciaVolumetrico = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                conf.RazonSocial = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                conf.NombreEstacion = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
            }

            return conf;
        }

        public int CantidadEmpresa()
        {
            int result = 0;
            var conn = WorkItem.Objetos<Conexion.Conexion>.Get();
            this._sbError.AppendLine(conn.ConectarBDConsola((comm) =>
                {
                    comm.CommandText = "SELECT COALESCE(MAX(CLAVE), 0) " +
                                         "FROM DGENEMPR";
                    comm.Parameters.Clear();

                    using (var reader = comm.ExecuteReader())
                    {
                        try
                        {
                            if (reader.Read())
                            {
                                result = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                this.MaxEmpresa = result + 1;
                            }
                        }
                        finally
                        {
                            if (!reader.IsClosed) { reader.Close(); }
                        }
                    }
                }));
            return result;
        }
        public int CantidadEstacion()
        {
            int result = 0;
            var conn = WorkItem.Objetos<Conexion.Conexion>.Get();
            this._sbError.AppendLine(conn.ConectarBDConsola((comm) =>
                {
                    comm.CommandText = "SELECT COALESCE(MAX(CLAVE), 0) " +
                                         "FROM DPVGESTS";
                    comm.Parameters.Clear();

                    using (var reader = comm.ExecuteReader())
                    {
                        try
                        {
                            if (reader.Read())
                            {
                                result = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                this.MaxEstacion = result + 1;
                            }
                        }
                        finally
                        {
                            if (!reader.IsClosed) { reader.Close(); }
                        }
                    }
                }));
            return result;
        }
        public int CantidadConfiguracion()
        {
            int result = 0;
            var conn = WorkItem.Objetos<Conexion.Conexion>.Get();
            this._sbError.AppendLine(conn.ConectarBDConsola((comm) =>
                {
                    comm.CommandText = "SELECT COUNT(RAZONSOCIAL) " +
                                         "FROM DPVGCONF";
                    comm.Parameters.Clear();

                    using (var reader = comm.ExecuteReader())
                    {
                        try
                        {
                            if (reader.Read())
                            {
                                result = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            }
                        }
                        finally
                        {
                            if (!reader.IsClosed) { reader.Close(); }
                        }
                    }
                }));
            return result;
        }

        public EmpresaConfiguracion Insertar(EmpresaConfiguracion datos)
        {

            var lic = this.InsertarLicencia(datos);
            if (lic == null) { return null; }

            var emp = this.InsertarEmpresa(datos);
            if (emp == null) { return null; }

            var est = this.InsertarEstacion(datos);
            if (est == null) { return null; }

            return datos;
        }
        public EmpresaConfiguracion Actualizar(EmpresaConfiguracion datos)
        {
            var lic = this.ActualizarLicencia(datos);
            if (lic == null) { return null; }

            var emp = this.ActualizarEmpresa(datos);
            if (emp == null) { return null; }

            var est = this.ActualizarEstacion(datos);
            if (est == null) { return null; }

            return datos;
        }
        public EmpresaConfiguracion ActualizarOInsertar(EmpresaConfiguracion datos)
        {
            if (this.CantidadConfiguracion() <= 0) { this.InsertarLicencia(datos); }
            if (this.CantidadEmpresa() <= 0) { this.InsertarEmpresa(datos); }
            if (this.CantidadEstacion() <= 0) { this.InsertarEstacion(datos); }

            return this.Actualizar(datos);
        }

        private Empresa ActualizarEmpresa(EmpresaConfiguracion datos)
        {
            var conn = WorkItem.Objetos<Conexion.Conexion>.Get();
            bool result = false;
            var emp = datos.GetEmpresa();
            var lic = datos.GetLicencia();

            this._sbError.AppendLine(conn.ConectarBDConsola((comm) =>
                {
                    comm.CommandText = "UPDATE DGENEMPR SET RFC = @RFC, " +
                                                           "DIRECCION = @DIRECCION, " +
                                                           "POBLACION = @POBLACION, " +
                                                           "ESTACION_IGAS = @ESTACION_IGAS, " +
                                                           "SERIEKEY = @SERIEKEY, " +
                                                           "NOMCOMERCIAL = @NOMCOMERCIAL, " +
                                                           "RAZONSOCIAL = @RAZONSOCIAL, " +
                                                           "TIPOLICENCIA = @TIPOLICENCIA, " +
                                                           "CLAVEACTUALIZACION = @LICENCIACVAC, " +
                                                           "FECHAACTUALIZACION = @FECHACVAC " +
                                                     "WHERE CLAVE = @CLAVE";
                    comm.Parameters.Clear();

                    comm.Parameters.Add("@CLAVE", 1);
                    comm.Parameters.Add("@RFC", emp.RFC);
                    comm.Parameters.Add("@DIRECCION", emp.Direccion);
                    comm.Parameters.Add("@POBLACION", emp.Poblacion);
                    comm.Parameters.Add("@ESTACION_IGAS", 1);
                    comm.Parameters.Add("@SERIEKEY", lic.NoSentinel);
                    comm.Parameters.Add("@NOMCOMERCIAL", emp.NombreComercial);
                    comm.Parameters.Add("@RAZONSOCIAL", lic.RazonSocial);
                    comm.Parameters.Add("@TIPOLICENCIA", lic.TipoLicencia);

                    comm.Parameters.Add("@FECHACVAC", lic.FechaVenceControlVersiones.GetValueOrDefault());
                    comm.Parameters.Add("@LICENCIACVAC", lic.LicenciaControlVersiones);

                    result = comm.ExecuteNonQuery() >= 1;
                }));

            return result ? emp : null;
        }
        private Licencia ActualizarLicencia(EmpresaConfiguracion datos)
        {
            var conn = WorkItem.Objetos<Conexion.Conexion>.Get();
            bool result = false;
            var lic = datos.GetLicencia();

            this._sbError.AppendLine(conn.ConectarBDConsola((comm) =>
                {
                    comm.CommandText = "UPDATE DPVGCONF SET NOMBRECOMERCIAL = @NOMBRECOMERCIAL, " +
                                                           "CLAVEPEMEX = @CLAVEPEMEX, " +
                                                           "USUARIOPEMEX = @USUARIOPEMEX, " +
                                                           "ESTEMPORAL = @ESTEMPORAL, " +
                                                           "FECHAVENCE = @FECHAVENCE, " +
                                                           "LICENCIA = @LICENCIA, " +
                                                           "RAZONSOCIAL = @RAZONSOCIAL, " +
                                                           "NUMEROSERIE = @NUMEROSERIE, " +
                                                           "ESTACION_IGAS = @ESTACION_IGAS ";
                    comm.Parameters.Clear();
                    comm.Parameters.AddRange(new[]
                        {
                            new FbParameter("@ESTACION_IGAS", FbDbType.Integer) { Value = 1 },
                            new FbParameter("@NOMBRECOMERCIAL", FbDbType.VarChar) { Value = datos.NombreComercial.Trim() },
                            new FbParameter("@CLAVEPEMEX", FbDbType.VarChar) { Value = datos.ClavePEMEX.Trim() },
                            new FbParameter("@USUARIOPEMEX", FbDbType.VarChar) { Value = datos.UsuarioPEMEX.Trim() },
                            new FbParameter("@NUMEROSERIE", FbDbType.Integer) { Value = lic.NoSentinel },
                            new FbParameter("@ESTEMPORAL", FbDbType.VarChar) { Value = lic.EsTemporal ? "Si" : "No" },
                            new FbParameter("@FECHAVENCE", FbDbType.Date) { Value = lic.FechaVence.HasValue ? lic.FechaVence : (object)DBNull.Value }, // (lic.FechaVence == null ? DBNull.Value : (object)lic.FechaVence) });
                            new FbParameter("@LICENCIA", FbDbType.VarChar) { Value = lic.LicenciaVolumetrico.Trim() },
                            new FbParameter("@RAZONSOCIAL", FbDbType.VarChar) { Value = lic.RazonSocial.Trim() }
                        });
                    //using (FbDataAdapter da = new FbDataAdapter("select * from dpvgconf", comm.Connection.ConnectionString))
                    //{
                    //    DataTable tablaesquema = new DataTable();
                    //    tablaesquema = da.FillSchema(tablaesquema, SchemaType.Mapped);
                    //    var x = tablaesquema.Columns;
                    //}


                    result = comm.ExecuteNonQuery() >= 1;
                }));

            return result ? lic : null;
        }
        private Estacion ActualizarEstacion(EmpresaConfiguracion datos)
        {
            var conn = WorkItem.Objetos<Conexion.Conexion>.Get();
            bool result = false;

            this._sbError.AppendLine(conn.ConectarBDConsola((comm) =>
                {
                    comm.CommandText = "UPDATE DPVGESTS SET CLAVE = @CLAVE, " +
                                                           "NOMBRE = @NOMBRE, " +
                                                           "NUMEROESTACION = @NUMEROESTACION, " +
                                                           "INOCUIDAD2 = @INOCUIDAD2";
                    comm.Parameters.Clear();
                    comm.Parameters.Add("@CLAVE", this.MaxEstacion <= 0 ? 1 : this.MaxEstacion);
                    comm.Parameters.Add("@NOMBRE", datos.NombreEstacion);
                    comm.Parameters.Add("@NUMEROESTACION", datos.NumEstacion);
                    comm.Parameters.Add("@INOCUIDAD2", "K?KL?>;MPK=PG?G;;>?H>;O:>;LIMLKH");
                    result = comm.ExecuteNonQuery() >= 1;
                    comm.Transaction.CommitRetaining();

                    if (result)
                    {
                        result = ExecuteLicInoc(comm, datos.GetLicencia());
                    }
                }));

            Estacion est = new Estacion();
            {
                est.Clave = 1;
                est.Nombre = datos.NombreEstacion;
                est.NumeroEstacion = datos.NumEstacion;
            }

            return result ? est : null;
        }

        private Empresa InsertarEmpresa(EmpresaConfiguracion datos)
        {
            var conn = WorkItem.Objetos<Conexion.Conexion>.Get();
            bool result = false;
            var emp = datos.GetEmpresa();
            var lic = datos.GetLicencia();

            this._sbError.AppendLine(conn.ConectarBDConsola((comm) =>
                {
                    comm.CommandText = "INSERT INTO DGENEMPR (CLAVE, " +
                                                             "RFC, " +
                                                             "DIRECCION, " +
                                                             "POBLACION, " +
                                                             "ESTACION_IGAS, " +
                                                             "SERIEKEY, " +
                                                             "NOMCOMERCIAL, " +
                                                             "RAZONSOCIAL, " +
                                                             "TIPOLICENCIA) " +
                                                     "VALUES (@CLAVE, " +
                                                             "@RFC, " +
                                                             "@DIRECCION, " +
                                                             "@POBLACION, " +
                                                             "@ESTACION_IGAS, " +
                                                             "@SERIEKEY, " +
                                                             "@NOMCOMERCIAL, " +
                                                             "@RAZONSOCIAL, " +
                                                             "@TIPOLICENCIA)";
                    comm.Parameters.Clear();
                    comm.Parameters.Add("@CLAVE", this.MaxEmpresa <= 0 ? 1 : this.MaxEmpresa);
                    comm.Parameters.Add("@RFC", emp.RFC);
                    comm.Parameters.Add("@DIRECCION", emp.Direccion);
                    comm.Parameters.Add("@POBLACION", emp.Poblacion);
                    comm.Parameters.Add("@ESTACION_IGAS", 1);
                    comm.Parameters.Add("@SERIEKEY", lic.NoSentinel);
                    comm.Parameters.Add("@NOMCOMERCIAL", emp.NombreComercial);
                    comm.Parameters.Add("@RAZONSOCIAL", lic.RazonSocial);
                    comm.Parameters.Add("@TIPOLICENCIA", lic.TipoLicencia);

                    result = comm.ExecuteNonQuery() >= 1;
                }));

            return result ? emp : null;
        }
        private Licencia InsertarLicencia(EmpresaConfiguracion datos)
        {
            var conn = WorkItem.Objetos<Conexion.Conexion>.Get();
            bool result = false;
            var lic = datos.GetLicencia();

            this._sbError.AppendLine(conn.ConectarBDConsola((comm) =>
                {
                    comm.CommandText = "INSERT INTO DPVGCONF (NOMBRECOMERCIAL, " +
                                                             "CLAVEPEMEX, " +
                                                             "USUARIOPEMEX, " +
                                                             "ESTEMPORAL, " +
                                                             "FECHAVENCE, " +
                                                             "LICENCIA, " +
                                                             "RAZONSOCIAL, " +
                                                             "NUMEROSERIE, " +
                                                             "ESTACION_IGAS) " +
                                                     "VALUES (@NOMBRECOMERCIAL, " +
                                                             "@CLAVEPEMEX, " +
                                                             "@USUARIOPEMEX, " +
                                                             "@ESTEMPORAL, " +
                                                             "@FECHAVENCE, " +
                                                             "@LICENCIA, " +
                                                             "@RAZONSOCIAL, " +
                                                             "@NUMEROSERIE, " +
                                                             "@ESTACION_IGAS) ";
                    comm.Parameters.Clear();
                    comm.Parameters.Add("@ESTACION_IGAS", 1);
                    comm.Parameters.Add("@NOMBRECOMERCIAL", datos.NombreComercial);
                    comm.Parameters.Add("@CLAVEPEMEX", datos.ClavePEMEX);
                    comm.Parameters.Add("@USUARIOPEMEX", datos.UsuarioPEMEX);
                    comm.Parameters.Add("@NUMEROSERIE", lic.NoSentinel);
                    comm.Parameters.Add("@ESTEMPORAL", lic.EsTemporal ? "Si" : "No");
                    comm.Parameters.Add("@LICENCIA", lic.LicenciaVolumetrico);
                    comm.Parameters.Add("@RAZONSOCIAL", lic.RazonSocial);
                    comm.Parameters.Add("@FECHAVENCE", (lic.FechaVence == null ? DBNull.Value : (object)lic.FechaVence));

                    result = comm.ExecuteNonQuery() >= 1;
                }));

            return result ? lic : null;
        }
        private Estacion InsertarEstacion(EmpresaConfiguracion datos)
        {
            var conn = WorkItem.Objetos<Conexion.Conexion>.Get();
            bool result = false;

            this._sbError.AppendLine(conn.ConectarBDConsola((comm) =>
                {
                    comm.CommandText = "INSERT INTO DPVGESTS (CLAVE, " +
                                                             "NOMBRE, " +
                                                             "NUMEROESTACION, " +
                                                             "INOCUIDAD) " +
                                                     "VALUES (@CLAVE, " +
                                                             "@NOMBRE, " +
                                                             "@NUMEROESTACION, " +
                                                             "@INOCUIDAD)";
                    comm.Parameters.Clear();
                    comm.Parameters.Add("@CLAVE", this.MaxEstacion <= 0 ? 1 : this.MaxEstacion);
                    comm.Parameters.Add("@NOMBRE", datos.NombreEstacion);
                    comm.Parameters.Add("@NUMEROESTACION", datos.NumEstacion);
                    comm.Parameters.Add("@INOCUIDAD", "?I<N>POO<:=IM>JK=NLML?NKK:<IPHLN");

                    result = comm.ExecuteNonQuery() >= 1;

                    comm.Transaction.CommitRetaining();

                    if (result)
                    {
                        result = ExecuteLicInoc(comm, datos.GetLicencia());
                    }
                }));

            Estacion est = new Estacion();
            {
                est.Clave = 1;
                est.Nombre = datos.NombreEstacion;
                est.NumeroEstacion = datos.NumEstacion;
            }

            return result ? est : null;
        }

        private bool ExecuteLicInoc(FbCommand comm, Licencia lic)
        {
            comm.CommandText = "SELECT CONSOLA " +
                                 "FROM DPVGESTS";

            StringBuilder sb = new StringBuilder();
            using (FbDataReader reader = comm.ExecuteReader())
            {
                try
                {
                    if (reader.Read())
                    {
                        bool insertedLic = false;
                        bool insertedFechaVence = false;
                        string[] aux = null;
                        string consola = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);

                        if (!string.IsNullOrEmpty(consola))
                        {
                            string[] arrConsola = consola.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                            char[] split = new char[] { '=' };
                            Array.ForEach(arrConsola, (p) =>
                            {
                                aux = p.Split(split, StringSplitOptions.RemoveEmptyEntries);
                                switch (aux[0].ToLower())
                                {
                                    case "inocuidad31licencia":
                                        sb.AppendFormat("Inocuidad31Licencia={0}", lic.LicienciaInocua).AppendLine();
                                        insertedLic = true;
                                        break;
                                    case "inocuidad31fechavence":
                                        sb.AppendFormat("Inocuidad31FechaVence={0}", (!lic.EsTemporalInocuo ? string.Empty : lic.FechaVenceInocua.GetValueOrDefault().ToString("dd/MM/yyyy"))).AppendLine();
                                        insertedFechaVence = true;
                                        break;
                                    case "manejaservicios":
                                        sb.AppendFormat("ManejaServicios={0}", "Si").AppendLine();
                                        break;
                                    case "puertoservicio":
                                        sb.AppendFormat("PuertoServicio={0}", "http://127.0.0.1:8199/bin").AppendLine();
                                        break;
                                }
                            });
                        }
                        else
                        {
                            sb.AppendFormat("Inocuidad31Licencia={0}", lic.LicienciaInocua).AppendLine();
                            sb.AppendFormat("Inocuidad31FechaVence={0}", (!lic.EsTemporalInocuo ? string.Empty : lic.FechaVenceInocua.GetValueOrDefault().ToString("dd/MM/yyyy"))).AppendLine();
                            sb.AppendFormat("ManejaServicios={0}", "Si").AppendLine();
                            sb.AppendFormat("PuertoServicio={0}", "http://127.0.0.1:8199/bin").AppendLine();    
                        }
                    }
                }
                finally
                {
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
            }
            if (!string.IsNullOrEmpty(sb.ToString().Trim()))
            {
                comm.Transaction.CommitRetaining();
                comm.CommandText = "UPDATE DPVGESTS SET CONSOLA=@CONSOLA";
                comm.Parameters.Add("@CONSOLA", sb.ToString().Trim());
                return comm.ExecuteNonQuery() >= 1;
            }

            return true;
        }
    }
}
