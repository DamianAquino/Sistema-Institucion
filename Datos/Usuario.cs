using API_Institucion.Datos;
using System.ComponentModel.DataAnnotations;

namespace API_Institucion.Entidades
{
    public class Usuario
    {
        public int id { get; set; }
        public int rolId {  get; set; }
        public required string dni { get; set; }
        public required string nombre { get; set; }
        public required string email { get; set; }
        public required string departamento { get; set; }
        public required string password { get; set; }
        public Usuario() { }
        public Usuario(string dni, string nombre, string email, string departamento, string password, int rolId)
        {
            this.dni = dni;
            this.nombre = nombre;
            this.email = email;
            this.departamento = departamento;
            this.password = password;
        }
    }
}
