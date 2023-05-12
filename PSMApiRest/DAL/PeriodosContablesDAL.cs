using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using PSMApiRest.Lib;
using PSMApiRest.Models;

namespace PSMApiRest.DAL
{
    public class PeriodosContablesDAL
    {
        private readonly DB dbCon;
        private DataTable dt;
        private readonly Hashtable Parametros;

        public PeriodosContablesDAL()
        {
            dt = new DataTable();
            dbCon = new DB();
            Parametros = new Hashtable();
        }
        public List<PeriodosContables> GetPeriodosContables(int Todos, string FechaDesde, string FechaHasta)
        {
            Parametros.Clear();
            Parametros.Add("@FechaDesde", FechaDesde);
            Parametros.Add("@FechaHasta", FechaHasta);
            Parametros.Add("@Todos", Todos);

            List<PeriodosContables> periodosContablesList = new List<PeriodosContables>();
            dt = dbCon.Procedure("AMIGO", "PeriodosContablesSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        PeriodosContables item = new PeriodosContables();
                        item.Id_Periodo = Convert.ToInt16(dt.Rows[i]["Id_Periodo"]);
                        item.Inicio = Convert.ToDateTime(dt.Rows[i]["Inicio"]);
                        item.Fin = Convert.ToDateTime(dt.Rows[i]["Fin"]);
                        item.Descripcion = Convert.ToString(dt.Rows[i]["Descripcion"]);
                        item.Bloqueado = Convert.ToByte(dt.Rows[i]["Bloqueado"]);
                        periodosContablesList.Add(item);
                    }
                }
            }
            return periodosContablesList;
        }
    }
}