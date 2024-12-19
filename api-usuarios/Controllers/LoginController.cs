using api_usuarios.Dto.Usuario;
using api_usuarios.Services.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_usuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuario _usuarioInterface;
        public LoginController(IUsuario usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegistrarUsuario(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            var usuario = await _usuarioInterface.RegistrarUsuario(usuarioCriacaoDto);
            return Ok(usuario);
        }
    }
}
