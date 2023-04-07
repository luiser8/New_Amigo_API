using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using PSMApiRest.Lib;
using PSMApiRest.Models;

namespace PSMApiRest.DAL
{
    public class InscripcionesDAL
    {
        private readonly DB dbCon;
        private DataTable dt;
        private readonly Hashtable Parametros;

        public InscripcionesDAL()
        {
            dt = new DataTable();
            dbCon = new DB();
            Parametros = new Hashtable();
        }
        public List<Inscripciones> GetInscripciones(Inscripciones inscripcionesRequest)
        {
            Parametros.Clear();
            Parametros.Add("@Id_Periodo", inscripcionesRequest.Id_Periodo);
            Parametros.Add("@Plan0", inscripcionesRequest.Plan0 != null ? inscripcionesRequest.Plan0 : 0);
            Parametros.Add("@Plan1", inscripcionesRequest.Plan1 != null ? inscripcionesRequest.Plan1 : 0);
            Parametros.Add("@Plan2", inscripcionesRequest.Plan2 != null ? inscripcionesRequest.Plan2 : 0);
            Parametros.Add("@Plan3", inscripcionesRequest.Plan3 != null ? inscripcionesRequest.Plan3 : 0);
            Parametros.Add("@Plan4", inscripcionesRequest.Plan4 != null ? inscripcionesRequest.Plan4 : 0);
            Parametros.Add("@Plan5", inscripcionesRequest.Plan5 != null ? inscripcionesRequest.Plan5 : 0);
            Parametros.Add("@Plan6", inscripcionesRequest.Plan6 != null ? inscripcionesRequest.Plan6 : 0);
            Parametros.Add("@Plan7", inscripcionesRequest.Plan7 != null ? inscripcionesRequest.Plan7 : 0);
            Parametros.Add("@Plan8", inscripcionesRequest.Plan8 != null ? inscripcionesRequest.Plan8 : 0);
            Parametros.Add("@Plan9", inscripcionesRequest.Plan9 != null ? inscripcionesRequest.Plan9 : 0);
            Parametros.Add("@Plan10", inscripcionesRequest.Plan10 != null ? inscripcionesRequest.Plan10 : 0);

            List<Inscripciones> InscripcionesList = new List<Inscripciones>();
            dt = dbCon.Procedure("AMIGO", "InscripcionesSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Inscripciones inscripciones = new Inscripciones();
                        inscripciones.Id_Inscripcion = Convert.ToInt32(dt.Rows[i]["Id_Inscripcion"]);
                        InscripcionesList.Add(inscripciones);
                    }
                }
            }
            return InscripcionesList;
        }

        public List<Inscripciones> GetIdInscripcion(string Lapso, string Identificador)
        {
            Parametros.Clear();
            Parametros.Add("@Lapso", Lapso);
            Parametros.Add("@Identificador", Identificador);

            List<Inscripciones> Id_Inscripcion = new List<Inscripciones>();
            dt = dbCon.Procedure("AMIGO", "IdInscripcionSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Inscripciones inscripciones = new Inscripciones();
                        inscripciones.Id_Terceros = Convert.ToInt32(dt.Rows[i]["Id_Terceros"]);
                        inscripciones.Id_Inscripcion = Convert.ToInt32(dt.Rows[i]["Id_Inscripcion"]);
                        inscripciones.Id_Plan = Convert.ToInt32(dt.Rows[i]["Id_Plan"]);
                        inscripciones.Id_TipoIngreso = Convert.ToInt32(dt.Rows[i]["Id_TipoIngreso"]);
                        inscripciones.Id_Carrera = Convert.ToInt32(dt.Rows[i]["Id_Carrera"]);
                        inscripciones.PlanDePago = Convert.ToString(dt.Rows[i]["PlanDePago"]);
                        inscripciones.TipoIngreso = Convert.ToString(dt.Rows[i]["TipoIngreso"]);
                        inscripciones.Telefonos = Convert.ToString(dt.Rows[i]["Telefonos"]);
                        inscripciones.Emails = Convert.ToString(dt.Rows[i]["EMail"]);
                        inscripciones.Fecha = Convert.ToDateTime(dt.Rows[i]["Fecha"]);
                        Id_Inscripcion.Add(inscripciones);
                    }
                }
            }
            return Id_Inscripcion;
        }
        public List<Inscripciones> InsertCuota(int Id_Inscripcion, int Id_Arancel, decimal Monto, string FechaVencimiento)
        {
            Parametros.Clear();
            Parametros.Add("@Id_Inscripcion", Id_Inscripcion);
            Parametros.Add("@Id_Arancel", Id_Arancel);
            Parametros.Add("@Monto", Monto);
            Parametros.Add("@FechaVencimiento", FechaVencimiento);

            List<Inscripciones> InscripcionesList = new List<Inscripciones>();
            dt = dbCon.Procedure("AMIGO", "DeudasSysAllInsert", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Inscripciones inscripciones = new Inscripciones();
                        inscripciones.Id_Inscripcion = Id_Inscripcion;
                        InscripcionesList.Add(inscripciones);
                    }
                }
            }
            return InscripcionesList;
        }
        public List<Inscripciones> UpdateInscripcion(int Id_Inscripcion, int Id_Plan, int Id_TipoIngreso)
        {
            Parametros.Clear();
            Parametros.Add("@Id_Inscripcion", Id_Inscripcion);
            Parametros.Add("@Id_Plan", Id_Plan);
            Parametros.Add("@Id_TipoIngreso", Id_TipoIngreso);

            List<Inscripciones> InscripcionesList = new List<Inscripciones>();
            dt = dbCon.Procedure("AMIGO", "InscripcionSysUpdate", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Inscripciones inscripciones = new Inscripciones();
                        inscripciones.Id_Inscripcion = Id_Inscripcion;
                        InscripcionesList.Add(inscripciones);
                    }
                }
            }
            return InscripcionesList;
        }
        public List<Inscripciones> DeleteInscripcion(int Id_Inscripcion)
        {
            Parametros.Clear();
            Parametros.Add("@Id_Inscripcion", Id_Inscripcion);

            List<Inscripciones> InscripcionesList = new List<Inscripciones>();
            dt = dbCon.Procedure("AMIGO", "InscripcionSysDelete", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Inscripciones inscripciones = new Inscripciones();
                        inscripciones.Id_Inscripcion = Id_Inscripcion;
                        InscripcionesList.Add(inscripciones);
                    }
                }
            }
            return InscripcionesList;
        }
    }
}