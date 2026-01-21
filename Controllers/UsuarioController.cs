using API_Institucion.Interfaces;
using API_Institucion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Institucion.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;
        private readonly Logger _logger;
        public UsuarioController(IUsuarioService service, Logger logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("inicio")]
        public IActionResult obtenerInformacion()
        {
            string dni = User.FindFirst("Dni")!.Value;

            var data = _service.obtenerInformacion();

            return Ok();
        }
/*
                [Authorize(Roles = "Alumno")]
        [HttpPost("materias")]
        public IActionResult InscripcionMateria([FromBody] List<MateriaDto> materias, string token)
        {
            var dni = ObtenerDni(token);
            if (dni != null) 
                return Ok(new { materias });
            else
                return BadRequest(new { mensaje = "Dni Invalido" });

        }

        private string? ObtenerDni(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            // Leer el token sin validarlo
            var jwtToken = handler.ReadJwtToken(token);
            var dni = jwtToken.Claims.FirstOrDefault(c => c.Type == "UsuarioDni")?.Value;
            
            return dni;
        }*/
    }
}
