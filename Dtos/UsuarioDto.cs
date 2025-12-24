namespace API_Institucion.Interfaces
{
    public class UsuarioDto
    {
        public int Rol { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Departamento { get; set; }
        public string Password { get; set; }
    }
}
