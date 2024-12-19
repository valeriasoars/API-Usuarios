using api_usuarios.Dto.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_usuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        [HttpPost("register")]
        public IActionResult RegistrarUsuario(UsuarioCriacaoDto usuario)
        {
            return Ok(usuario);
        }
    }
}
