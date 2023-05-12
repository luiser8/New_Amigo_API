using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PSMApiRest.DAL;

namespace PSMApiRest.Controllers
{
    [Authorize]
    [RoutePrefix("api/conciliaciones")]
    public class ConciliacionesController : ApiController
    {
        readonly ConciliacionesDAL conciliacionesDAL = new ConciliacionesDAL();

        /// <summary>
        /// </summary>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/conciliaciones/all
        [Route("all")]
        public IHttpActionResult GetConciliaciones([FromUri] int Todos, string FechaDesde, string FechaHasta)
        {
            try
            {
                return Ok(conciliacionesDAL.GetConciliaciones(Todos, FechaDesde, FechaHasta));
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}