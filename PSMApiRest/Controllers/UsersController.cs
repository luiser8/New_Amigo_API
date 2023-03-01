using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PSMApiRest.DAL;
using PSMApiRest.Lib;
using PSMApiRest.Models;

namespace PSMApiRest.Controllers
{
    [Authorize]
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        UsersDAL usersDAL = new UsersDAL();
        /// <summary>
        /// Establecemos inicio de sesion, verificando usuario y contrasena
        /// </summary>
        /// <param name="users"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // POST: api/users/login
        [Route("login")]
        public IHttpActionResult Login([FromBody] Users users)
        {
            if (users.Usuario != null && users.Contrasena != null)
            {
                try
                {
                    string ContrasenaMD5 = MD5.GetMD5(users.Contrasena);
                    var result = usersDAL.Login(users.Usuario, ContrasenaMD5).ToList();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            return Ok();
        }
        /// <summary>
        /// Obtenemos lista de Usuarios
        /// </summary>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // GET: api/users/get
        [Route("get")]
        public IHttpActionResult GetUsers()
        {
            try
            {
                return Ok(usersDAL.GetUsuarios().ToList());
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Establecemos la creacion de usuarios
        /// </summary>
        /// <param name="users"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // POST: api/users/add
        [Route("add")]
        public IHttpActionResult PostUsers([FromBody] Users users)
        {
            if (users.RolId != null)
            {
                try
                {
                    var result = usersDAL.CreateUsuarios(users).ToList();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            return Ok();
        }
        /// <summary>
        /// Establecemos la actualizacion de usuarios
        /// </summary>
        /// <param name="UsuarioId"></param>
        /// <param name="users"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // PUT: api/users/update
        [Route("update")]
        public IHttpActionResult PutUsers(int? UsuarioId,  Users users)
        {
            if (UsuarioId != null)
            {
                try
                {
                    return Ok(usersDAL.UpdateUsuarios(users).ToList());
                }
                catch (Exception ex)
                {
                    return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
        /// <summary>
        /// Establecemos la eliminacion de usuarios
        /// </summary>
        /// <param name="UsuarioId"></param>
        /// <returns> 
        ///     Retorna un objeto JSON
        /// </returns>
        /// <response code="200">Retorno del registro</response>
        /// <response code="400">Retorno de null si no hay registros</response> 
        // DELETE: api/users/delete
        [Route("delete")]
        public IHttpActionResult DeleteUsers(int? UsuarioId)
        {
            if (UsuarioId != null)
            {
                try
                {
                    return Ok(usersDAL.DelUsuarios((int)UsuarioId).ToList());
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
