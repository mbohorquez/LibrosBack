using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservasCore6.Models.Request;
using ReservasCore6.Models.Response;
using ReservasCore6.Services;
using System.ComponentModel.DataAnnotations;

namespace ReservasCore6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        /// <summary>
        /// Metodo para iniciar la sesion
        /// </summary>
        /// <param name="model">modelo usuario y contraseña</param>
        /// <returns>autorizacion</returns>
        [HttpPost("login")]
        public IActionResult Autentificar([FromBody] AuthRequest model)
        {
            Respuesta respuesta = new Respuesta();
            var userResponse = _usuarioService.Auth(model);
            if (userResponse == null)
            {
                respuesta.Mensaje = "No autorizado";
                return Unauthorized(respuesta);
            }
            respuesta.Mensaje = "Autorizado";
            respuesta.Data = userResponse;
            return Ok (respuesta);
        }
    }
}
