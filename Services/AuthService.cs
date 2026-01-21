using API_Institucion.Dtos;
using API_Institucion.Entidades;
using API_Institucion.Interfaces;
using API_Institucion.Persistencia;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API_Institucion.Services
{
    public class AuthService : IAuthService
    {
        private readonly Conexion_Db _dbContext;
        private readonly IConfiguration _variables_de_entorno;

        public AuthService(Conexion_Db dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _variables_de_entorno = config;
        }

        public AuthResultDto login(Login login)
        {
            var usuario = _dbContext.usuarios.SingleOrDefault(user => user.dni == login.Dni);

            if (usuario == null)
                return new AuthResultDto
                {
                    Estado = false,
                    Mensaje = "Usuario no encontrado."
                };

            if (BCrypt.Net.BCrypt.Verify(login.Password, usuario.password))
            {
                string token = GenerarToken(usuario);

                Logger.Info($"Login success usuario {login.Dni}");
                return new AuthResultDto
                {
                    Estado = true,
                    Mensaje = token
                };
            }
            else
            {
                Logger.Info($"Login fail usuario {login.Dni}");
                return new AuthResultDto
                {
                    Estado = false,
                    Mensaje = "Creadenciales incorrectas."
                };
            }
        }

        public async Task<AuthResultDto> RegistrarAsync(UsuarioDto dto)
        {
            if (dto.Foto == null || dto.Foto.Length == 0)
                return new AuthResultDto
                {
                    Estado = false,
                    Mensaje = "La foto es obligatoria."
                };

            if (_dbContext.usuarios.Any(usuario => usuario.dni == dto.Dni))
                return new AuthResultDto 
                { 
                    Estado = false, 
                    Mensaje = "El usuario ya estaba registrado." 
                };
            
            string nombre_foto = GenerarHash(dto.Dni + dto.Nombre + dto.Rol);
            var ruta_foto = Path.Combine("fotos", nombre_foto);

            using (var stream = new FileStream(ruta_foto, FileMode.Create))
            {
                await dto.Foto.CopyToAsync(stream);
            }

            string hash_password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            Usuario usuario = new Usuario
            {
                dni = dto.Dni,
                nombre = dto.Nombre,
                email = dto.Email,
                carrera = dto.Carrera,
                password = hash_password,
                ruta_foto = ruta_foto,
                rol = dto.Rol
            };
            _dbContext.usuarios.Add(usuario);
            _dbContext.SaveChanges();

            return new AuthResultDto
            {
                Estado = true,
                Mensaje = "Usuario creado satisfactoriamente."
            };
        }

        private string GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim("Dni", usuario.dni),
                new Claim("UsuarioType", usuario.rol)
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
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GenerarHash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = sha256.ComputeHash(bytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}
