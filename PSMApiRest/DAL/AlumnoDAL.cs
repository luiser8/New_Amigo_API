using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using PSMApiRest.Lib;
using PSMApiRest.Models;

namespace PSMApiRest.DAL
{
    public class AlumnoDAL
    {
        private readonly DB dbCon;
        private DataTable dt;
        private readonly Hashtable Parametros;

        public AlumnoDAL()
        {
            dt = new DataTable();
            dbCon = new DB();
            Parametros = new Hashtable();
        }
        public List<Alumno> GetAlumno(string Cedula)
        {
            Parametros.Clear();
            Parametros.Add("@Cedula", Cedula);
            List<Alumno> AlumnoList = new List<Alumno>();
            dt = dbCon.Procedure("PRD", "AlumnosSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Alumno alumno = new Alumno();
                        alumno.IdAl = Convert.ToInt32(dt.Rows[i]["IdAl"]);
                        alumno.Cedula = Convert.ToString(dt.Rows[i]["Cedula"]);
                        alumno.Fullnombre = Convert.ToString(dt.Rows[i]["Fullnombre"]);
                        alumno.Foto = (Byte[])dt.Rows[i]["Foto"];
                        alumno.Sexo = Convert.ToByte(dt.Rows[i]["Sexo"]);
                        alumno.LapCur = Convert.ToString(dt.Rows[i]["LapCur"]);
                        alumno.LapIng = Convert.ToString(dt.Rows[i]["LapIng"]);
                        alumno.EstAca = Convert.ToString(dt.Rows[i]["EstAca"]);
                        alumno.codcarrera = Convert.ToInt32(dt.Rows[i]["codcarrera"]);
                        AlumnoList.Add(alumno);
                    }
                }
            }
            return AlumnoList;
        }
        public string GetAlumnoEstAca(string Cedula)
        {
            string estaca = "";
            Parametros.Clear();
            Parametros.Add("@Cedula", Cedula);
            dt = dbCon.Procedure("PRD", "AlumnosSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        estaca = Convert.ToString(dt.Rows[i]["EstAca"]);
                    }
                }
            }
            return estaca;
        }

        public string GetAlumnoLapIng(string Cedula)
        {
            string laping = "";
            Parametros.Clear();
            Parametros.Add("@Cedula", Cedula);
            dt = dbCon.Procedure("PRD", "AlumnosSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        laping = Convert.ToString(dt.Rows[i]["LapIng"]);
                    }
                }
            }
            return laping;
        }

        public bool GetAlumnoDocumentosPorConsignar(string cedula)
        {
            int value = 0;
            // 1 no debe documentos 0 si debe documentos
            Parametros.Clear();
            Parametros.Add("@Cedula", cedula);
            dt = dbCon.Procedure("PRD", "SinDocumentosSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        value = Convert.ToInt16(dt.Rows[i]["Verificacion"]);
                    }
                }
            }
            return value == 1 ? false : true;
        }
    }
}