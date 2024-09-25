using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rodonaves.Models;
using Rodonaves.Models.ViewModels;
using Rodonaves.Services;

namespace Rodonaves.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Unidades")]
    public class UnidadeController : ControllerBase
    {
        private readonly IUnidadeService _unidadeService;

        public UnidadeController(IUnidadeService unidadeService)
        {
            _unidadeService = unidadeService;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> ListagemUnidadeAsync() 
        {
            var result = await _unidadeService.ListagemUnidadeAsync();
            return Ok(result);
        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> CadastroUnidadeAsync([FromBody] Unidades unidade) 
        {
            var result = await _unidadeService.CadastroUnidadeAsync(unidade);
            if (result == null) 
            {
                return BadRequest("Erro ao cadastrar a unidade. Verifique se todos os dados estão corretos.");
            }
            return Ok("Unidade cadastrada com sucesso.");
        }

        [HttpPut("atualizar/{id}")]
        public async Task<IActionResult> AtualizarUnidadeAsync([FromRoute] int id, AtualizarUnidadeViewModel unidadeModel) 
        {
            var result = await _unidadeService.AtualizarUnidadeAsync(unidadeModel, id);
            if (result == null) 
            {
                return NotFound("Unidade não encontrada para atualização.");
            }

            return Ok("Unidade atualizada com sucesso.");
        }
    }
}
