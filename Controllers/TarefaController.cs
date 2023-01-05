using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> ObterPorId(int id)
        {
            // TODO: Buscar o Id no banco utilizando o EF
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada
            if (id <= 0)
            {
                return BadRequest();
            }
            var result = await _context.Tarefas.ToListAsync();
            return Ok(result.Where<Tarefa>(t=> t.Id == id).OrderBy(t => t.Titulo));
        }

        [HttpGet("ObterTodos")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF
            var result = await _context.Tarefas.ToListAsync();
            return Ok(result.OrderBy(t => t.Titulo));
        }

        [HttpGet("ObterPorTitulo")]
        public async Task<ActionResult<Tarefa>> ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var result = await _context.Tarefas.ToListAsync();
            return Ok(result.Where<Tarefa>(t => t.Titulo == titulo).OrderBy(t => t.Titulo));
        }

        [HttpGet("ObterPorData")]
        public async Task<ActionResult<Tarefa>> ObterPorData(DateTime data)
        {
            var tarefa = await _context.Tarefas.ToListAsync();
            return Ok(tarefa.Where(x => x.Data.Date == data.Date).OrderBy(x => x.Titulo));
        }

        [HttpGet("ObterPorStatus")]
        public async Task<ActionResult<Tarefa>> ObterPorStatus(EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var tarefa = await _context.Tarefas.ToListAsync();
            return Ok(tarefa.Where(x => x.Status == status).OrderBy(x => x.Titulo));
        }

        [HttpPost]
        public async Task<ActionResult<Tarefa>> Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
            _context.Add(tarefa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            var tarefas = await _context.Tarefas.ToListAsync();
            var tarefaUpdate = tarefas.Where<Tarefa>(t => t.Id == id).FirstOrDefault();
            if (!(tarefaUpdate is null))
            {
                tarefaUpdate.Titulo = tarefa.Titulo;
                tarefaUpdate.Descricao = tarefa.Descricao;
                tarefaUpdate.Data = tarefa.Data;
                tarefaUpdate.Status = tarefa.Status;
            }
            _context.Update(tarefaUpdate);
            var result = _context.SaveChangesAsync();
            return Ok(tarefaUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            return NoContent();
        }
    }
}
