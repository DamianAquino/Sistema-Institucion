using API_Institucion.Datos;
using API_Institucion.Entidades;
using Microsoft.EntityFrameworkCore;

namespace API_Institucion.Persistencia
{
    public class Conexion_Db : DbContext
    {
        public DbSet<Usuario> usuarios { get; set; }

        public Conexion_Db(DbContextOptions<Conexion_Db> options) : base(options) { }
    }
}
