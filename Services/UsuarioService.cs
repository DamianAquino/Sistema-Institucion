using API_Institucion.Datos;
using API_Institucion.Interfaces;
using API_Institucion.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace API_Institucion.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly Conexion_Db _dbContext;
        public UsuarioService(Conexion_Db dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserInformation>> ObtenerInformacion()
        {
            var information = await _dbContext
                .Set<UserInformation>()
                .FromSqlRaw("SELECT * FROM vw_usuarios_activos")
                .AsNoTracking()
                .ToListAsync();

            return information;
        }
    }
}
