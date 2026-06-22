using System.Text;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Persistencia.Entidades;

namespace iGasWizardVolumetricos.Persistencia.Logica
{
    public class VariablesVolPersistencia
    {
        public string Guardar(VariablesVol e)
        {
            string ex = string.Empty;

            MarcaDispensarioVol marca = WorkItem.Objetos<MarcaDispensarioVol>.Get();

            switch (marca.Marca)
            {
                case 1:
                    ex = wayne(e, marca);
                    break;
                case 2:
                    ex = bennett(e, marca);
                    break;
                case 4:
                    ex = gilbarco(e, marca);
                    break;
            }

            return ex;
        }

        private string gilbarco(VariablesVol e, MarcaDispensarioVol marca)
        {
            string ex = string.Empty;

            UbicarBD db = WorkItem.Objetos<UbicarBD>.Get();
            Conexion.Conexion cxn = new iGasWizardVolumetricos.Persistencia.Logica.Conexion.Conexion(db.RutaBD);
            ex = cxn.ConectarBDConsola((c) =>
            {
                c.CommandText = "SELECT CONSOLA FROM DPVGESTS";
                StringBuilder sb = new StringBuilder();
                using (FbDataReader reader = c.ExecuteReader())
                {
                    try
                    {
                        if (reader.Read())
                        {
                            string consola = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                            if (!string.IsNullOrEmpty(consola))
                            {
                                string[] arrConsola = consola.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
                                char[] split = new char[] { '=' };
                                if (arrConsola.Length > 0)
                                {
                                    foreach (var s in arrConsola)
                                    {
                                        sb.AppendLine(string.Format(s));
                                    }
                                    sb.AppendLine(string.Format("SetUpPam1000={0}", e.SetUpPam1000));
                                    sb.AppendLine(string.Format("DecimalesPresetPAM={0}", e.DecimalesPresetPAM));
                                    sb.AppendLine(string.Format("DecimalesPresetPAMLitros={0}", e.DecimalesPresetPAMLitros));
                                    sb.AppendLine(string.Format("MaximoPresetPAM={0}", e.MaximoPresetPAM));
                                    sb.AppendLine(string.Format("ReconexionesAros={0}", marca.ReconexionAros));
                                    sb.AppendLine(string.Format("AjustePam={0}", e.AjustePam ? "Si" : "No"));
                                    sb.AppendLine(string.Format("ModoAutorizaPam={0}", e.ModoAutorizaPam));
                                }
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
                c.Transaction.CommitRetaining();
                c.CommandText = "UPDATE DPVGESTS SET CONSOLA=@CONSOLA";
                c.Parameters.Add("@CONSOLA", sb.ToString());
                c.ExecuteNonQuery();
            });

            return ex;
        }

        private string bennett(VariablesVol e, MarcaDispensarioVol marca)
        {
            string ex = string.Empty;

            UbicarBD db = WorkItem.Objetos<UbicarBD>.Get();
            Conexion.Conexion cxn = new iGasWizardVolumetricos.Persistencia.Logica.Conexion.Conexion(db.RutaBD);
            ex = cxn.ConectarBDConsola((c) =>
            {
                c.CommandText = "SELECT CONSOLA FROM DPVGESTS";
                StringBuilder sb = new StringBuilder();
                using (FbDataReader reader = c.ExecuteReader())
                {
                    try
                    {
                        if (reader.Read())
                        {
                            string consola = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                            if (!string.IsNullOrEmpty(consola))
                            {
                                string[] arrConsola = consola.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
                                char[] split = new char[] { '=' };
                                if (arrConsola.Length > 0)
                                {
                                    foreach(var s in arrConsola)
                                    {
                                        sb.AppendLine(string.Format(s));
                                    }
                                    sb.AppendLine(string.Format("SoportaSeleccionProducto={0}", e.SoportaSeleccionProducto ? "Si" : "No"));
                                    sb.AppendLine(string.Format("ReconexionesAros={0}", marca.ReconexionAros));
                                }
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
                c.Transaction.CommitRetaining();
                c.CommandText = "UPDATE DPVGESTS SET CONSOLA=@CONSOLA";
                c.Parameters.Add("@CONSOLA", sb.ToString());
                c.ExecuteNonQuery();
            });
            
            return ex;
        }

        private string wayne(VariablesVol e, MarcaDispensarioVol marca)
        {
            string ex = string.Empty;

            UbicarBD db = WorkItem.Objetos<UbicarBD>.Get();
            Conexion.Conexion cxn = new iGasWizardVolumetricos.Persistencia.Logica.Conexion.Conexion(db.RutaBD);
            ex = cxn.ConectarBDConsola((c) =>
            {
                c.CommandText = "SELECT CONSOLA FROM DPVGESTS";
                StringBuilder sb = new StringBuilder();
                using (FbDataReader reader = c.ExecuteReader())
                {
                    try
                    {
                        if (reader.Read())
                        {
                            string consola = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                            if (!string.IsNullOrEmpty(consola))
                            {
                                string[] arrConsola = consola.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
                                char[] split = new char[] { '=' };
                                if (arrConsola.Length > 0)
                                {
                                    foreach (var s in arrConsola)
                                    {
                                        sb.AppendLine(string.Format(s));
                                    }
                                    sb.AppendLine(string.Format("DecimalesPresetWayne={0}", e.DecimalesPresetWayne));
                                    sb.AppendLine(string.Format("DecimalesPresetWayneLitros={0}", e.DecimalesPresetWayneLitros));
                                    sb.AppendLine(string.Format("AjusteWayne={0}", e.AjusteWayne ? "Si" : "No"));
                                    sb.AppendLine(string.Format("NivelPrecioWayne={0}", e.NivelPrecioWayne));
                                    sb.AppendLine(string.Format("ReconexionesAros={0}", marca.ReconexionAros));
                                }
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
                c.Transaction.CommitRetaining();
                c.CommandText = "UPDATE DPVGESTS SET CONSOLA=@CONSOLA";
                c.Parameters.Add("@CONSOLA", sb.ToString());
                c.ExecuteNonQuery();
               
                c.CommandText = "UPDATE DPVGTCMB SET CON_PRODUCTOPRECIO=@CON_PRODUCTOPRECIO WHERE CLAVE=@CLAVE";
                e.ConProductoPrecio.ForEach(i =>
                    {
                        c.Parameters.Clear();
                        c.Parameters.Add("@CLAVE", i.Clave);
                        c.Parameters.Add("@CON_PRODUCTOPRECIO", i.ConProductoPrecio);
                        c.ExecuteNonQuery();
                    });
            });

            return ex;
        }
    }
}
