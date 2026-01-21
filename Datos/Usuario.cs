
namespace API_Institucion.Entidades
{
    public class Usuario
    {
        public int id { get; set; }
        public required string dni { get; set; }
        public required string nombre { get; set; }
        public required string email { get; set; }
        public required string carrera { get; set; }
        public required string password { get; set; }
        public required string ruta_foto { get; set; }
        public required string rol { get; set; }
        public Usuario() { }
        public Usuario(string dni, string nombre, string email, string carrera, string password, string ruta_foto, string rol)
        {
            this.dni = dni;
            this.nombre = nombre;
            this.email = email;
            this.carrera = carrera;
            this.password = password;
            this.ruta_foto = ruta_foto;
            this.rol = rol;
        }
    }
}
