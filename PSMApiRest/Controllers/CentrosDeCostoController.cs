using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PSMApiRest.DAL;

namespace PSMApiRest.Controllers
{
    [Authorize]
    [RoutePrefix("api/centrosDeCosto")]
    public class CentroDeCostoController : ApiController
    {
        readonly CentrosDeCostoDAL centrosDeCostoDAL = new CentrosDeCostoDAL();
        /// <summary>
        /// Indicamos parametros para obtener lista de Centros De Costo
        /// </summary>
        /// <param name="Lapso"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/centrosDeCosto/get
        [Route("get")]
        public IHttpActionResult GetCentrosDeCosto()
        {
            try
            {
                return Ok(centrosDeCostoDAL.GetCentrosDeCosto().ToList());
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
