using API_Institucion.Entidades;

namespace API_Institucion.Datos
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public Rol(string nombre) 
        {
            this.Nombre = nombre;
        }

        public ICollection<Usuario> Usuarios { get; set; }
    }
}
