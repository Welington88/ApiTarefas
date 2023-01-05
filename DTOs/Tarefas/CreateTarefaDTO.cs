using System;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.DTOs.Tarefas
{
	public class CreateTarefaDTO
	{
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public EnumStatusTarefa Status { get; set; }
    }
}

