namespace API_Institucion.Interfaces
{
    public class UsuarioDto
    {
        public string Rol { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Carrera { get; set; }
        public string Password { get; set; }
        public IFormFile Foto { get; set; }
    }
}
