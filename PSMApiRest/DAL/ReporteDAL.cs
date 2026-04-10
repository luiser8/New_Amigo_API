using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PSMApiRest.Lib;
using PSMApiRest.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

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
        public List<Reporte> GetReporteDeudas(string Lapso, byte Pagada = 0)
        {
            Parametros.Clear();
            Parametros.Add("@Lapso", Lapso);

            List<Reporte> reporteList = new List<Reporte>();
            dt = dbCon.Procedure("AMIGO_PUERTA", "ListadoDeudas", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Reporte reporte = new Reporte();
                        //CuotaDAL cuotaDAL = new CuotaDAL();
                        reporte.Lapso = Convert.ToString(dt.Rows[i]["Lapso"]);
                        reporte.Fullnombre = Convert.ToString(dt.Rows[i]["Fullnombre"]);
                        reporte.Identificador = Convert.ToString(dt.Rows[i]["Identificador"]);
                        reporte.Telefonos = Convert.ToString(dt.Rows[i]["Telefonos"]);
                        reporte.Email = Convert.ToString(dt.Rows[i]["Email"]);
                        reporte.Descripcion = Convert.ToString(dt.Rows[i]["Descripcion"]);
                        //reporte.Cuota = Convert.ToString(dt.Rows[i]["Cuota"]);
                        //reporte.Dolar = reporte.Cuota.Contains("SAIA") ? cuotaDAL.SingleCuota(1, Lapso) : cuotaDAL.SingleCuota(2, Lapso);
                        reporte.Monto = Convert.ToDecimal(dt.Rows[i]["Monto"]);
                        reporte.Carrera = Convert.ToString(dt.Rows[i]["Nivel"]);
                        reporte.Secciones = Convert.ToString(dt.Rows[i]["Secciones"]);
                        //reporte.MontoFacturas = Convert.ToDecimal(dt.Rows[i]["MontoFacturas"]);
                        //reporte.Total = Math.Floor(Convert.ToDecimal(dt.Rows[i]["Total"]) * 100) / 100;
                        reporteList.Add(reporte);
                    }
                }
            }
            return reporteList;
        }

        public List<Reporte> GetReporteDeudasPorConceptos(string Lapso, int IdArancel, byte Pagada = 0)
        {
            Parametros.Clear();
            Parametros.Add("@Lapso", Lapso);
            Parametros.Add("@Arancel", IdArancel);
            Parametros.Add("@Pagada", Pagada);

            List<Reporte> reporteList = new List<Reporte>();
            dt = dbCon.Procedure("AMIGO_PUERTA", "ListadoDeudasPorConceptos", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Reporte reporte = new Reporte();
                        //CuotaDAL cuotaDAL = new CuotaDAL();
                        reporte.Lapso = Convert.ToString(dt.Rows[i]["Lapso"]);
                        reporte.Fullnombre = Convert.ToString(dt.Rows[i]["Fullnombre"]);
                        reporte.Identificador = Convert.ToString(dt.Rows[i]["Identificador"]);
                        reporte.Telefonos = Convert.ToString(dt.Rows[i]["Telefonos"]);
                        reporte.Email = Convert.ToString(dt.Rows[i]["Email"]);
                        //reporte.Descripcion = Convert.ToString(dt.Rows[i]["Descripcion"]);
                        //reporte.Cuota = Convert.ToString(dt.Rows[i]["Cuota"]);
                        //reporte.Dolar = reporte.Cuota.Contains("SAIA") ? cuotaDAL.SingleCuota(1, Lapso) : cuotaDAL.SingleCuota(2, Lapso);
                        reporte.Monto = Convert.ToDecimal(dt.Rows[i]["Monto"]);
                        reporte.Carrera = Convert.ToString(dt.Rows[i]["Nivel"]);
                        reporte.Concepto = Convert.ToString(dt.Rows[i]["Concepto"]);
                        //reporte.Secciones = Convert.ToString(dt.Rows[i]["Secciones"]);
                        //reporte.MontoFacturas = Convert.ToDecimal(dt.Rows[i]["MontoFacturas"]);
                        //reporte.Total = Math.Floor(Convert.ToDecimal(dt.Rows[i]["Total"]) * 100) / 100;
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
        public List<ReportePorCarreras> GetReporteAllCarreras(int IdPeriodo, string Desde, string Hasta)
        {
            Parametros.Clear();
            Parametros.Add("@IdPeriodo", IdPeriodo);
            Parametros.Add("@Desde", Desde);
            Parametros.Add("@Hasta", Hasta);

            List<ReportePorCarreras> reporteList = new List<ReportePorCarreras>();
            dt = dbCon.Procedure("AMIGO", "ReporteCarreraSys", Parametros);

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
        public List<ReporteFacturacion> GetReporteFacturacion(string FechaDesde, string FechaHasta, int IdBanco, int Tipo)
        {
            Parametros.Clear();
            Parametros.Add("@FechaDesde", FechaDesde);
            Parametros.Add("@FechaHasta", FechaHasta);
            Parametros.Add("@IdBanco", IdBanco);
            Parametros.Add("@Tipo", Tipo);

            List<ReporteFacturacion> reporteList = new List<ReporteFacturacion>();
            dt = dbCon.Procedure("AMIGO", "ReporteFacturacionSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ReporteFacturacion reporte = new ReporteFacturacion
                        {
                            FechaDelPago = Convert.ToDateTime(dt.Rows[i]["FechaDelPago"]),
                            NroReferencia = Convert.ToString(dt.Rows[i]["NroReferencia"]),
                            NombresYapellidos = Convert.ToString(dt.Rows[i]["NombresYapellidos"]),
                            Cedula = Convert.ToString(dt.Rows[i]["Cedula"]),
                            Escuela = Convert.ToString(dt.Rows[i]["Escuela"]),
                            Monto = Convert.ToDecimal(dt.Rows[i]["Monto"]),
                            MontoIP = Convert.ToDecimal(dt.Rows[i]["MontoIP"]),
                            Concepto = Convert.ToString(dt.Rows[i]["Concepto"]),
                            FechaRegistroPago = Convert.ToDateTime(dt.Rows[i]["FechaRegistroPago"]),
                            NroReciboCaja = Convert.ToString(dt.Rows[i]["NroReciboCaja"]),
                            Tipo = Convert.ToInt16(dt.Rows[i]["Tipo"])
                        };
                        reporteList.Add(reporte);
                    }
                }
            }
            return reporteList;
        }
        public List<ReporteFacturacionDepositos> GetReporteFacturacionPorDepositos(string FechaDesde, string FechaHasta, int IdBanco)
        {
            Parametros.Clear();
            Parametros.Add("@FechaDesde", FechaDesde);
            Parametros.Add("@FechaHasta", FechaHasta);
            Parametros.Add("@Banco", IdBanco);

            List<ReporteFacturacionDepositos> reporteList = new List<ReporteFacturacionDepositos>();
            dt = dbCon.Procedure("AMIGO_PUERTA", "ReporteFacturacionDepositos", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ReporteFacturacionDepositos reporte = new ReporteFacturacionDepositos
                        {
                            IdFactura = Convert.ToInt32(dt.Rows[i]["Id_Factura"]),
                            DocumentosPago = DeserializarDepositos(dt.Rows[i]),
                            Identificador = Convert.ToString(dt.Rows[i]["Identificador"]),
                            Fullnombre = Convert.ToString(dt.Rows[i]["ApellidosYNombres"]),
                            ConceptosPago = DeserializarConceptos(dt.Rows[i]),
                            MontoTotal = Convert.ToDecimal(dt.Rows[i]["MontoTotal"]),
                        };
                        reporteList.Add(reporte);
                    }
                }
            }
            return reporteList;
        }
        public CierreCajaTotalResponse GetReporteCierreCaja(string FechaDesde, string FechaHasta)
        {
            var response = new CierreCajaTotalResponse
            {
                Datos = new List<CierreCajaResponse>(),
                Fechas = new List<string>(),
                FechaDesde = DateTime.Parse(FechaDesde),
                FechaHasta = DateTime.Parse(FechaHasta)
            };

            Parametros.Clear();
            Parametros.Add("@FechaDesde", FechaDesde);
            Parametros.Add("@FechaHasta", FechaHasta);

            dt = dbCon.Procedure("AMIGO_PUERTA", "ReporteFacturacionCierre", Parametros);

            if (dbCon.ErrorEstatus && dt.Rows.Count > 0)
            {
                // Obtener las columnas de fechas y ordenarlas de menor a mayor
                var fechasColumns = new List<string>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName != "Banco" && col.ColumnName != "TotalPorBanco")
                    {
                        fechasColumns.Add(col.ColumnName);
                    }
                }

                // Ordenar las fechas de menor a mayor
                response.Fechas = fechasColumns
                    .Select(f => DateTime.ParseExact(f, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                    .OrderBy(d => d)
                    .Select(d => d.ToString("dd/MM/yyyy"))
                    .ToList();

                // Procesar cada fila
                foreach (DataRow row in dt.Rows)
                {
                    var reporte = new CierreCajaResponse
                    {
                        Banco = row["Banco"].ToString(),
                        DepositosPorFecha = new Dictionary<string, decimal>(),
                        TotalPorBanco = row["TotalPorBanco"] != DBNull.Value ? Convert.ToDecimal(row["TotalPorBanco"]) : 0
                    };

                    // Cargar los montos por fecha usando las fechas ordenadas
                    foreach (string fecha in response.Fechas)
                    {
                        decimal monto = row[fecha] != DBNull.Value ? Convert.ToDecimal(row[fecha]) : 0;
                        reporte.DepositosPorFecha.Add(fecha, monto);
                    }

                    response.Datos.Add(reporte);
                }

                // Calcular el total general (excluyendo la fila TOTAL si existe)
                var filasSinTotal = response.Datos.Where(d => d.Banco != "TOTAL").ToList();
                response.TotalGeneral = filasSinTotal.Sum(d => d.TotalPorBanco);
            }

            return response;
        }
        private List<ConceptosPago> DeserializarConceptos(DataRow row)
        {
            var jsonString = row["ConceptosPago"]?.ToString();

            if (string.IsNullOrEmpty(jsonString) || jsonString == "[]")
                return new List<ConceptosPago>();

            try
            {
                // Limpiar el JSON si es necesario
                jsonString = System.Text.RegularExpressions.Regex.Replace(jsonString, @"\s+", " ");

                var settings = new JsonSerializerSettings
                {
                    DateFormatString = "yyyy-MM-dd",
                    NullValueHandling = NullValueHandling.Ignore
                };

                return JsonConvert.DeserializeObject<List<ConceptosPago>>(jsonString, settings);
            }
            catch (JsonException _)
            {
                // 🔴 Intentar una solución más directa
                try
                {
                    // Si el JSON viene como objeto en lugar de array
                    if (jsonString.Trim().StartsWith("{"))
                    {
                        var singleObject = JsonConvert.DeserializeObject<ConceptosPago>(jsonString);
                        return new List<ConceptosPago> { singleObject };
                    }

                    // Intentar reparar el JSON común de SQL Server 2014
                    jsonString = jsonString
                        .Replace("\"{", "{")
                        .Replace("}\"", "}")
                        .Replace("\\\"", "\"");

                    // Quitar comillas dobles extras alrededor del array
                    if (jsonString.StartsWith("\"[") && jsonString.EndsWith("]\""))
                    {
                        jsonString = jsonString.Substring(2, jsonString.Length - 4);
                    }

                    var jArray = JArray.Parse(jsonString);
                    return jArray.ToObject<List<ConceptosPago>>();
                }
                catch (Exception ex2)
                {
                    Console.WriteLine($"Error en fallback: {ex2.Message}");
                    Console.WriteLine($"JSON problemático: {jsonString}");
                    return new List<ConceptosPago>();
                }
            }
        }
        private List<DocumentosPago> DeserializarDepositos(DataRow row)
        {
            var jsonString = row["DocumentosPago"]?.ToString();

            if (string.IsNullOrEmpty(jsonString) || jsonString == "[]")
                return new List<DocumentosPago>();

            try
            {
                // Limpiar el JSON si es necesario
                jsonString = System.Text.RegularExpressions.Regex.Replace(jsonString, @"\s+", " ");

                var settings = new JsonSerializerSettings
                {
                    //DateFormatString = "yyyy-MM-dd",
                    NullValueHandling = NullValueHandling.Ignore
                };

                return JsonConvert.DeserializeObject<List<DocumentosPago>>(jsonString, settings);
            }
            catch (JsonException _)
            {
                // 🔴 Intentar una solución más directa
                try
                {
                    // Si el JSON viene como objeto en lugar de array
                    if (jsonString.Trim().StartsWith("{"))
                    {
                        var singleObject = JsonConvert.DeserializeObject<DocumentosPago>(jsonString);
                        return new List<DocumentosPago> { singleObject };
                    }

                    // Intentar reparar el JSON común de SQL Server 2014
                    jsonString = jsonString
                        .Replace("\"{", "{")
                        .Replace("}\"", "}")
                        .Replace("\\\"", "\"");

                    // Quitar comillas dobles extras alrededor del array
                    if (jsonString.StartsWith("\"[") && jsonString.EndsWith("]\""))
                    {
                        jsonString = jsonString.Substring(2, jsonString.Length - 4);
                    }

                    var jArray = JArray.Parse(jsonString);
                    return jArray.ToObject<List<DocumentosPago>>();
                }
                catch (Exception ex2)
                {
                    Console.WriteLine($"Error en fallback: {ex2.Message}");
                    Console.WriteLine($"JSON problemático: {jsonString}");
                    return new List<DocumentosPago>();
                }
            }
        }
    }
}