using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PSMApiRest.DAL;

namespace PSMApiRest.Controllers
{
    [Authorize]
    [RoutePrefix("api/alumno")]
    public class AlumnoController : ApiController
    {
        AlumnoDAL alumnoDAL = new AlumnoDAL();
        /// <summary>
        /// </summary>
        /// <param name="Cedula"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/alumno/get
        [Route("get")]
        public IHttpActionResult GetAlumno(string Cedula)
        {
            try
            {
                return Ok(alumnoDAL.GetAlumno(Cedula));
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
