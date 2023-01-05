using System;
using AutoMapper;
using TrilhaApiDesafio.DTOs.Tarefas;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Profiles
{
	public class TarefasProfile : Profile
	{
		public TarefasProfile()
		{
            CreateMap<CreateTarefaDTO, Tarefa>();
            CreateMap<Tarefa, ReadTarefaDTO>();
            CreateMap<UpdateTarefaDTO, Tarefa>();
        }
	}
}

