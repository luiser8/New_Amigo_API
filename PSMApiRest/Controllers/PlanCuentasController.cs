using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PSMApiRest.DAL;
using PSMApiRest.Models;

namespace PSMApiRest.Controllers
{
    [Authorize]
    [RoutePrefix("api/planCuentas")]
    public class PlanCuentasController : ApiController
    {
        readonly PlanCuentasDAL planCuentasDAL = new PlanCuentasDAL();
        /// <summary>
        /// Indicamos parametros para obtener lista de plan de Cuentas
        /// </summary>
        /// <param name="Todos"></param>
        /// <param name="NombreFiltro"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/planCuentas/get
        [Route("get")]
        public IHttpActionResult GetPlanCuentas(byte Todos, string NombreFiltro)
        {
            try
            {
                return Ok(planCuentasDAL.GetPlanCuentas(Todos, NombreFiltro).ToList());
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Indicamos parametros para obtener lista de plan de Cuentas
        /// </summary>
        /// <param name="IdCuenta"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/planCuentas/byid
        [Route("byid")]
        public IHttpActionResult GetPlanCuentasById(int IdCuenta)
        {
            try
            {
                return Ok(planCuentasDAL.GetPlanCuentasById(IdCuenta).ToList());
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Establecemos la edicion de Asientos Contables
        /// </summary>
        /// <param name="planCuentas"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // PUT: api/planCuentas/edit
        [Route("edit")]
        public IHttpActionResult PutPlanCuentas([FromBody] PlanCuentas planCuentas)
        {
            if (planCuentas.IdCuenta != null)
            {
                try
                {
                    var result = planCuentasDAL.PutPlanCuentas(planCuentas);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            return Ok();
        }
    }
}
