using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using PSMApiRest.Lib;
using PSMApiRest.Models;

namespace PSMApiRest.DAL
{
    public class LapsosDAL
    {
        private readonly DB dbCon;
        private DataTable dt;
        private readonly Hashtable Parametros;

        public LapsosDAL()
        {
            dt = new DataTable();
            dbCon = new DB();
            Parametros = new Hashtable();
        }
        public List<Lapsos> GetLapsos(int puerta)
        {
            Parametros.Clear();
            Parametros.Add("@Puerta", 1);

            List<Lapsos> LapsosList = new List<Lapsos>();
            dt = dbCon.Procedure("AMIGO", "LapsosSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Lapsos lapsos = new Lapsos();
                        lapsos.Id_Periodo = Convert.ToInt16(dt.Rows[i]["Id_Periodo"]);
                        lapsos.Lapso = Convert.ToString(dt.Rows[i]["Lapso"]);
                        lapsos.Activo = Convert.ToByte(dt.Rows[i]["Activo"]);
                        lapsos.Cerrado = Convert.ToByte(dt.Rows[i]["Cerrado"]);
                        lapsos.tipo_lapso = Convert.ToByte(dt.Rows[i]["tipo_lapso"]);
                        LapsosList.Add(lapsos);
                    }
                }
            }
            return LapsosList;
        }
    }
}