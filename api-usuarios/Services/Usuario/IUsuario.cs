using api_usuarios.Dto.Login;
using api_usuarios.Dto.Usuario;
using api_usuarios.Models;

namespace api_usuarios.Services.Usuario
{
    public interface IUsuario
    {
        Task<ResponseModel<UsuarioModel>> RegistrarUsuario(UsuarioCriacaoDto usuarioCriacaoDto);
        Task<ResponseModel<List<UsuarioModel>>> ListarUsuarios();
        Task<ResponseModel<UsuarioModel>> BuscarUsuarioPorId(int id);
        Task<ResponseModel<UsuarioModel>> EditarUsuario(UsuarioEdicaoDto usuarioEdicaoDto);
        Task<ResponseModel<UsuarioModel>> RemoverUsuario(int id);
        Task<ResponseModel<UsuarioModel>> Login(UsuarioLoginDto usuarioLoginDto);

    }
}
