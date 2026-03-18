using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PSMApiRest.DAL;
using PSMApiRest.Models;

namespace PSMApiRest.Controllers
{
    [Authorize]
    [RoutePrefix("api/facturacion")]
    public class FacturacionController : ApiController
    {
        private readonly FacturaDAL facturaDAL = new FacturaDAL();
        /// <summary>
        /// </summary>
        /// <param name="Id_Factura"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/facturacion/get
        [Route("get")]
        public IHttpActionResult GetFacturacion(int Id_factura)
        {
            try
            {
                return Ok(facturaDAL.GetFacturaMontoYDepositos(Id_factura));
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// </summary>
        /// <param name="saldoAFavorDto"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // POST: api/facturacion/insert_saldo_favor
        [Route("insert_saldo_favor")]
        public IHttpActionResult InsertSaldoFavor(SaldoAFavorDto saldoAFavorDto)
        {
            try
            {
                return Ok(facturaDAL.InsertSaldoAFavor(saldoAFavorDto));
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// </summary>
        /// <param name="saldoAFavorDto"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // PUT: api/facturacion/update_saldo_favor
        [Route("update_saldo_favor")]
        public IHttpActionResult EditSaldoFavor(SaldoAFavorDto saldoAFavorDto)
        {
            try
            {
                return Ok(facturaDAL.UpdateSaldoAFavor(saldoAFavorDto));
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// </summary>
        /// <param name="depositoDto"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // POST: api/facturacion/insert_deposito
        [Route("insert_deposito")]
        public IHttpActionResult InsertDeposito(DepositoDto depositoDto)
        {
            try
            {
                return Ok(facturaDAL.InsertDeposito(depositoDto));
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// </summary>
        /// <param name="depositoDto"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // PUT: api/facturacion/update_deposito
        [Route("update_deposito")]
        public IHttpActionResult UpdateDeposito(DepositoDto depositoDto)
        {
            try
            {
                return Ok(facturaDAL.UpdateDeposito(depositoDto));
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}