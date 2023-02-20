using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using PSMApiRest.Lib;
using PSMApiRest.Models;

namespace PSMApiRest.DAL
{
    public class ReporteDAL
    {
        private readonly DB dbCon;
        private DataTable dt;
        private readonly Hashtable Parametros;

        public ReporteDAL()
        {
            dt = new DataTable();
            dbCon = new DB();
            Parametros = new Hashtable();
        }
        public List<Reporte> GetReporteDeudas(string Lapso, byte Pagada)
        {
            Parametros.Clear();
            Parametros.Add("@Lapso", Lapso);
            Parametros.Add("@Pagada", Pagada);

            List<Reporte> reporteList = new List<Reporte>();
            dt = dbCon.Procedure("AMIGO", "DeudasSysReporte", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Reporte reporte = new Reporte();
                        CuotaDAL cuotaDAL = new CuotaDAL();
                        reporte.Lapso = Convert.ToString(dt.Rows[i]["Lapso"]);
                        reporte.Fullnombre = Convert.ToString(dt.Rows[i]["Fullnombre"]);
                        reporte.Identificador = Convert.ToString(dt.Rows[i]["Identificador"]);
                        reporte.Telefonos = Convert.ToString(dt.Rows[i]["Telefonos"]);
                        reporte.Email = Convert.ToString(dt.Rows[i]["Email"]);
                        reporte.Descripcion = Convert.ToString(dt.Rows[i]["Descripcion"]);
                        reporte.Cuota = Convert.ToString(dt.Rows[i]["Cuota"]);
                        reporte.Dolar = reporte.Cuota.Contains("SAIA") ? cuotaDAL.SingleCuota(1, Lapso) : cuotaDAL.SingleCuota(2, Lapso);
                        reporte.Monto = Convert.ToDecimal(dt.Rows[i]["Monto"]);
                        reporte.MontoFacturas = Convert.ToDecimal(dt.Rows[i]["MontoFacturas"]);
                        reporte.Total = Math.Floor(Convert.ToDecimal(dt.Rows[i]["Total"]) * 100) / 100;
                        reporteList.Add(reporte);
                    }
                }
            }
            return reporteList;
        }
        public List<Reporte> GetReportePagadas(string Lapso)
        {
            Parametros.Clear();
            Parametros.Add("@Lapso", Lapso);

            List<Reporte> reporteList = new List<Reporte>();
            dt = dbCon.Procedure("AMIGO", "ReporteCuotasPagadasSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Reporte reporte = new Reporte();
                        CuotaDAL cuotaDAL = new CuotaDAL();
                        reporte.Lapso = Convert.ToString(dt.Rows[i]["Lapso"]);
                        reporte.IdFactura = Convert.ToInt64(dt.Rows[i]["Id_Factura"]);
                        reporte.Fullnombre = Convert.ToString(dt.Rows[i]["Fullnombre"]);
                        reporte.Identificador = Convert.ToString(dt.Rows[i]["Identificador"]);
                        reporte.Telefonos = Convert.ToString(dt.Rows[i]["Telefonos"]);
                        reporte.Email = Convert.ToString(dt.Rows[i]["Email"]);
                        reporte.Descripcion = Convert.ToString(dt.Rows[i]["Descripcion"]);
                        reporte.Cuota = Convert.ToString(dt.Rows[i]["Cuota"]);
                        reporte.Dolar = reporte.Cuota.Contains("SAIA") ? cuotaDAL.SingleCuota(2, Lapso) : cuotaDAL.SingleCuota(1, Lapso);
                        reporte.Monto = Convert.ToDecimal(dt.Rows[i]["Monto"]);
                        reporte.Fecha = Convert.ToDateTime(dt.Rows[i]["Fecha"]);
                        reporteList.Add(reporte);
                    }
                }
            }
            return reporteList;
        }
        public List<ReporteMenu> GetReporteMenu(int IdPeriodo, string Desde, string Hasta)
        {
            Parametros.Clear();
            Parametros.Add("@IdPeriodo", IdPeriodo);
            Parametros.Add("@Desde", Desde);
            Parametros.Add("@Hasta", Hasta);

            List<ReporteMenu> reporteList = new List<ReporteMenu>();
            dt = dbCon.Procedure("AMIGO", "ReporteMenuSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ReporteMenu reporte = new ReporteMenu();
                        reporte.IdPlan = Convert.ToInt32(dt.Rows[i]["Id_Plan"]);
                        reporte.PlanPago = Convert.ToString(dt.Rows[i]["PlanPago"]);
                        reporte.Inscritos = Convert.ToInt32(dt.Rows[i]["Inscritos"]);
                        reporteList.Add(reporte);
                    }
                }
            }
            return reporteList;
        }
        public List<ReporteMenuCarreras> GetReporteMenuCarreras(int IdPeriodo, string Desde, string Hasta)
        {
            Parametros.Clear();
            Parametros.Add("@IdPeriodo", IdPeriodo);
            Parametros.Add("@Desde", Desde);
            Parametros.Add("@Hasta", Hasta);

            List<ReporteMenuCarreras> reporteList = new List<ReporteMenuCarreras>();
            dt = dbCon.Procedure("AMIGO", "ReporteMenuPorCarreraSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ReporteMenuCarreras reporte = new ReporteMenuCarreras();
                        reporte.IdCarrera = Convert.ToInt32(dt.Rows[i]["Id_Carrera"]);
                        reporte.Carrera = Convert.ToString(dt.Rows[i]["Carrera"]);
                        reporte.Inscritos = Convert.ToInt32(dt.Rows[i]["Inscritos"]);
                        reporteList.Add(reporte);
                    }
                }
            }
            return reporteList;
        }
        public List<ReportePlanDePago> GetReportePlanDePago(int IdPeriodo, int IdPlan, string Desde, string Hasta)
        {
            Parametros.Clear();
            Parametros.Add("@IdPeriodo", IdPeriodo);
            Parametros.Add("@IdPlan", IdPlan);
            Parametros.Add("@Desde", Desde);
            Parametros.Add("@Hasta", Hasta);

            List<ReportePlanDePago> reporteList = new List<ReportePlanDePago>();
            dt = dbCon.Procedure("AMIGO", "ReportePlanesDePagosSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ReportePlanDePago reporte = new ReportePlanDePago();
                        //reporte.Sexo = Convert.ToInt32(dt.Rows[i]["Sexo"]);
                        //reporte.Id_Alumno = Convert.ToInt32(dt.Rows[i]["Id_Alumno"]);
                        reporte.Cedula = Convert.ToString(dt.Rows[i]["Cedula"]);
                        //reporte.Apellidos = Convert.ToString(dt.Rows[i]["Apellidos"]);
                        //reporte.Nombres = Convert.ToString(dt.Rows[i]["Nombres"]);
                        reporte.Telefonos = Convert.ToString(dt.Rows[i]["Telefonos"]);
                        reporte.EMail = Convert.ToString(dt.Rows[i]["EMail"]);
                        reporte.Carrera = Convert.ToString(dt.Rows[i]["Carrera"]);
                        reporte.TiposIngreso = Convert.ToString(dt.Rows[i]["TiposIngreso"]);
                        reporte.PlanDePago = Convert.ToString(dt.Rows[i]["PlanDePago"]);
                        reporte.Fecha = Convert.ToDateTime(dt.Rows[i]["Fecha"]);
                        reporteList.Add(reporte);
                    }
                }
            }
            return reporteList;
        }
        public List<ReportePorCarreras> GetReportePorCarreras(int IdPeriodo, int IdCarrera, string Desde, string Hasta)
        {
            Parametros.Clear();
            Parametros.Add("@IdPeriodo", IdPeriodo);
            Parametros.Add("@IdCarrera", IdCarrera);
            Parametros.Add("@Desde", Desde);
            Parametros.Add("@Hasta", Hasta);

            List<ReportePorCarreras> reporteList = new List<ReportePorCarreras>();
            dt = dbCon.Procedure("AMIGO", "ReportePorCarreraSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ReportePorCarreras reporte = new ReportePorCarreras();
                        //reporte.Sexo = Convert.ToInt32(dt.Rows[i]["Sexo"]);
                        //reporte.Id_Alumno = Convert.ToInt32(dt.Rows[i]["Id_Alumno"]);
                        reporte.Cedula = Convert.ToString(dt.Rows[i]["Cedula"]);
                        //reporte.Apellidos = Convert.ToString(dt.Rows[i]["Apellidos"]);
                        //reporte.Nombres = Convert.ToString(dt.Rows[i]["Nombres"]);
                        reporte.Telefonos = Convert.ToString(dt.Rows[i]["Telefonos"]);
                        reporte.EMail = Convert.ToString(dt.Rows[i]["EMail"]);
                        reporte.Carrera = Convert.ToString(dt.Rows[i]["Carrera"]);
                        reporte.TiposIngreso = Convert.ToString(dt.Rows[i]["TiposIngreso"]);
                        reporte.PlanDePago = Convert.ToString(dt.Rows[i]["PlanDePago"]);
                        reporte.Fecha = Convert.ToDateTime(dt.Rows[i]["Fecha"]);
                        reporteList.Add(reporte);
                    }
                }
            }
            return reporteList;
        }
    }
}