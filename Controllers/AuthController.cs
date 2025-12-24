using API_Institucion.Dtos;
using API_Institucion.Entidades;
using API_Institucion.Interfaces;
using API_Institucion.Persistencia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Institucion.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Cambia el nombre del campo privado para evitar ambigüedad
        private readonly Conexion_Db _contextoDb;
        // Me permite acceder a appsettings.json
        private readonly IConfiguration _variables_de_entorno;

        public AuthController(IConfiguration config, Conexion_Db contextoDb)
        {
            _contextoDb = contextoDb;
            _variables_de_entorno = config;
        }

        [HttpPost("login")]
        public IActionResult login([FromBody] Login login)
        {
            var usuario = _contextoDb.usuarios.SingleOrDefault(user => user.dni == login.Dni);

            if (usuario == null)
                return NotFound(new { mensaje = "Usuario no encontrado." });

            if (BCrypt.Net.BCrypt.Verify(login.Password, usuario.password))
            {
                string token = GenerarToken(usuario);
                return Ok(new { token });
            }
            else
                return Unauthorized(new { mensaje = "Creadenciales incorrectas." });
        }

        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] UsuarioDto usuario_dto)
        {
            if (_contextoDb.usuarios.Any(usuario => usuario.dni == usuario_dto.Dni))
                return BadRequest(new { mensaje = "El Dni ya esta registrado." });

            string hash_password = BCrypt.Net.BCrypt.HashPassword(usuario_dto.Password);

            Usuario usuario = new Usuario
            {
                dni = usuario_dto.Dni,
                nombre = usuario_dto.Nombre,
                email = usuario_dto.Email,
                departamento = usuario_dto.Departamento,
                password = hash_password,
            };
            _contextoDb.usuarios.Add(usuario);
            _contextoDb.SaveChanges();

            return Ok(new { mensaje = "Usuario creado correctamente." });
        }

        private string GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                // Claim estandar, dentificador principal del usuario
                new Claim("Dni", usuario.dni),
                new Claim("Rol", usuario.rolId.ToString()),
                // Identificador del token
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Convertir password a bytes para firmar y validar el token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_variables_de_entorno["Jwt:Key"]));
            // Definir algoritmo de cifrado del token (HmacSha256)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crear el token
            var token = new JwtSecurityToken(
                issuer: _variables_de_entorno["Jwt:Issuer"],
                audience: _variables_de_entorno["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddYears(100),
                signingCredentials: creds
            );

            // Serializar a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
