using API_Institucion.Entidades;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;

namespace API_Institucion.Interfaces
{
    public class AlumnoInterfaz
    {
        public bool validado { get; set; }
        public string Token { get; set; }

        public AlumnoInterfaz(string token)
        {
            Token = token;
        }

        public bool Validacion(bool validado)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(Token);

            return true;
        }
    }
}
