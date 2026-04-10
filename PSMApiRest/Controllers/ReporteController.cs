using ClosedXML.Excel;
using Microsoft.Ajax.Utilities;
using PSMApiRest.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace PSMApiRest.Controllers
{
    [Authorize]
    [RoutePrefix("api/reporte")]
    public class ReporteController : ApiController
    {
        readonly ReporteDAL reporteDAL = new ReporteDAL();
        readonly BancosDAL bancosDAL = new BancosDAL();

        /// <summary>
        /// Indicamos parametros para obtener reporte de deudas
        /// </summary>
        /// <param name="Lapso"></param>
        /// <param name="Pagada"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/reporte/deudas
        [HttpGet]
        [Route("deudas")]
        public HttpResponseMessage GetReporteDeudas([FromUri] string Lapso, byte Pagada)
        {
            DataTable dt = new DataTable("Cuentas");
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn("Lapso", typeof(string)),
                                            new DataColumn("Identificador", typeof(long)),
                                            new DataColumn("FullNombres", typeof(string)),
                                            new DataColumn("Telefonos", typeof(string)),
                                            new DataColumn("Email", typeof(string)),
                                            new DataColumn("Descripcion", typeof(string)),
                                            new DataColumn("Carrera", typeof(string)),
                                            new DataColumn("Secciones", typeof(string)),
                                            new DataColumn("Monto", typeof(decimal))
            });

            foreach (var reporte in reporteDAL.GetReporteDeudas(Lapso, Pagada))
            {
               dt.Rows.Add(reporte.Lapso, reporte.Identificador, reporte.Fullnombre, reporte.Telefonos, reporte.Email, reporte.Descripcion, reporte.Carrera, reporte.Secciones, reporte.Monto);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {

                using (MemoryStream stream = new MemoryStream())
                {
                    var wwb = wb.Worksheets.Add(dt);
                    wwb.Columns().AdjustToContents();
                    wb.SaveAs(stream);
                    
                    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                    result.Content = new ByteArrayContent(stream.GetBuffer());
                    result.Content.Headers.ContentLength = stream.Length;
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "reporte deudas" + "_" + DateTime.Now.ToShortDateString() + ".xlsx"
                    };
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    return result;
                }
            }
        }
        /// <summary>
        /// Indicamos parametros para obtener reporte de deudas por conceptos
        /// </summary>
        /// <param name="Lapso"></param>
        /// <param name="Pagada"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/reporte/por_conceptos
        [HttpGet]
        [Route("por_conceptos")]
        public HttpResponseMessage GetReporteDeudasPorConceptos([FromUri] string Lapso, int IdArancel, byte Pagada)
        {
            DataTable dt = new DataTable("Deudas Por Conceptos");
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("Lapso", typeof(string)),
                                            new DataColumn("Identificador", typeof(long)),
                                            new DataColumn("FullNombres", typeof(string)),
                                            new DataColumn("Telefonos", typeof(string)),
                                            new DataColumn("Email", typeof(string)),
                                           // new DataColumn("Descripcion", typeof(string)),
                                            new DataColumn("Carrera", typeof(string)),
                                            new DataColumn("Concepto", typeof(string)),
                                            new DataColumn("Monto", typeof(decimal))
            });

            foreach (var reporte in reporteDAL.GetReporteDeudasPorConceptos(Lapso, IdArancel, Pagada))
            {
                dt.Rows.Add(reporte.Lapso, reporte.Identificador, reporte.Fullnombre, reporte.Telefonos, reporte.Email, reporte.Carrera, reporte.Concepto, reporte.Monto);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {

                using (MemoryStream stream = new MemoryStream())
                {
                    var wwb = wb.Worksheets.Add(dt);
                    wwb.Columns().AdjustToContents();
                    wb.SaveAs(stream);

                    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                    result.Content = new ByteArrayContent(stream.GetBuffer());
                    result.Content.Headers.ContentLength = stream.Length;
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "reporte deudas por conceptos" + "_" + DateTime.Now.ToShortDateString() + ".xlsx"
                    };
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    return result;
                }
            }
        }
        /// <summary>
        /// Indicamos parametros para obtener reporte de deudas pagadas
        /// </summary>
        /// <param name="Lapso"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/reporte/pagadas
        [HttpGet]
        [Route("pagadas")]
        public HttpResponseMessage GetReportePagadas([FromUri] string Lapso)
        {
            DataTable dt = new DataTable("Pagadas");
            dt.Columns.AddRange(new DataColumn[11] { new DataColumn("Lapso", typeof(string)),
                                            new DataColumn("IdFactura", typeof(long)),
                                            new DataColumn("Identificador", typeof(Int32)),
                                            new DataColumn("FullNombres", typeof(string)),
                                            new DataColumn("Telefonos", typeof(string)),
                                            new DataColumn("Email", typeof(string)),
                                            new DataColumn("Descripcion", typeof(string)),
                                            new DataColumn("Cuota", typeof(string)),
                                            new DataColumn("Dolar", typeof(decimal)),
                                            new DataColumn("Monto", typeof(decimal)),
                                            new DataColumn("Fecha", typeof(DateTime))
            });

            foreach (var reporte in reporteDAL.GetReportePagadas(Lapso))
            {
                dt.Rows.Add(reporte.Lapso, reporte.IdFactura, reporte.Identificador, reporte.Fullnombre, reporte.Telefonos, reporte.Email, reporte.Descripcion, reporte.Cuota, reporte.Dolar, reporte.Monto, reporte.Fecha);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {

                using (MemoryStream stream = new MemoryStream())
                {
                    var wwb = wb.Worksheets.Add(dt);
                    wwb.Columns().AdjustToContents();
                    wb.SaveAs(stream);

                    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                    result.Content = new ByteArrayContent(stream.GetBuffer());
                    result.Content.Headers.ContentLength = stream.Length;
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "reporte pagadas" + "_" + DateTime.Now.ToShortDateString() + ".xlsx"
                    };
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    return result;
                }
            }
        }
        /// <summary>
        /// Indicamos parametros para obtener reporte inscritos por planes de pagos
        /// </summary>
        /// <param name="IdPeriodo"></param>
        /// <param name="IdPlan"></param>
        /// <param name="Desde"></param>
        /// <param name="Hasta"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/reporte/planesDePago
        [HttpGet]
        [Route("planesDePago")]
        public HttpResponseMessage GetReportePlanDePago([FromUri] int IdPeriodo, int IdPlan, string Desde, string Hasta)
        {
            DataTable dt = new DataTable("PlanesDePago");
            dt.Columns.AddRange(new DataColumn[7] { 
                                            //new DataColumn("Sexo", typeof(string)),
                                            new DataColumn("Cedula", typeof(string)),
                                            //new DataColumn("Apellidos", typeof(string)),
                                            //new DataColumn("Nombres", typeof(string)),
                                            new DataColumn("Telefonos", typeof(string)),
                                            new DataColumn("Email", typeof(string)),
                                            new DataColumn("Carrera", typeof(string)),
                                            new DataColumn("Tipo de ingreso", typeof(string)),
                                            new DataColumn("Plan de pago", typeof(string)),
                                            new DataColumn("Fecha de inscripcion", typeof(DateTime))
            });

            foreach (var reporte in reporteDAL.GetReportePlanDePago(IdPeriodo, IdPlan, Desde, Hasta))
            {
                dt.Rows.Add(/*reporte.Sexo,*/ reporte.Cedula, /*reporte.Apellidos, reporte.Nombres,*/ reporte.Telefonos, reporte.EMail, reporte.Carrera, reporte.TiposIngreso, reporte.PlanDePago, reporte.Fecha);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {

                using (MemoryStream stream = new MemoryStream())
                {
                    var wwb = wb.Worksheets.Add(dt);
                    wwb.Columns().AdjustToContents();
                    wb.SaveAs(stream);

                    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                    result.Content = new ByteArrayContent(stream.GetBuffer());
                    result.Content.Headers.ContentLength = stream.Length;
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "reporte planes" + "_" + DateTime.Now.ToShortDateString() + ".xlsx"
                    };
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    return result;
                }
            }
        }
        /// <summary>
        /// Indicamos parametros para obtener reporte inscritos por carreras
        /// </summary>
        /// <param name="IdPeriodo"></param>
        /// <param name="IdCarrera"></param>
        /// <param name="Desde"></param>
        /// <param name="Hasta"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/reporte/porcarreras
        [HttpGet]
        [Route("porcarreras")]
        public HttpResponseMessage GetReportePorCarreras([FromUri] int IdPeriodo, int IdCarrera, string Desde, string Hasta)
        {
            DataTable dt = new DataTable("porcarreras");
            dt.Columns.AddRange(new DataColumn[7] { 
                                            //new DataColumn("Sexo", typeof(string)),
                                            new DataColumn("Cedula", typeof(string)),
                                            //new DataColumn("Apellidos", typeof(string)),
                                            //new DataColumn("Nombres", typeof(string)),
                                            new DataColumn("Telefonos", typeof(string)),
                                            new DataColumn("Email", typeof(string)),
                                            new DataColumn("Carrera", typeof(string)),
                                            new DataColumn("Tipo de ingreso", typeof(string)),
                                            new DataColumn("Plan de pago", typeof(string)),
                                            new DataColumn("Fecha de inscripcion", typeof(DateTime))
            });

            foreach (var reporte in reporteDAL.GetReportePorCarreras(IdPeriodo, IdCarrera, Desde, Hasta))
            {
                dt.Rows.Add(/*reporte.Sexo,*/ reporte.Cedula, /*reporte.Apellidos, reporte.Nombres,*/ reporte.Telefonos, reporte.EMail, reporte.Carrera, reporte.TiposIngreso, reporte.PlanDePago, reporte.Fecha);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {

                using (MemoryStream stream = new MemoryStream())
                {
                    var wwb = wb.Worksheets.Add(dt);
                    wwb.Columns().AdjustToContents();
                    wb.SaveAs(stream);

                    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                    result.Content = new ByteArrayContent(stream.GetBuffer());
                    result.Content.Headers.ContentLength = stream.Length;
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "reporte carreras" + "_" + DateTime.Now.ToShortDateString() + ".xlsx"
                    };
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    return result;
                }
            }
        }
        /// <summary>
        /// Indicamos parametros para obtener reporte inscritos por todas las carreras
        /// </summary>
        /// <param name="IdPeriodo"></param>
        /// <param name="Desde"></param>
        /// <param name="Hasta"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/reporte/allcarreras
        [HttpGet]
        [Route("allcarreras")]
        public HttpResponseMessage GetReporteAllCarreras([FromUri] int IdPeriodo, string Desde, string Hasta)
        {
            DataTable dt = new DataTable("allcarreras");
            dt.Columns.AddRange(new DataColumn[7] { 
                                            new DataColumn("Cedula", typeof(string)),
                                            new DataColumn("Telefonos", typeof(string)),
                                            new DataColumn("Email", typeof(string)),
                                            new DataColumn("Carrera", typeof(string)),
                                            new DataColumn("Tipo de ingreso", typeof(string)),
                                            new DataColumn("Plan de pago", typeof(string)),
                                            new DataColumn("Fecha de inscripcion", typeof(DateTime))
            });

            foreach (var reporte in reporteDAL.GetReporteAllCarreras(IdPeriodo, Desde, Hasta))
            {
                dt.Rows.Add(reporte.Cedula, reporte.Telefonos, reporte.EMail, reporte.Carrera, reporte.TiposIngreso, reporte.PlanDePago, reporte.Fecha);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {

                using (MemoryStream stream = new MemoryStream())
                {
                    var wwb = wb.Worksheets.Add(dt);
                    wwb.Columns().AdjustToContents();
                    wb.SaveAs(stream);

                    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                    result.Content = new ByteArrayContent(stream.GetBuffer());
                    result.Content.Headers.ContentLength = stream.Length;
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "reporte carreras" + "_" + DateTime.Now.ToShortDateString() + ".xlsx"
                    };
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    return result;
                }
            }
        }
        /// <summary>
        /// Indicamos parametros para obtener menu de planes de pago
        /// </summary>
        /// <param name="IdPeriodo"></param>
        /// <param name="Desde"></param>
        /// <param name="Hasta"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/reporte/menu
        [HttpGet]
        [Route("menu")]
        public IHttpActionResult GetReporteMenu([FromUri] int IdPeriodo, string Desde, string Hasta)
        {
            if (IdPeriodo != null)
            {
                try
                {
                    return Ok(reporteDAL.GetReporteMenu(IdPeriodo, Desde, Hasta).ToList());
                }
                catch (Exception ex)
                {
                    return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
        /// <summary>
        /// Indicamos parametros para obtener menu de carreras
        /// </summary>
        /// <param name="IdPeriodo"></param>
        /// <param name="Desde"></param>
        /// <param name="Hasta"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/reporte/menucarreras
        [HttpGet]
        [Route("menucarreras")]
        public IHttpActionResult GetReporteMenuCarreras([FromUri] int IdPeriodo, string Desde, string Hasta)
        {
            if (IdPeriodo != null)
            {
                try
                {
                    return Ok(reporteDAL.GetReporteMenuCarreras(IdPeriodo, Desde, Hasta).ToList());
                }
                catch (Exception ex)
                {
                    return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
        /// <summary>
        /// Indicamos parametros para obtener reporte de facturacion
        /// </summary>
        /// <param name="FechaDesde"></param>
        /// <param name="FechaHasta"></param>
        /// <param name="IdBanco"></param>
        /// <param name="Tipo"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/reporte/facturacion
        [HttpGet]
        [Route("facturacion")]
        public HttpResponseMessage GetReporteFacturacion([FromUri] string FechaDesde, string FechaHasta, int IdBanco, int Tipo)
        {
            DataTable dt = new DataTable("Facturacion");
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn("FechaDelPago", typeof(DateTime)),
                                            new DataColumn("NroReferencia", typeof(string)),
                                            new DataColumn("NombresYapellidos", typeof(string)),
                                            new DataColumn("Cedula", typeof(string)),
                                            new DataColumn("Escuela", typeof(string)),
                                            new DataColumn("Monto", typeof(decimal)),
                                            new DataColumn("Concepto", typeof(string)),
                                            new DataColumn("FechaRegistroPago", typeof(DateTime)),
                                            new DataColumn("NroFactura", typeof(string))
            });

            foreach (var reporte in reporteDAL.GetReporteFacturacion(FechaDesde, FechaHasta, IdBanco, Tipo))
            {
                dt.Rows.Add(reporte.FechaDelPago, reporte.NroReferencia, reporte.NombresYapellidos, reporte.Cedula, reporte.Escuela, reporte.Monto, reporte.Concepto, reporte.FechaRegistroPago, reporte.NroReciboCaja);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {

                using (MemoryStream stream = new MemoryStream())
                {
                    var wwb = wb.Worksheets.Add(dt);
                    wwb.Columns().AdjustToContents();
                    wb.SaveAs(stream);

                    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(stream.GetBuffer())
                    };
                    result.Content.Headers.ContentLength = stream.Length;
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "reporte facturacion" + "_" + DateTime.Now.ToShortDateString() + ".xlsx"
                    };
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    return result;
                }
            }
        }
        /// <summary>
        /// Indicamos parametros para obtener reporte de facturacion
        /// </summary>
        /// <param name="FechaDesde"></param>
        /// <param name="FechaHasta"></param>
        /// <param name="IdBanco"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/reporte/depositos
        [HttpGet]
        [Route("depositos")]
        public HttpResponseMessage GetReporteFacturacionDepositos([FromUri] string FechaDesde, string FechaHasta, int IdBanco)
        {
            DataTable dt = new DataTable("Depositos_Facturacion");
            dt.Columns.AddRange(new DataColumn[6] { new DataColumn("Factura", typeof(string)),
                            new DataColumn("Documentos de Pagos", typeof(string)),
                            new DataColumn("Cedula", typeof(string)),
                            new DataColumn("Nombres y Apellidos", typeof(string)),
                            new DataColumn("Por Conceptos de", typeof(string)),
                            new DataColumn("Monto", typeof(decimal))
    });

            if (string.IsNullOrWhiteSpace(FechaDesde) || string.IsNullOrWhiteSpace(FechaHasta) || IdBanco == null)
                return null;

            var response = reporteDAL.GetReporteFacturacionPorDepositos(FechaDesde, FechaHasta, IdBanco);
            decimal sumatoriaTotal = response.Sum(r => r.MontoTotal);

            foreach (var reporte in response)
            {
                string depositos = string.Join(" | ", reporte.DocumentosPago.Select(d =>
                    $"{d.Referencia} | {d.Banco} | {d.Fecha} | {d.Monto}"
                ));

                string conceptos = string.Join(" | ", reporte.ConceptosPago.Select(c =>
                    $"{c.Concepto} | {c.Monto}"
                ));

                dt.Rows.Add(
                    Convert.ToString(reporte.IdFactura),
                    depositos,
                    reporte.Identificador,
                    reporte.Fullnombre,
                    conceptos,
                    reporte.MontoTotal
                );
            }

            using (XLWorkbook wb = new XLWorkbook())
            using (MemoryStream stream = new MemoryStream())
            {
                // Agregar el título ENCIMA de los headers
                var ws = wb.Worksheets.Add("Depositos_Facturacion");

                // Insertar una fila al inicio para el título
                // ws.InsertRowsAbove(1, 1);

                // Colocar el título con el rango de fechas
                ws.Cell(1, 1).Value = $"DESDE {FechaDesde:dd/MM/yyyy} HASTA {FechaHasta:dd/MM/yyyy} DE {bancosDAL.GetBanco(IdBanco).Descripcion}";
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Font.FontSize = 14;
                ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                // Combinar el título desde la columna 1 hasta la 6
                ws.Range(1, 1, 1, 6).Merge();

                // Agregar los datos del DataTable comenzando desde la fila 2
                ws.Cell(2, 1).InsertTable(dt, true);

                // Obtener la última fila con datos (excluyendo el título)
                int lastDataRow = dt.Rows.Count + 1; // +1 por la fila de headers

                // Escribir la sumatoria en la columna de Monto (columna F) debajo de los datos
                ws.Cell(lastDataRow + 2, 6).Value = sumatoriaTotal;
                ws.Cell(lastDataRow + 2, 6).Style.Font.Bold = true;
                ws.Cell(lastDataRow + 2, 6).Style.NumberFormat.Format = "#,##0.00";

                // Agregar texto "TOTAL:" en la primera columna de la fila de total
                ws.Cell(lastDataRow + 2, 1).Value = "TOTAL:";
                ws.Cell(lastDataRow + 2, 1).Style.Font.Bold = true;

                // Formato para la columna de Monto en los datos
                var montoColumn = ws.Column(6);
                montoColumn.Style.NumberFormat.Format = "#,##0.00";

                // Agregar un borde superior a la fila de total
                ws.Range(lastDataRow + 2, 1, lastDataRow + 2, 6).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                // Ajustar el ancho de las columnas
                ws.Columns().AdjustToContents();

                wb.SaveAs(stream);

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(stream.ToArray())
                };

                result.Content.Headers.ContentLength = stream.Length;

                var contentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = $"reporte_facturacion_{DateTime.Now:yyyyMMdd}.xlsx",
                    FileNameStar = $"reporte_facturacion_{DateTime.Now:yyyyMMdd}.xlsx"
                };

                result.Content.Headers.ContentDisposition = contentDisposition;
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                return result;
            }
        }
        /// <summary>
        /// Indicamos parametros para obtener reporte de cierres de caja
        /// </summary>
        /// <param name="FechaDesde">Fecha inicial del reporte</param>
        /// <param name="FechaHasta">Fecha final del reporte</param>
        /// <returns> 
        ///     Retorna un archivo Excel con el reporte de cierres de caja
        /// </returns>
        /// <response code="200">Retorno exitoso del archivo Excel</response>
        /// <response code="400">Error en los parámetros o sin registros</response> 
        // GET: api/reporte/cierrescaja
        [HttpGet]
        [Route("cierrescaja")]
        public HttpResponseMessage GetReporteCierresCaja([FromUri] string FechaDesde, string FechaHasta)
        {
            // Validar parámetros
            if (string.IsNullOrWhiteSpace(FechaDesde) || string.IsNullOrWhiteSpace(FechaHasta))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "Debe especificar las fechas de inicio y fin");
            }

            if (!DateTime.TryParse(FechaDesde, out DateTime fechaDesde) ||
                !DateTime.TryParse(FechaHasta, out DateTime fechaHasta))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "Formato de fecha inválido. Use yyyy-MM-dd");
            }

            if (fechaDesde > fechaHasta)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "La fecha inicial no puede ser mayor que la fecha final");
            }

            // Obtener los datos del reporte
            var response = reporteDAL.GetReporteCierreCaja(FechaDesde, FechaHasta);

            if (response == null || response.Datos == null || response.Datos.Count == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "No se encontraron registros para el rango de fechas especificado");
            }

            // Generar todas las fechas del rango como strings en formato dd/MM/yyyy
            List<string> fechasOrdenadas = new List<string>();
            for (DateTime date = fechaDesde; date <= fechaHasta; date = date.AddDays(1))
            {
                fechasOrdenadas.Add(date.ToString("dd/MM/yyyy"));
            }

            // Generar el archivo Excel
            using (XLWorkbook wb = new XLWorkbook())
            using (MemoryStream stream = new MemoryStream())
            {
                var ws = wb.Worksheets.Add("Cierres de Caja");

                // =========================================
                // FORMATO DEL EXCEL
                // =========================================

                // Título del reporte
                ws.Cell(1, 1).Value = $"CIERRES DE CAJA {fechaDesde:MMMM yyyy}".ToUpper();
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Font.FontSize = 14;
                ws.Range(1, 1, 1, fechasOrdenadas.Count + 2).Merge();
                ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Subtítulo con rango de fechas
                ws.Cell(2, 1).Value = $"DESDE {fechaDesde:dd/MM/yyyy} HASTA {fechaHasta:dd/MM/yyyy}";
                ws.Cell(2, 1).Style.Font.Italic = true;
                ws.Range(2, 1, 2, fechasOrdenadas.Count + 2).Merge();
                ws.Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Encabezados de columnas (fila 3)
                int headerRow = 3;
                int col = 1;

                // Columna Banco
                ws.Cell(headerRow, col).Value = "BANCO";
                ws.Cell(headerRow, col).Style.DateFormat.Format = "dd/MM/yyyy";
               // ws.Cell(headerRow, col).DataType = XLDataType.Text;
                ws.Cell(headerRow, col).Style.DateFormat.SetFormat("[$-es-ES]dd/MM/yyyy");
                ws.Cell(headerRow, col).Style.Font.Bold = true;
                ws.Cell(headerRow, col).Style.Fill.BackgroundColor = XLColor.LightGray;
                ws.Cell(headerRow, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(headerRow, col).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(headerRow, col).Style.Border.BottomBorderColor = XLColor.Black;
                col++;

                // Columnas de fechas - USAR STRING
                foreach (var fecha in fechasOrdenadas)
                {
                    ws.Cell(headerRow, col).Value = $"{fecha:dd/MM/yyyy}";
 

                    // 👇 FORMATO DE FECHA
                    ws.Cell(headerRow, col).Style.Font.Bold = true;
                    ws.Cell(headerRow, col).Style.Fill.BackgroundColor = XLColor.LightGray;
                    ws.Cell(headerRow, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    col++;
                }

                // Columna Total por Banco
                ws.Cell(headerRow, col).Value = "TOTAL POR BANCO";
                ws.Cell(headerRow, col).Style.Font.Bold = true;
                ws.Cell(headerRow, col).Style.Fill.BackgroundColor = XLColor.LightGray;
                ws.Cell(headerRow, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(headerRow, col).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(headerRow, col).Style.Border.BottomBorderColor = XLColor.Black;

                // Formato de números para todas las columnas numéricas
                for (int c = 2; c <= fechasOrdenadas.Count + 2; c++)
                {
                    ws.Column(c).Style.DateFormat.Format = "dd/MM/yyyy";
                    ws.Cell(headerRow, col).Style.DateFormat.Format = "dd/MM/yyyy";
                   // ws.Cell(headerRow, col).DataType = XLDataType.Text;
                    ws.Cell(headerRow, col).Style.DateFormat.SetFormat("[$-es-ES]dd/MM/yyyy");
                    ws.Column(c).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                }

                // Columna de bancos alineada a la izquierda
                ws.Column(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                // Llenar datos
                int dataStartRow = headerRow + 1;
                int currentRow = dataStartRow;

                foreach (var reporte in response.Datos)
                {
                    col = 1;

                    // Banco
                    ws.Cell(currentRow, col).Value = reporte.Banco;
                    ws.Cell(currentRow, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    col++;

                    // Montos por fecha
                    foreach (var fecha in fechasOrdenadas)
                    {
                        decimal monto = reporte.DepositosPorFecha.ContainsKey(fecha) ? reporte.DepositosPorFecha[fecha] : 0;
                        ws.Cell(currentRow, col).Value = monto;
                        ws.Cell(currentRow, col).Style.NumberFormat.Format = "#,##0.00";
                        ws.Cell(currentRow, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        col++;
                    }

                    // Total por banco
                    ws.Cell(currentRow, col).Value = reporte.TotalPorBanco;
                    ws.Cell(currentRow, col).Style.NumberFormat.Format = "#,##0.00";
                    ws.Cell(currentRow, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                    // Destacar la fila TOTAL
                    if (reporte.Banco == "TOTAL")
                    {
                        ws.Row(currentRow).Style.Font.Bold = true;
                        ws.Row(currentRow).Style.Fill.BackgroundColor = XLColor.LightYellow;
                        ws.Row(currentRow).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        ws.Row(currentRow).Style.Border.TopBorderColor = XLColor.Black;
                    }

                    currentRow++;
                }

                // Agregar fila de TOTAL GENERAL si no existe en los datos
                bool existeTotal = response.Datos.Any(d => d.Banco == "TOTAL");

                if (!existeTotal && response.TotalGeneral > 0)
                {
                    col = 1;

                    // Columna Banco
                    ws.Cell(currentRow, col).Value = "TOTAL GENERAL";
                    ws.Cell(currentRow, col).Style.Font.Bold = true;
                    ws.Cell(currentRow, col).Style.Fill.BackgroundColor = XLColor.LightYellow;
                    ws.Cell(currentRow, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    col++;

                    // Fórmulas SUM para cada columna de fecha
                    foreach (var fecha in fechasOrdenadas)
                    {
                        ws.Cell(currentRow, col).FormulaA1 = $"=SUM({ws.Cell(dataStartRow, col).Address}:{ws.Cell(currentRow - 1, col).Address})";
                        ws.Cell(currentRow, col).Style.NumberFormat.Format = "#,##0.00";
                        ws.Cell(currentRow, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        ws.Cell(currentRow, col).Style.Font.Bold = true;
                        ws.Cell(currentRow, col).Style.Fill.BackgroundColor = XLColor.LightYellow;
                        col++;
                    }

                    // Total General
                    ws.Cell(currentRow, col).Value = response.TotalGeneral;
                    ws.Cell(currentRow, col).Style.NumberFormat.Format = "#,##0.00";
                    ws.Cell(currentRow, col).Style.Font.Bold = true;
                    ws.Cell(currentRow, col).Style.Fill.BackgroundColor = XLColor.LightYellow;
                    ws.Cell(currentRow, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    ws.Cell(currentRow, col).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                    // Aplicar borde superior a toda la fila
                    ws.Range(currentRow, 1, currentRow, fechasOrdenadas.Count + 2).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                }

                // Agregar borde exterior a toda la tabla
                int totalRows = currentRow - 1;
                var tablaCompleta = ws.Range(headerRow, 1, totalRows, fechasOrdenadas.Count + 2);
                tablaCompleta.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                tablaCompleta.Style.Border.OutsideBorderColor = XLColor.Black;

                // Ajustar ancho de columnas automáticamente
                ws.Columns().AdjustToContents();

                // Ajustar un ancho específico para las columnas de fechas
                for (int c = 2; c <= fechasOrdenadas.Count + 1; c++)
                {
                    ws.Column(c).Width = 12;
                }

                // Congelar el panel para mantener los encabezados visibles
                ws.SheetView.FreezeRows(headerRow);

                // Guardar el workbook
                wb.SaveAs(stream);

                // Preparar la respuesta HTTP
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(stream.ToArray())
                };

                result.Content.Headers.ContentLength = stream.Length;

                var contentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = $"cierres_caja_{fechaDesde:yyyyMMdd}_{fechaHasta:yyyyMMdd}.xlsx",
                    FileNameStar = $"cierres_caja_{fechaDesde:yyyyMMdd}_{fechaHasta:yyyyMMdd}.xlsx"
                };

                result.Content.Headers.ContentDisposition = contentDisposition;
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                return result;
            }
        }
    }
}
