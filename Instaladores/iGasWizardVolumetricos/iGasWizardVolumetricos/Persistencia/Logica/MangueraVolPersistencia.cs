using System;
using iGasWizardVolumetricos.Generales;
using iGasWizardVolumetricos.Persistencia.Entidades;

namespace iGasWizardVolumetricos.Persistencia.Logica
{
    public class MangueraVolPersistencia
    {
        public string Guardar(ListaMangueraVol lista)
        {
            string ex = null;
            ListaCombustibleVol combustibles = WorkItem.Objetos<ListaCombustibleVol>.Get();
            MarcaDispensarioVol marca = WorkItem.Objetos<MarcaDispensarioVol>.Get();

            int? con_DigitoAjuste = null;
            int? digitoAjusteVol = null;
            int? digitosGilbarco = null;
            if (WorkItem.Objetos<VariablesVol>.Exist())
            {
                VariablesVol var = WorkItem.Objetos<VariablesVol>.Get();
                if (marca.Marca.Equals(1))
                {
                    con_DigitoAjuste = var.ConDigitoAjuste;
                }
                else if (marca.Marca.Equals(4))
                {
                    con_DigitoAjuste = var.ConDigitoAjuste;
                    digitoAjusteVol = var.DigitoAjusteVol;
                    digitosGilbarco = var.DigitosGilbarco;
                }
            }


            UbicarBD db = WorkItem.Objetos<UbicarBD>.Get();
            Conexion.Conexion cxn = new iGasWizardVolumetricos.Persistencia.Logica.Conexion.Conexion(db.RutaBD);
            ex = cxn.ConectarBDConsola((c) =>
            {
                c.CommandText = "DELETE FROM DPVGBOMB";
                c.ExecuteNonQuery();

                c.CommandText = "INSERT INTO DPVGBOMB" +
                                "(MANGUERA" +
                                ", POSCARGA" +
                                ", COMBUSTIBLE" +
                                ", ISLA" +
                                ", CON_PRECIO" +
                                ", CON_POSICION" +
                                ", CON_DIGITOAJUSTE" +
                                ", IMPRESORA" +
                                ", ACTIVO" +
                                ", IMPRIMEAUTOM" +
                                ", DIGITOAJUSTEPRECIO" +
                                ", CAMPOLECTURA" +
                                ", MODOOPERACION" +
                                ", TANQUE" +
                                ", IMPRETARJETAS" +
                                ", DIGITOSGILBARCO" +
                                ", DIGITOAJUSTEVOL" +
                                ", RFID" +
                                ", CLIENTE" +
                                ", VEHICULO" +
                                ", CONTROL_AROS" +
                                ", ERROR" +
                                ", MENSAJE_ERROR" +
                                ", DIGITOAJUSTEPRESET)" +
                                "VALUES" +
                                "(@MANGUERA" +
                                ", @POSCARGA" +
                                ", @COMBUSTIBLE" +
                                ", @ISLA" +
                                ", @CON_PRECIO" +
                                ", @CON_POSICION" +
                                ", @CON_DIGITOAJUSTE" +
                                ", @IMPRESORA" +
                                ", @ACTIVO" +
                                ", @IMPRIMEAUTOM" +
                                ", @DIGITOAJUSTEPRECIO" +
                                ", @CAMPOLECTURA" +
                                ", @MODOOPERACION" +
                                ", @TANQUE" +
                                ", @IMPRETARJETAS" +
                                ", @DIGITOSGILBARCO" +
                                ", @DIGITOAJUSTEVOL" +
                                ", @RFID" +
                                ", @CLIENTE" +
                                ", @VEHICULO" +
                                ", @CONTROL_AROS" +
                                ", @ERROR" +
                                ", @MENSAJE_ERROR" +
                                ", @DIGITOAJUSTEPRESET)";

                lista.ForEach(i =>
                {
                    c.Parameters.Clear();
                    c.Parameters.Add("@MANGUERA", i.Clave);
                    c.Parameters.Add("@POSCARGA", i.Posicion);
                    c.Parameters.Add("@COMBUSTIBLE", combustibles.Find(e => e.Nombre.Equals(i.TipoCombustible)).Clave);
                    c.Parameters.Add("@ISLA", i.Isla);
                    c.Parameters.Add("@CON_PRECIO", DBNull.Value);
                    c.Parameters.Add("@CON_POSICION", i.ConPosicion);
                    c.Parameters.Add("@CON_DIGITOAJUSTE", con_DigitoAjuste);
                    c.Parameters.Add("@IMPRESORA", DBNull.Value);
                    c.Parameters.Add("@ACTIVO", "Si");
                    c.Parameters.Add("@IMPRIMEAUTOM", DBNull.Value);
                    c.Parameters.Add("@DIGITOAJUSTEPRECIO", DBNull.Value);
                    c.Parameters.Add("@CAMPOLECTURA", i.CampoLectura);
                    c.Parameters.Add("@MODOOPERACION", marca.Marca.Equals(3) ? null : marca.ModoOperacion);
                    c.Parameters.Add("@TANQUE", DBNull.Value);
                    c.Parameters.Add("@IMPRETARJETAS", DBNull.Value);
                    c.Parameters.Add("@DIGITOSGILBARCO", digitosGilbarco);
                    c.Parameters.Add("@DIGITOAJUSTEVOL", digitoAjusteVol);
                    c.Parameters.Add("@RFID", DBNull.Value);
                    c.Parameters.Add("@CLIENTE", DBNull.Value);
                    c.Parameters.Add("@VEHICULO", DBNull.Value);
                    c.Parameters.Add("@CONTROL_AROS", DBNull.Value);
                    c.Parameters.Add("@ERROR", DBNull.Value);
                    c.Parameters.Add("@MENSAJE_ERROR", DBNull.Value);
                    c.Parameters.Add("@DIGITOAJUSTEPRESET", DBNull.Value);
                    c.ExecuteNonQuery();
                });
            });

            return ex;
        }
    }
}
