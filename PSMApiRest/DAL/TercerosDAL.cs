using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using PSMApiRest.Lib;
using PSMApiRest.Models;

namespace PSMApiRest.DAL
{
    public class TercerosDAL
    {
        private readonly DB dbCon;
        private DataTable dt;
        private readonly Hashtable Parametros;

        public TercerosDAL()
        {
            dt = new DataTable();
            dbCon = new DB();
            Parametros = new Hashtable();
        }
        public bool UpdateTerceros(string Id_Terceros, string Identificador, string Telefonos, string Emails)
        {
            Parametros.Clear();
            Parametros.Add("@Id_Terceros", Id_Terceros);
            Parametros.Add("@Identificador", Identificador);
            Parametros.Add("@Telefonos", Telefonos != "" ? Telefonos : null);
            Parametros.Add("@Emails", Emails != "" ? Emails : null);

            dbCon.Procedure("AMIGO", "TercerosSysUpdate", Parametros);

            return dbCon.ErrorEstatus;
        }

        public bool GetTercero(string Identificador)
        {
            Parametros.Clear();
            Parametros.Add("@Identificador", Identificador);
            dt = dbCon.Procedure("AMIGO", "TercerosSysSelect", Parametros);
            int existe = 0;

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        existe = Convert.ToInt32(dt.Rows[i]["Existe"]);
                    }
                }
            }
            return existe > 0 ? true : false;
        }
    }
}