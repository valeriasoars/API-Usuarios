﻿using api_usuarios.Data;
using api_usuarios.Dto.Usuario;
using api_usuarios.Models;
using api_usuarios.Services.Senha;

namespace api_usuarios.Services.Usuario
{
    public class UsuarioService : IUsuario
    {
        private readonly AppDbContext _context;
        private readonly ISenha _senhaInterface;
        public UsuarioService(AppDbContext context, ISenha senhaInterface)
        {
            _context = context;
            _senhaInterface = senhaInterface;
        }

        public async Task<ResponseModel<UsuarioModel>> RegistrarUsuario(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try
            {
                if (VerificaSeExisteEmailUsuarioRepetidos(usuarioCriacaoDto))
                {
                    response.Mensagem = "Email/Usuário já cadastrado!";
                    response.Status = false;
                    return response;
                }

                _senhaInterface.CriarSenhaHash(usuarioCriacaoDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                UsuarioModel usuario = new UsuarioModel()
                {
                    Usuario = usuarioCriacaoDto.Usuario,
                    Email = usuarioCriacaoDto.Email,
                    Nome = usuarioCriacaoDto.Nome,
                    Sobrenome = usuarioCriacaoDto.Sobrenome,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt
                };

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                response.Mensagem = "Usuário cadastrado com sucesso!";

                return response;

               

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }

        }

        private bool VerificaSeExisteEmailUsuarioRepetidos(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            var usuario = _context.Usuarios.FirstOrDefault(item => item.Email == usuarioCriacaoDto.Email ||
                                                            item.Usuario == usuarioCriacaoDto.Usuario);

            if (usuario == null) 
            { 
                return false;
            }

            return true;
        }
    }
}
