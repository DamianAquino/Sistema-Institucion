using API_Institucion.Dtos;

namespace API_Institucion.Interfaces
{
    public interface IAuthService
    {
        public AuthResultDto login(Login login);
        Task<AuthResultDto> RegistrarAsync(UsuarioDto dto);
    }
}
