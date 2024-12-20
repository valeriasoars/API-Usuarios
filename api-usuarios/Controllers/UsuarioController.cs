﻿using api_usuarios.Dto.Usuario;
using api_usuarios.Services.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_usuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuarioInterface;
        public UsuarioController(IUsuario usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        [HttpGet]

        public async Task<IActionResult> ListarUsuarios()
        {
            var usuarios = await _usuarioInterface.ListarUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> BuscarUsuarioPorId(int id)
        {
            var usuario = await _usuarioInterface.BuscarUsuarioPorId(id);
            return Ok(usuario);
        }

        [HttpPut]
        public async Task<IActionResult> EditarUsuario(UsuarioEdicaoDto usuarioEdicaoDto)
        {
            var usuario = await _usuarioInterface.EditarUsuario(usuarioEdicaoDto);
            return Ok(usuario);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoverUsuario(int id)
        {
            var usuario = await _usuarioInterface.RemoverUsuario(id);
            return Ok(usuario);
        }

    }
}
