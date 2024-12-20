using api_usuarios.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace api_usuarios.Services.Senha
{
    public class SenhaService : ISenha
    {

        private readonly IConfiguration _config;
        public SenhaService(IConfiguration config) 
        { 
            _config = config;
        }

        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512()) 
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }



        public bool VerficaSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512(senhaSalt)) 
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return computeHash.SequenceEqual(senhaSalt);
            }
        }

        public string CriarToken(UsuarioModel usuario)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("Email", usuario.Email),
                new Claim("Username", usuario.Usuario)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
               );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
