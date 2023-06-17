using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using PSMApiRest.Lib;
using PSMApiRest.Models;

namespace PSMApiRest.DAL
{
    public class CentrosDeCostoDAL
    {
        private readonly DB dbCon;
        private DataTable dt;
        private readonly Hashtable Parametros;

        public CentrosDeCostoDAL()
        {
            dt = new DataTable();
            dbCon = new DB();
            Parametros = new Hashtable();
        }
        public List<CentrosDeCosto> GetCentrosDeCosto()
        {
            Parametros.Clear();

            List<CentrosDeCosto> centrosDeCostosList = new List<CentrosDeCosto>();
            dt = dbCon.Procedure("AMIGO", "CentrosDeCostoSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CentrosDeCosto centrosDeCosto = new CentrosDeCosto
                        {
                            IdCentro = Convert.ToInt32(dt.Rows[i]["Id_Centro"]),
                            Descripcion = Convert.ToString(dt.Rows[i]["Descripcion"]),
                            Activo = Convert.ToByte(dt.Rows[i]["Activo"]),
                        };
                        centrosDeCostosList.Add(centrosDeCosto);
                    }
                }
            }
            return centrosDeCostosList;
        }
    }
}