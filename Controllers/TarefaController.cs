using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.DTOs.Tarefas;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Service;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpGet("ObterTodos")]
        public async Task<ActionResult<IEnumerable<ReadTarefaDTO>>> ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF
            var result = await _tarefaService.ObterTodos();
            return Ok(result.OrderBy(t => t.Titulo));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadTarefaDTO>> ObterPorId(int id)
        {
            // TODO: Buscar o Id no banco utilizando o EF
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada
            if (id <= 0)
            {
                return BadRequest();
            }
            var result = await _tarefaService.ObterPorId(id);
            return Ok(result);
        }

        [HttpGet("ObterPorTitulo")]
        public async Task<ActionResult<ReadTarefaDTO>> ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var result = await _tarefaService.ObterPorTitulo(titulo);
            return Ok(result);
        }

        [HttpGet("ObterPorData")]
        public async Task<ActionResult<ReadTarefaDTO>> ObterPorData(DateTime data)
        {
            var tarefa = await _tarefaService.ObterPorData(data);
            return Ok(tarefa.Where(x => x.Data.Date == data.Date).OrderBy(x => x.Titulo));
        }

        [HttpGet("ObterPorStatus")]
        public async Task<ActionResult<ReadTarefaDTO>> ObterPorStatus(EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var tarefa = await _tarefaService.ObterPorStatus(status);
            return Ok(tarefa.Where(x => x.Status == status).OrderBy(x => x.Titulo));
        }

        [HttpPost]
        public async Task<ActionResult<Tarefa>> Criar(CreateTarefaDTO tarefa)
        {
            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
            await _tarefaService.Criar(tarefa);
            return Ok(tarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, UpdateTarefaDTO tarefa)
        {
            var result = _tarefaService.Atualizar(id,tarefa);
            return Ok(result.Result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var tarefa = await _tarefaService.Deletar(id);

            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            return NoContent();
        }
    }
}
