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
    [RoutePrefix("api/asientosContables")]
    public class AsientosContablesController : ApiController
    {
        readonly AsientosContablesDAL asientosContablesDAL = new AsientosContablesDAL();

        /// <summary>
        /// </summary>
        /// <param name="FechaDesde"></param>
        /// <param name="FechaHasta"></param>
        /// <param name="IdPeriodoContable"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/asientosContables/all
        [Route("all")]
        public IHttpActionResult GetAsientosContables([FromUri] string FechaDesde, string FechaHasta, int IdPeriodoContable)
        {
            try
            {
                return Ok(asientosContablesDAL.GetAsientosContables(FechaDesde, FechaHasta, IdPeriodoContable));
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="FechaDesde"></param>
        /// <param name="FechaHasta"></param>
        /// <param name="IdPeriodoContable"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/asientosContables/libromayor
        [Route("libromayor")]
        public IHttpActionResult GetAsientosContablesLibroMayor([FromUri] string FechaDesde, string FechaHasta, int IdPeriodoContable)
        {
            try
            {
                return Ok(asientosContablesDAL.GetAsientosContablesLibroMayor(FechaDesde, FechaHasta, IdPeriodoContable));
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        /// <summary>
        /// </summary>
        /// <param name="FechaDesde"></param>
        /// <param name="FechaHasta"></param>
        /// <param name="IdPeriodoContable"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/asientosContables/balance
        [Route("balance")]
        public IHttpActionResult GetAsientosContablesBalance([FromUri] string FechaDesde, string FechaHasta, int IdPeriodoContable)
        {
            try
            {
                return Ok(asientosContablesDAL.GetAsientosContablesBalance(FechaDesde, FechaHasta, IdPeriodoContable));
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        /// <summary>
        /// Establecemos la creacion de Asientos Contables
        /// </summary>
        /// <param name="asientoContablePayload"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // POST: api/asientosContables/add
        [Route("add")]
        public IHttpActionResult PostAsientosContables([FromBody] AsientoContablePayload asientoContablePayload)
        {
            if (asientoContablePayload.IdPeriodoContable != null)
            {
                try
                {
                    var result = asientosContablesDAL.CreateAsientosContables(asientoContablePayload);
                    if (result != null)
                    {
                        asientosContablesDAL.CreateAsientosContablesDetalle(asientoContablePayload.AsientoContableDetalle, result);
                    }
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            return Ok();
        }

        /// <summary>
        /// Establecemos la edicion de Asientos Contables
        /// </summary>
        /// <param name="asientoContableEditPayload"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // PUT: api/asientosContables/edit
        [Route("edit")]
        public IHttpActionResult PutAsientosContables([FromBody] AsientoContableEditPayload asientoContableEditPayload)
        {
            if (asientoContableEditPayload.IdAsiento != null)
            {
                try
                {
                    var result = asientosContablesDAL.PutAsientosContables(asientoContableEditPayload);
                    if (asientoContableEditPayload.AsientoContableDetalle?.Count > 1)
                    {
                        asientosContablesDAL.PutAsientosContablesDetalles(asientoContableEditPayload.AsientoContableDetalle);
                    }
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            return Ok();
        }

        /// <summary>
        /// Establecemos la edicion de Asientos Contables
        /// </summary>
        /// <param name="asientoContableEditPayload"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // PUT: api/asientosContables/activar
        [Route("activar")]
        public IHttpActionResult PutActivarAsientosContablesDetalle([FromBody] AsientoContableDetalleEditPayload asientoContableEditPayload)
        {
            if (asientoContableEditPayload.IdAsientoDetalle != null)
            {
                try
                {
                    var result = asientosContablesDAL.PutAsientosContablesDetallesActivoInactvo(asientoContableEditPayload);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            return Ok();
        }
    }
}