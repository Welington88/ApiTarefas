using System;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.DTOs.Tarefas;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Service
{
	public interface ITarefaService
    {
        Task<ReadTarefaDTO> ObterPorId(int id);
        Task<List<ReadTarefaDTO>> ObterTodos();
        Task<List<ReadTarefaDTO>> ObterPorTitulo(string titulo);
        Task<List<ReadTarefaDTO>> ObterPorData(DateTime data);
        Task<List<ReadTarefaDTO>> ObterPorStatus(EnumStatusTarefa status);
        Task<ActionResult<ReadTarefaDTO>> Criar(CreateTarefaDTO tarefa);
        Task<ReadTarefaDTO> Atualizar(int id, UpdateTarefaDTO tarefa);
        Task<Result> Deletar(int id);
    }
}