using api_usuarios.Data;
using api_usuarios.Dto.Login;
using api_usuarios.Dto.Usuario;
using api_usuarios.Models;
using api_usuarios.Services.Senha;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ResponseModel<UsuarioModel>> BuscarUsuarioPorId(int id)
        {
            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);
                if(usuario == null)
                {
                    response.Mensagem = "Usuário não localizado!";
                    return response;
                }

                response.Dados = usuario;
                response.Mensagem = "Usuário localizado!";
                return response;

            }
            catch (Exception ex) 
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<UsuarioModel>> EditarUsuario(UsuarioEdicaoDto usuarioEdicaoDto)
        {
            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try
            {
                var usuarioBanco = await _context.Usuarios.FindAsync(usuarioEdicaoDto.Id);

                if(usuarioBanco == null)
                {
                    response.Mensagem = "Usuário não localizado!";
                    return response;
                }

                usuarioBanco.Nome = usuarioEdicaoDto.Nome;
                usuarioBanco.Sobrenome = usuarioEdicaoDto.Sobrenome;
                usuarioBanco.Email = usuarioEdicaoDto.Email;
                usuarioBanco.Usuario = usuarioEdicaoDto.Usuario;
                usuarioBanco.DataAlteracao = DateTime.Now;

                _context.Update(usuarioBanco);
                await _context.SaveChangesAsync();
                response.Mensagem = "Usuário editado com sucesso!";
                response.Dados = usuarioBanco;
                return response;

            }
            catch(Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }

        }

        public async Task<ResponseModel<List<UsuarioModel>>> ListarUsuarios()
        {
            ResponseModel<List<UsuarioModel>> response = new ResponseModel<List<UsuarioModel>>();

            try
            {
                var usuarios = await _context.Usuarios.ToListAsync();
                response.Dados = usuarios;
                response.Mensagem = "Usuários localizados!";
                return response;

            }
            catch (Exception ex) 
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<UsuarioModel>> Login(UsuarioLoginDto usuarioLoginDto)
        {
            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(userBanco => userBanco.Email == usuarioLoginDto.Email);

                if(usuario == null)
                {
                    response.Mensagem = "Usuário não localizado!";
                    response.Status = false;
                    return response;
                }

                if(_senhaInterface.VerficaSenhaHash(usuarioLoginDto.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                {
                    response.Mensagem = "Credenciais inválidas!";
                    response.Status = false;
                    return response;
                }

                var token = _senhaInterface.CriarToken(usuario);
                usuario.Token = token;

                _context.Update(usuario);
                await _context.SaveChangesAsync();
                response.Dados = usuario;
                response.Mensagem = "Usuário logado com sucesso!";
                return response;
 
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }

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

        public async Task<ResponseModel<UsuarioModel>> RemoverUsuario(int id)
        {
            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);

                if (usuario == null)
                {
                    response.Mensagem = "Usuário não localizado!";
                    return response;
                }

                _context.Remove(usuario);
                response.Dados = usuario;
                response.Mensagem = "Usuário deletado com sucesso!";
                await _context.SaveChangesAsync();
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
