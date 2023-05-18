using System;
using System.Net.Http;
using System.Web.Http;
using PSMApiRest.DAL;
using System.Net;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using System.Net.Http.Headers;
using System.Linq;

namespace PSMApiRest.Controllers
{
    [Authorize]
    [RoutePrefix("api/reporte")]
    public class ReporteController : ApiController
    {
        readonly ReporteDAL reporteDAL = new ReporteDAL();

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
            dt.Columns.AddRange(new DataColumn[10] { new DataColumn("Lapso", typeof(string)),
                                            new DataColumn("Identificador", typeof(Int32)),
                                            new DataColumn("FullNombres", typeof(string)),
                                            new DataColumn("Telefonos", typeof(string)),
                                            new DataColumn("Email", typeof(string)),
                                            new DataColumn("Descripcion", typeof(string)),
                                            new DataColumn("Cuota", typeof(string)),
                                            new DataColumn("Dolar", typeof(decimal)),
                                            new DataColumn("Monto", typeof(decimal)),
                                            new DataColumn("Total", typeof(decimal))
            });

            foreach (var reporte in reporteDAL.GetReporteDeudas(Lapso, Pagada))
            {
                dt.Rows.Add(reporte.Lapso, reporte.Identificador, reporte.Fullnombre, reporte.Telefonos, reporte.Email, reporte.Descripcion, reporte.Cuota, reporte.Dolar, reporte.Monto, reporte.Total);
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
        /// <param name="Lapso"></param>
        /// <param name="Pagada"></param>
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
    }
}
