namespace api_usuarios.Services.Senha
{
    public interface ISenha
    {
        void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
    }
}
