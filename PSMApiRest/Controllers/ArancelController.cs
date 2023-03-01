using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PSMApiRest.DAL;

namespace PSMApiRest.Controllers
{
    [Authorize]
    [RoutePrefix("api/arancel")]
    public class ArancelController : ApiController
    {
        ArancelDAL arancelDAL = new ArancelDAL();
        /// <summary>
        /// Indicamos parametros para obtener lista de Aranceles por lapso
        /// </summary>
        /// <param name="Lapso"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/arancel/get
        [Route("get")]
        public IHttpActionResult GetArancel([FromUri] string Lapso, int TipoArancel)
        {
            if (Lapso != null)
            {
                try
                {
                    return Ok(arancelDAL.GetArancel(Lapso, TipoArancel).ToList());
                }
                catch (Exception ex)
                {
                    return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
