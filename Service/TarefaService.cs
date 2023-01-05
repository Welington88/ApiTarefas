using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.DTOs.Tarefas;
using TrilhaApiDesafio.Models;
using FluentResults;

namespace TrilhaApiDesafio.Service
{
	public class TarefaService : ITarefaService
    {
        private IMapper _mapper;
        private OrganizadorContext _context;

        public TarefaService(IMapper mapper, OrganizadorContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ReadTarefaDTO> ObterPorId(int id)
        {
            // TODO: Buscar o Id no banco utilizando o EF
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada
            if (id <= 0)
            {
                throw new Exception("Wrong get id");
            }
            var result = await _context.Tarefas.ToListAsync();
            var resultMap = result.Where<Tarefa>(t => t.Id == id).OrderBy(t => t.Titulo).FirstOrDefault();
            return _mapper.Map<ReadTarefaDTO>(resultMap);
        }

        public async Task<List<ReadTarefaDTO>> ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF
            var result = await _context.Tarefas.ToListAsync();
            var resultMap = result.OrderBy(t => t.Titulo).ToList();
            return _mapper.Map<List<ReadTarefaDTO>>(resultMap);
        }

        public async Task<List<ReadTarefaDTO>> ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            if (titulo is null)
            {
                throw new Exception("Wrong get id");
            }
            var result = await _context.Tarefas.ToListAsync();
            var resultMap = result.Where<Tarefa>(t => t.Titulo == titulo).OrderBy(t => t.Titulo).ToList();
            return _mapper.Map<List<ReadTarefaDTO>>(resultMap);
        }

        public async Task<List<ReadTarefaDTO>> ObterPorData(DateTime data)
        {
            if (data.ToString() is null)
            {
                throw new Exception("Wrong get id");
            }
            var result = await _context.Tarefas.ToListAsync();
            var resultMap = result.Where<Tarefa>(t => t.Data == data).OrderBy(t => t.Titulo).ToList();
            return _mapper.Map<List<ReadTarefaDTO>>(resultMap);
        }

        public async Task<List<ReadTarefaDTO>> ObterPorStatus(EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            if (status.ToString() is null)
            {
                throw new Exception("Wrong get id");
            }
            var result = await _context.Tarefas.ToListAsync();
            var resultMap = result.Where<Tarefa>(t => t.Status == status).OrderBy(t => t.Titulo).ToList();
            return _mapper.Map<List<ReadTarefaDTO>>(resultMap);
        }

        public async Task<ActionResult<ReadTarefaDTO>> Criar(CreateTarefaDTO tarefa)
        {
            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
            var tarefaData = _mapper.Map<Tarefa>(tarefa);
            var id = _context.Tarefas.Max(t => t.Id);
            tarefaData.Id = ++id;
            tarefaData.Data = DateTime.Now;
            _context.Add(tarefaData);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadTarefaDTO>(tarefaData);
        }

        public async Task<ReadTarefaDTO> Atualizar(int id, UpdateTarefaDTO tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                throw new Exception("Wrong get id");

            // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            var tarefas = await _context.Tarefas.ToListAsync();
            var tarefaUpdate = tarefas.Where<Tarefa>(t => t.Id == id).FirstOrDefault();
            if (!(tarefaUpdate is null))
            {
                tarefaUpdate.Titulo = tarefa.Titulo;
                tarefaUpdate.Descricao = tarefa.Descricao;
                tarefaUpdate.Data = DateTime.Now;
                tarefaUpdate.Status = tarefa.Status;
            }
            _context.Update(tarefaUpdate);
            var result = _context.SaveChangesAsync();
     
            return _mapper.Map<ReadTarefaDTO>(tarefaUpdate);
        }

        public async Task<Result> Deletar(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                throw new Exception("Wrong get id");
            }

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            return Result.Ok();
        }
    }
}

