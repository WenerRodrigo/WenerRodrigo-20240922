using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rodonaves.Models;
using Rodonaves.Models.ViewModels;
using Rodonaves.Services;

namespace Rodonaves.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Colaborador")]
    public class ColaboradorController : ControllerBase
    {
        private readonly IColaboradorService _colaboradorService;

        public ColaboradorController(IColaboradorService colaboradorService)
        {
            _colaboradorService = colaboradorService;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> ListagemColaboradorAsync() 
        {
            var result = await _colaboradorService.ListagemColaboradoresAsync();

            return Ok(result);
        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> CadastroColaboradorAsync([FromBody] Colaboradores colaboradorCadastro) 
        {
            var colaborador = await _colaboradorService.CadastroColaboradorAsync(colaboradorCadastro);
            if (colaborador == null) 
            { 
                return NotFound("Unidade relacionada não encontrada ou está inativa."); 
            }
            return CreatedAtAction(nameof(ListagemColaboradorAsync), new { id = colaborador.Id }, "Colaborador cadastrado com sucesso.");
        }

        [HttpPut("atualizar/{id}")]
        public async Task<IActionResult> AtualizarColaboradorAsync([FromRoute] int id, AtualizarColaboradorViewModel colaboradorModel) 
        {
            var result = await _colaboradorService.AtualizarColaboradorAsync(id, colaboradorModel);
            if (result == null) 
            { 
                return NotFound("Colaborador não encontrado."); 
            }
            return Ok("Colaborador atualizado com sucesso.");
        }

        [HttpDelete("deletar/{id}")]
        public async Task<IActionResult> DeletarColaboradorAsync([FromRoute] int id) 
        {
            var result = await _colaboradorService.DeletarColaboradorAsync(id);
            if (result) 
            {
                return Ok("Colaborador deletado com sucesso.");
            }
            return NotFound("Colaborador não encontrado.");
        }
    }
}
