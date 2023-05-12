using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PSMApiRest.DAL;

namespace PSMApiRest.Controllers
{
    [Authorize]
    [RoutePrefix("api/periodosContables")]
    public class PeriodosContablesController : ApiController
    {
        readonly PeriodosContablesDAL periodosContablesDAL = new PeriodosContablesDAL();

        /// <summary>
        /// </summary>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/periodosContables/all
        [Route("all")]
        public IHttpActionResult GetPeriodosContables([FromUri] int Todos, string FechaDesde, string FechaHasta)
        {
            try
            {
                return Ok(periodosContablesDAL.GetPeriodosContables(Todos, FechaDesde, FechaHasta));
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
