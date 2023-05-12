using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using PSMApiRest.Lib;
using PSMApiRest.Models;

namespace PSMApiRest.DAL
{
    public class ConciliacionesDAL
    {
        private readonly DB dbCon;
        private DataTable dt;
        private readonly Hashtable Parametros;

        public ConciliacionesDAL()
        {
            dt = new DataTable();
            dbCon = new DB();
            Parametros = new Hashtable();
        }
        public List<Conciliaciones> GetConciliaciones(int Todos, string FechaDesde, string FechaHasta)
        {
            Parametros.Clear();
            Parametros.Add("@FechaDesde", FechaDesde);
            Parametros.Add("@FechaHasta", FechaHasta);
            Parametros.Add("@Todos", Todos);

            List<Conciliaciones> conciliacionesList = new List<Conciliaciones>();
            dt = dbCon.Procedure("AMIGO", "ConciliacionesSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Conciliaciones item = new Conciliaciones();
                        item.Id_Archivo = Convert.ToInt16(dt.Rows[i]["Id_Archivo"]);
                        item.Id_Banco = Convert.ToInt16(dt.Rows[i]["Id_Banco"]);
                        item.Banco = Convert.ToString(dt.Rows[i]["Banco"]);
                        item.Id_Usuario = Convert.ToInt16(dt.Rows[i]["Id_Usuario"]);
                        item.Descripcion = Convert.ToString(dt.Rows[i]["Descripcion"]);
                        item.Fecha = Convert.ToDateTime(dt.Rows[i]["Fecha"]);
                        item.SaldoAnterior = Convert.ToDecimal(dt.Rows[i]["SaldoAnterior"]);
                        item.SaldoFinal = Convert.ToDecimal(dt.Rows[i]["SaldoFinal"]);
                        item.Cerrado = Convert.ToByte(dt.Rows[i]["Cerrado"]);
                        conciliacionesList.Add(item);
                    }
                }
            }
            return conciliacionesList;
        }
    }
}