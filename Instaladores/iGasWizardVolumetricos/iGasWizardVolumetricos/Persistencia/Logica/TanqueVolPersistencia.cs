using System;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Persistencia.Entidades;

namespace iGasWizardVolumetricos.Persistencia.Logica
{
    public class TanqueVolPersistencia
    {
        public string Guardar(ListaTanqueVol lista)
        {
            string ex = null;
            ListaCombustibleVol combustibles = WorkItem.Objetos<ListaCombustibleVol>.Get();
            UbicarBD db = WorkItem.Objetos<UbicarBD>.Get();
            Conexion.Conexion cxn = new iGasWizardVolumetricos.Persistencia.Logica.Conexion.Conexion(db.RutaBD);
            ex = cxn.ConectarBDConsola((c) =>
            {
                c.CommandText = "DELETE FROM DPVGTANQ";
                c.ExecuteNonQuery();

                c.CommandText = "INSERT INTO DPVGTANQ " +
                                "(TANQUE" +
                                ", COMBUSTIBLE" +
                                ", CLAVEPRODUCTOMEDICION" +
                                ", VOLUMENFONDAJE" +
                                ", CAPACIDAD" +
                                ", ALTURA" +
                                ", ACTIVO" +
                                ", CAPACIDADOPERATIVA" +
                                ", MINIMOOPERACION" +
                                ", CALC_VENTAS_ENTRADAS" +
                                ", MAXIMAENTRADA" +
                                ", SIFONEADO) " +
                                "VALUES " +
                                "(@TANQUE" +
                                ", @COMBUSTIBLE" +
                                ", @CLAVEPRODUCTOMEDICION" +
                                ", @VOLUMENFONDAJE" +
                                ", @CAPACIDAD" +
                                ", @ALTURA" +
                                ", @ACTIVO" +
                                ", @CAPACIDADOPERATIVA" +
                                ", @MINIMOOPERACION" +
                                ", @CALC_VENTAS_ENTRADAS" +
                                ", @MAXIMAENTRADA" +
                                ", @SIFONEADO)";

                lista.ForEach(i =>
                    {
                        c.Parameters.Clear();
                        c.Parameters.Add("@TANQUE", i.Clave);
                        c.Parameters.Add("@COMBUSTIBLE", combustibles.Find(e => e.Nombre.Equals(i.TipoCombustible)).Clave);
                        c.Parameters.Add("@CLAVEPRODUCTOMEDICION", DBNull.Value);
                        c.Parameters.Add("@VOLUMENFONDAJE", i.VolumenFondo);
                        c.Parameters.Add("@CAPACIDAD", i.Capacidad);
                        c.Parameters.Add("@ALTURA", i.Altura);
                        c.Parameters.Add("@ACTIVO", "Si");
                        c.Parameters.Add("@CAPACIDADOPERATIVA", DBNull.Value);
                        c.Parameters.Add("@MINIMOOPERACION", DBNull.Value);
                        c.Parameters.Add("@CALC_VENTAS_ENTRADAS", "Si");
                        c.Parameters.Add("@MAXIMAENTRADA", DBNull.Value);
                        c.Parameters.Add("@SIFONEADO", "N");
                        c.ExecuteNonQuery();
                    });
            });

            return ex;
        }
    }
}
