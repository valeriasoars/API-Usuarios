using api_usuarios.Models;

namespace api_usuarios.Services.Senha
{
    public interface ISenha
    {
        void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
        bool VerficaSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt);
        string CriarToken(UsuarioModel usuario);
    }
}
