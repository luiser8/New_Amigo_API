using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PSMApiRest.DAL;
using PSMApiRest.Models;

namespace PSMApiRest.Controllers
{
    [Authorize]
    [RoutePrefix("api/inscripcion")]
    public class InscripcionController : ApiController
    {
        InscripcionesDAL inscripcionesDAL = new InscripcionesDAL();
        /// <summary>
        /// </summary>
        /// <param name="Identificador"></param>
        /// <param name="Lapso"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/inscripcion/get
        [Route("get")]
        public IHttpActionResult GetInscripcion(string Identificador, string Lapso)
        {
            try
            {
                FacturaDAL facturaDAL = new FacturaDAL();
                var inscripcion = inscripcionesDAL.GetIdInscripcion(Lapso, Identificador).ToArray();
                foreach (var item in inscripcion)
                {
                    item.Factura = facturaDAL.GetFactura(item.Id_Inscripcion).ToArray();
                }
                return Ok(inscripcion);
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Indicamos parametros para obtener deuda
        /// </summary>
        /// <param name="id_inscripcion"></param>
        /// <param name="inscripciones"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // PUT: api/inscripcion/update
        [Route("update")]
        public IHttpActionResult PutInscripcion(int? id_inscripcion, Inscripciones inscripciones)
        {
            if (id_inscripcion != null)
            {
                try
                {
                    return Ok(inscripcionesDAL.UpdateInscripcion((int)id_inscripcion, inscripciones.Id_Plan, inscripciones.Id_TipoIngreso));
                }
                catch (Exception ex)
                {
                    return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
        /// <summary>
        /// Indicamos parametros para eliminar inscripcion
        /// </summary>
        /// <param name="id_inscripcion"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // DELETE: api/inscripcion/delete
        [Route("delete")]
        public IHttpActionResult DeleteInscripcion([FromUri] int? id_inscripcion)
        {
            if (id_inscripcion != null)
            {
                try
                {
                    return Ok(inscripcionesDAL.DeleteInscripcion((int)id_inscripcion));
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
