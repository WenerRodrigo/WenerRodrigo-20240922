using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rodonaves.Models;
using Rodonaves.Models.ViewModels;
using Rodonaves.Services;

namespace Rodonaves.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("listar/{status}")]
        public async Task<IActionResult> ListagemUsuarioAsync([FromRoute] bool status) 
        {
            var result = await _usuarioService.ListagemUsuarioAsync(status);
            return Ok(result);        
        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> CadastroUsuarioAsync(Usuario usuario) 
        {
            if (usuario == null) 
            {
                return BadRequest("Nenhum dado foi informado. Verifique e tente novamente.");
            }
            var result = await _usuarioService.CadastroUsuarioAsync(usuario);
            if (result == null) 
            {
                return BadRequest("Erro ao cadastrar o usuário. Verifique os dados fornecidos.");
            }
            return Ok("Usuário cadastrado com sucesso.");
        }

        [HttpPut("atualizar{id}")]
        public async Task<IActionResult> AtualizarUsuarioAsync([FromRoute] int id, AtualizarUsuarioViewModel usuarioModel) 
        {
            var result = await _usuarioService.AtualizarUsuarioAsync(usuarioModel, id);
            if (result == null) 
            {
                return NotFound("Usuário não encontrado para atualização.");
            }
            return Ok("Usuário atualizado com sucesso.");
        }
    }
}
