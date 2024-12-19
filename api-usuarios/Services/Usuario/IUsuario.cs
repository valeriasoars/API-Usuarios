using api_usuarios.Dto.Usuario;
using api_usuarios.Models;

namespace api_usuarios.Services.Usuario
{
    public interface IUsuario
    {
        Task<ResponseModel<UsuarioModel>> RegistrarUsuario(UsuarioCriacaoDto usuarioCriacaoDto);
    }
}
