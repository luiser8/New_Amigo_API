using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PSMApiRest.DAL;

namespace PSMApiRest.Controllers
{
    [Authorize]
    [RoutePrefix("api/lapsos")]
    public class LapsosController : ApiController
    {
        LapsosDAL lapsosDAL = new LapsosDAL();

        /// <summary>
        /// </summary>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/lapsos/all
        [Route("all")]
        public IHttpActionResult GetLapsos()
        {
            try
            {
                return Ok(lapsosDAL.GetLapsos());
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
