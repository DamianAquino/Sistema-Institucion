using API_Institucion.Interfaces;
using API_Institucion.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace API_Institucion.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly Conexion_Db _contexto_db;
        private readonly IConfiguration _config;

        public UsuarioController(IConfiguration config, Conexion_Db contexto_db)
        {
            _contexto_db = contexto_db;
            _config = config;
        }

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
            var dni = jwtToken.Claims.FirstOrDefault(c => c.Type == "UsuarioDni")?.Value;;
            
            return dni;
        }
    }
}
