using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PSMApiRest.DAL;

namespace PSMApiRest.Controllers
{
    [Authorize]
    [RoutePrefix("api/monedas")]
    public class MonedasController : ApiController
    {
        readonly MonedasDAL monedasDAL = new MonedasDAL();
        /// <summary>
        /// Indicamos parametros para obtener lista de monedas
        /// </summary>
        /// <param name="Lapso"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/monedas/get
        [Route("get")]
        public IHttpActionResult GetMonedas()
        {
            try
            {
                return Ok(monedasDAL.GetMonedas().ToList());
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
