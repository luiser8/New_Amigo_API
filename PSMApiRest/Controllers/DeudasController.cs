using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PSMApiRest.DAL;
using PSMApiRest.Lib;
using PSMApiRest.Models;

namespace PSMApiRest.Controllers
{
    [Authorize]
    [RoutePrefix("api/deudas")]
    public class DeudasController : ApiController
    {
        readonly DeudaDAL deudaDAL = new DeudaDAL();
        readonly InscripcionesDAL inscripcionesDAL = new InscripcionesDAL();
        readonly TercerosDAL tercerosDAL = new TercerosDAL();
        readonly AlumnoDAL alumnoDAL = new AlumnoDAL();
        /// <summary>
        /// Indicamos parametros para obtener deuda
        /// </summary>
        /// <param name="deudaPayload"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // POST: api/deudas/check
        [Route("check")]
        public IHttpActionResult Check([FromBody] DeudaPayload deudaPayload)
        {
            if (deudaPayload.Lapso != null && deudaPayload.Identificador != null)
            {
                try
                {
                    var respuesta = deudaDAL.GetDeuda(deudaPayload.Puerta, deudaPayload.Lapso, deudaPayload.Identificador);
                    var respuestaTipo = inscripcionesDAL.GetIdInscripcion(deudaPayload.Lapso, deudaPayload.Identificador).ToList();
                    string planDePago = respuestaTipo.Count >= 1 ? respuestaTipo.FirstOrDefault().PlanDePago : "No encontrado";
                    bool esBecado = inscripcionesDAL.GetIdInscripcion(deudaPayload.Lapso, deudaPayload.Identificador).Where(x => x.PlanDePago.Contains("BECA")).Count() >= 1;
                    bool existe = tercerosDAL.GetTercero(deudaPayload.Identificador);
                    string estadoAcademico = alumnoDAL.GetAlumnoEstAca(deudaPayload.Identificador);
                    string lapsoIngreso = alumnoDAL.GetAlumnoLapIng(deudaPayload.Identificador);

                    respuesta.PlanDePago = planDePago;
                    respuesta.Existe = existe;

                    if (esBecado && respuestaTipo.Count >= 1)
                    {
                        respuesta.NoPasa = false;
                        respuesta.EsBecado = true;
                    }

                    if (respuesta.Deudas.Count == 0 && !esBecado && respuestaTipo.Count >= 1)
                    {
                        respuesta.PagoTodo = true;
                    }
                    if (!esBecado && respuestaTipo.Count <= 0 && respuesta.Deudas.Count == 0 && existe && estadoAcademico != "Egresado")
                    {
                        respuesta.NoPasa = true;
                        respuesta.EsDesertor = true;
                    }
                    if (estadoAcademico == "Egresado")
                    {
                        respuesta.EsEgresado = true;
                        respuesta.NoPasa = false;
                    }
                    if (estadoAcademico == "Amonestado" || estadoAcademico == "Suspensión Académica")
                    {
                        respuesta.EsAmonestado = true;
                        respuesta.NoPasa = true;
                    }
                    if (!existe)
                    {
                        respuesta.NoPasa = true;
                    }

                    for (int i = 0; i < respuesta.Deudas.Count; i++)
                    {
                        DateTime today = DateTime.Now;
                        int compare = DateTime.Compare(today, respuesta.Deudas[i].FechaVencimiento);
                        if (compare >= 1)
                        {
                            respuesta.NoPasa = true;
                            respuesta.EsBecado = false;
                        }
                    }

                    if (DateTime.TryParse(lapsoIngreso, out DateTime fechaIngreso))
                    {
                        if (fechaIngreso >= new DateTime(2022, 1, 1))
                        {
                            respuesta.SinDocumentos = alumnoDAL.GetAlumnoDocumentosPorConsignar(deudaPayload.Identificador);
                        }
                        else
                        {
                            respuesta.SinDocumentos = false;
                        }
                    }
                    return Ok(respuesta);
                }
                catch (Exception ex)
                {
                    return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            return StatusCode(HttpStatusCode.NotFound);
        }
        /// <summary>
        /// Indicamos parametros para obtener deuda
        /// </summary>
        /// <param name="pagada"></param>
        /// <param name="id_inscripcion"></param>
        /// <param name="id_arancel"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // DELETE: api/deudas/delete
        [Route("delete")]
        public IHttpActionResult DeleteDeuda([FromUri] int? pagada, int? id_inscripcion, int? id_arancel)
        {
            if (id_inscripcion != null && id_arancel != null)
            {
                try
                {
                    return Ok(deudaDAL.DeleteDeuda((int)pagada, (int)id_inscripcion, (int)id_arancel).ToList());             
                }
                catch (Exception ex)
                {
                    return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
        /// <summary>
        /// Indicamos parametros para obtener deudas insertadas y eliminarlas
        /// </summary>
        /// <param name="cuotasResetPayload"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // DELETE: api/deudas/reset
        [Route("reset")]
        public IHttpActionResult ResetDeudasInserts(CuotasResetPayload cuotasResetPayload)
        {
            if (cuotasResetPayload.Lapso != null)
            {
                CuotaDAL cuotaDAL = new CuotaDAL();
                try
                {
                    var idInscripciones = cuotaDAL.GetResetCuotasInserts(cuotasResetPayload).ToList();
                    foreach (var item in idInscripciones)
                    {
                        deudaDAL.DeleteDeuda(item.Pagada, item.IdInscripcion, item.IdArancel).ToList();
                    };
                    return Ok();
                }
                catch (Exception ex)
                {
                    return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
        /// <summary>
        /// Indicamos parametros para obtener deuda
        /// </summary>
        /// <param name="id_cuenta"></param>
        /// <param name="deuda"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // PUT: api/deudas/update
        [Route("update")]
        public IHttpActionResult PutDeuda(int? id_cuenta, Deuda deuda)
        {
            if (id_cuenta != null)
            {
                try
                {
                    return Ok(deudaDAL.EditDeuda((int)id_cuenta, deuda.Pagada, deuda.Monto, deuda.MontoFacturas).ToList());
                }
                catch (Exception ex)
                {
                    return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
        /// <summary>
        /// Indicamos parametros para obtener deuda
        /// </summary>
        /// <param name="deuda"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // POST: api/deudas/insert
        [Route("insert")]
        public IHttpActionResult InsertDeuda([FromBody] Deuda deuda)
        {
            if (deuda.FechaVencimiento != null)
            {
                try
                {
                    return Ok(deudaDAL.InsertDeuda(deuda.Id_Inscripcion, deuda.Id_Arancel, Calculo.TotalMonto(deuda.MontoFacturas, deuda.Monto /*101712546.56M*/), deuda.FechaVencimiento).ToList());
                }
                catch (Exception ex)
                {
                    return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = deuda.Id_Inscripcion }, deuda);
        }
    }
}
