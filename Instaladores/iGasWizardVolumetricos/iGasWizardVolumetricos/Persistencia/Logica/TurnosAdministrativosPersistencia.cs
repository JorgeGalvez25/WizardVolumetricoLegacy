using System;
using FirebirdSql.Data.FirebirdClient;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Persistencia.Entidades;

namespace iGasWizardVolumetricos.Persistencia.Logica
{
    public class TurnosAdministrativosPersistencia
    {
        private TurnoAdministrativo ReadToEntidad(FbDataReader reader)
        {
            TurnoAdministrativo result = new TurnoAdministrativo();

            result.Turno = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
            string hInicio = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
            string hFin = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
            string hMin = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
            string hMax = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);

            TimeSpan aux = TimeSpan.MinValue;
            TimeSpan.TryParse(hInicio, out aux);
            result.HoraInicio = aux;

            TimeSpan.TryParse(hFin, out aux);
            result.HoraFin = aux;

            TimeSpan.TryParse(hMin, out aux);
            result.HoraMin = aux;

            TimeSpan.TryParse(hMax, out aux);
            result.HoraMax = aux;

            return result;
        }

        public ListaTurnosAdministrativos ObtenerTodos()
        {
            var conn = WorkItem.Objetos<Conexion.Conexion>.Get();
            ListaTurnosAdministrativos result = new ListaTurnosAdministrativos();

            conn.ConectarBDConsola((comm) =>
            {
                comm.CommandText = "SELECT " +
                                        "TURNO, " +
                                        "HORAINICIAL, " +
                                        "HORAFINAL, " +
                                        "HORAMIN, " +
                                        "HORAMAX " +
                                   " FROM DPVGTURC";

                using (FbDataReader reader = comm.ExecuteReader())
                {
                    try
                    {
                        while (reader.Read())
                        {
                            result.Add(ReadToEntidad(reader));
                        }
                    }
                    finally
                    {
                        if (!reader.IsClosed) { reader.Close(); }
                    }
                }
            });
            return result;
        }

        private void CrearParametros(ref FbCommand comm, TurnoAdministrativo turnos)
        {
            comm.Parameters.Clear();
            comm.Parameters.Add("@TURNO", turnos.Turno);
            comm.Parameters.Add("@HORAINICIAL", new DateTime(turnos.HoraInicio.Ticks).ToString("HH:mm"));
            comm.Parameters.Add("@HORAFINAL", new DateTime(turnos.HoraFin.Ticks).ToString("HH:mm"));
            comm.Parameters.Add("@HORAMIN", new DateTime(turnos.HoraMin.Ticks).ToString("HH:mm"));
            comm.Parameters.Add("@HORAMAX", new DateTime(turnos.HoraMax.Ticks).ToString("HH:mm"));
        }

        public TurnoAdministrativo InsertarActualizar(TurnoAdministrativo turno)
        {
            var items = this.ObtenerTodos();

            return (items != null && items.Count > 0) ? this.Actualizar(turno) : this.Insertar(turno);
        }
        public ListaTurnosAdministrativos InsertarActualizarLista(ListaTurnosAdministrativos turnos)
        {
            var items = this.ObtenerTodos();
            bool result = (items != null && items.Count > 0) ? this.ActualizarLista(turnos) : this.InsertarLista(turnos);

            return result ? turnos : null;
        }

        public bool InsertarLista(ListaTurnosAdministrativos turnos)
        {
            var conn = WorkItem.Objetos<Conexion.Conexion>.Get();
            bool result = false;

            conn.ConectarBDConsola((comm) =>
            {
                comm.CommandText = "DELETE FROM DPVGTURC";
                comm.ExecuteNonQuery();

                comm.CommandText = "DELETE FROM DPVGTURCA";
                comm.ExecuteNonQuery();

                for (int i = 0; i < turnos.Count; i++)
                {
                    comm.CommandText = "INSERT INTO DPVGTURC " +
                                        "(TURNO, " +
                                        "HORAINICIAL, " +
                                        "HORAFINAL, " +
                                        "HORAMIN, " +
                                        "HORAMAX) " +
                                   "VALUES " +
                                        "(@TURNO, " +
                                        "@HORAINICIAL, " +
                                        "@HORAFINAL, " +
                                        "@HORAMIN, " +
                                        "@HORAMAX)";
                    this.CrearParametros(ref comm, turnos[i]);

                    result = comm.ExecuteNonQuery() >= 1;

                    if (result)
                    {
                        if (InsReplicar(turnos[i], comm) == null)
                        {
                            comm.Transaction.RollbackRetaining();
                            throw new Exception("No fue posible insertar en DPVGTURCA");
                        }

                        comm.Transaction.CommitRetaining();
                    }
                }
            });

            return result;
        }
        public bool ActualizarLista(ListaTurnosAdministrativos turnos)
        {
            var conn = WorkItem.Objetos<Conexion.Conexion>.Get();
            bool result = false;

            conn.ConectarBDConsola((comm) =>
            {
                comm.CommandText = "UPDATE DPVGTURC SET " +
                                        "HORAINICIAL = @HORAINICIAL, " +
                                        "HORAFINAL = @HORAFINAL, " +
                                        "HORAMIN = @HORAMIN, " +
                                        "HORAMAX = @HORAMAX " +
                                   "WHERE (TURNO = @TURNO)";
                for (int i = 0; i < turnos.Count; i++)
                {
                    this.CrearParametros(ref comm, turnos[i]);

                    result = comm.ExecuteNonQuery() >= 1;

                    if (result)
                    {
                        if (ActReplicar(turnos[i], comm) == null)
                        {
                            comm.Transaction.RollbackRetaining();
                            throw new Exception("No fue posible agregar turnos a DPVGTURNCA");
                        }
                        comm.Transaction.CommitRetaining();
                    }
                }
            });

            return result;
        }

        public TurnoAdministrativo Insertar(TurnoAdministrativo turno)
        {
            var conn = WorkItem.Objetos<Conexion.Conexion>.Get();
            bool result = false;

            conn.ConectarBDConsola((comm) =>
            {
                comm.CommandText = "INSERT INTO DPVGTURC " +
                                        "(TURNO, " +
                                        "HORAINICIAL, " +
                                        "HORAFINAL, " +
                                        "HORAMIN, " +
                                        "HORAMAX) " +
                                   "VALUES " +
                                        "(@TURNO, " +
                                        "@HORAINICIAL, " +
                                        "@HORAFINAL, " +
                                        "@HORAMIN, " +
                                        "@HORAMAX)";
                this.CrearParametros(ref comm, turno);

                result = comm.ExecuteNonQuery() >= 1;

                if (result)
                {
                    if (InsReplicar(turno, comm) == null)
                    {
                        comm.Transaction.RollbackRetaining();
                        throw new Exception("No fue posible insertar en DPVGTURCA");
                    }

                    comm.Transaction.CommitRetaining();
                }
            });

            return result ? turno : null;
        }
        private TurnoAdministrativo InsReplicar(TurnoAdministrativo turno, FbCommand comm)
        {
            bool result = false;

            comm.CommandText = "INSERT INTO DPVGTURCA " +
                                    "(TURNO, " +
                                    "HORAINICIAL, " +
                                    "HORAFINAL, " +
                                    "HORAMIN, " +
                                    "HORAMAX) " +
                               "VALUES " +
                                    "(@TURNO, " +
                                    "@HORAINICIAL, " +
                                    "@HORAFINAL, " +
                                    "@HORAMIN, " +
                                    "@HORAMAX)";
            this.CrearParametros(ref comm, turno);

            result = comm.ExecuteNonQuery() >= 1;

            return result ? turno : null;
        }

        public TurnoAdministrativo Actualizar(TurnoAdministrativo turno)
        {
            var conn = WorkItem.Objetos<Conexion.Conexion>.Get();
            bool result = false;

            conn.ConectarBDConsola((comm) =>
            {
                comm.CommandText = "UPDATE DPVGTURC SET " +
                                        "HORAINICIAL = @HORAINICIAL, " +
                                        "HORAFINAL = @HORAFINAL, " +
                                        "HORAMIN = @HORAMIN, " +
                                        "HORAMAX = @HORAMAX " +
                                   "WHERE (TURNO = @TURNO)";
                this.CrearParametros(ref comm, turno);

                result = comm.ExecuteNonQuery() >= 1;

                if (result)
                {
                    if (ActReplicar(turno, comm) == null)
                    {
                        comm.Transaction.RollbackRetaining();
                        throw new Exception("No fue posible agregar turnos a DPVGTURNCA");
                    }

                    comm.Transaction.CommitRetaining();
                }
            });

            return result ? turno : null;
        }
        private TurnoAdministrativo ActReplicar(TurnoAdministrativo turno, FbCommand comm)
        {
            bool result = false;

            comm.CommandText = "UPDATE DPVGTURC SET " +
                                    "HORAINICIAL = @HORAINICIAL, " +
                                    "HORAFINAL = @HORAFINAL, " +
                                    "HORAMIN = @HORAMIN, " +
                                    "HORAMAX = @HORAMAX " +
                               "WHERE (TURNO = @TURNO)";

            result = comm.ExecuteNonQuery() >= 1;
            return result ? turno : null;
        }
    }
}
