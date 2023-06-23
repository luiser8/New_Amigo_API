using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using PSMApiRest.Lib;
using PSMApiRest.Models;

namespace PSMApiRest.DAL
{
    public class MonedasDAL
    {
        private readonly DB dbCon;
        private DataTable dt;
        private readonly Hashtable Parametros;

        public MonedasDAL()
        {
            dt = new DataTable();
            dbCon = new DB();
            Parametros = new Hashtable();
        }
        public List<Monedas> GetMonedas()
        {
            Parametros.Clear();

            List<Monedas> monedasList = new List<Monedas>();
            dt = dbCon.Procedure("AMIGO", "MonedasSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Monedas monedas = new Monedas
                        {
                            IdMoneda = Convert.ToInt32(dt.Rows[i]["Id_Moneda"]),
                            Simbolo = Convert.ToString(dt.Rows[i]["Simbolo"]),
                            Descripcion = Convert.ToString(dt.Rows[i]["Descripcion"]),
                        };
                        monedasList.Add(monedas);
                    }
                }
            }
            return monedasList;
        }
    }
}