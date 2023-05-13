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
        public List<Bancos> GetBancos()
        {
            Parametros.Clear();

            List<Bancos> bancosList = new List<Bancos>();
            dt = dbCon.Procedure("AMIGO", "BancosSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Bancos item = new Bancos();
                        item.Id_Banco = Convert.ToInt16(dt.Rows[i]["Id_Banco"]);
                        item.Id_Cuenta = Convert.ToInt16(dt.Rows[i]["Id_Cuenta"]);
                        item.Id_CuentaDebito = Convert.ToInt16(dt.Rows[i]["Id_CuentaDebito"]);
                        item.Id_FormatoConciliacion = Convert.ToInt16(dt.Rows[i]["Id_FormatoConciliacion"]);
                        item.Id_SubEspecifica = Convert.ToInt16(dt.Rows[i]["Id_SubEspecifica"]);
                        item.NumeroCuenta = Convert.ToString(dt.Rows[i]["NumeroCuenta"]);
                        item.Descripcion = Convert.ToString(dt.Rows[i]["Descripcion"]);
                        item.Tipo = Convert.ToInt16(dt.Rows[i]["Tipo"]);
                        item.Activa = Convert.ToByte(dt.Rows[i]["Activa"]);
                        bancosList.Add(item);
                    }
                }
            }
            return bancosList;
        }
    }
}