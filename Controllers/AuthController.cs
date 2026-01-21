using API_Institucion.Dtos;
using API_Institucion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Institucion.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService service)
        {
            _authService = service;
        }

        [HttpPost("login")]
        public IActionResult login([FromBody] Login login)
        {
            AuthResultDto result = _authService.login(login);

            if(result.Estado)
                return Ok(new {mensaje = result.Mensaje});
            else
                return BadRequest(new { mensaje = result.Mensaje });
        }

        [HttpPost("registrar")]
        public IActionResult Registrar([FromForm] UsuarioDto usuario_dto)
        {
            AuthResultDto result = _authService.RegistrarAsync(usuario_dto).Result;

            if (result.Estado)
                return Ok(new { mensaje = result.Mensaje });
            else
                return BadRequest(new { mensaje = result.Mensaje });
        }
    }
}
