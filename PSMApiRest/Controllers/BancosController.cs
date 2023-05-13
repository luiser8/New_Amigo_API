using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PSMApiRest.DAL;

namespace PSMApiRest.Controllers
{
    [Authorize]
    [RoutePrefix("api/bancos")]
    public class BancosController : ApiController
    {
        readonly BancosDAL bancosDAL = new BancosDAL();

        /// <summary>
        /// </summary>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/bancos/all
        [Route("all")]
        public IHttpActionResult GetBancos()
        {
            try
            {
                return Ok(bancosDAL.GetBancos());
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
