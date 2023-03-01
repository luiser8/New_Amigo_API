using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PSMApiRest.DAL;

namespace PSMApiRest.Controllers
{
    [Authorize]
    [RoutePrefix("api/planes")]
    public class PlanesController : ApiController
    {
        PlanesDAL planesDAL = new PlanesDAL();
        /// <summary>
        /// Indicamos parametros para obtener lista de Planes por lapso
        /// </summary>
        /// <param name="Lapso"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/planes/get
        [Route("get")]
        public IHttpActionResult GetPlanes([FromUri] string Lapso, byte Tipo)
        {
            if (Lapso != null)
            {
                try
                {
                    return Ok(planesDAL.GetPlanes(Lapso, Tipo).ToList());
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
