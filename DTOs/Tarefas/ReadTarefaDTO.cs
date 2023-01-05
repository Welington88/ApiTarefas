using System;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.DTOs.Tarefas
{
	public class ReadTarefaDTO
	{
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public EnumStatusTarefa Status { get; set; }
    }
}

