using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using PSMApiRest.Lib;
using PSMApiRest.Models;

namespace PSMApiRest.DAL
{
    public class BancosDAL
    {
        private readonly DB dbCon;
        private DataTable dt;
        private readonly Hashtable Parametros;

        public BancosDAL()
        {
            dt = new DataTable();
            dbCon = new DB();
            Parametros = new Hashtable();
        }
        public List<Bancos> GetBancos(int id_factura)
        {
            Parametros.Clear();
            Parametros.Add("@IdFactura", id_factura);

            List<Bancos> bancosList = new List<Bancos>();
            dt = dbCon.Procedure("AMIGO_PUERTA", "ListadoBancos", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Bancos item = new Bancos
                        {
                            Id_Banco = Convert.ToInt16(dt.Rows[i]["Id_Banco"]),
                            Id_Cuenta = Convert.ToInt16(dt.Rows[i]["Id_Cuenta"]),
                            NumeroCuenta = Convert.ToString(dt.Rows[i]["NumeroCuenta"]),
                            Descripcion = Convert.ToString(dt.Rows[i]["Descripcion"])
                        };
                        bancosList.Add(item);
                    }
                }
            }
            return bancosList;
        }
    }
}