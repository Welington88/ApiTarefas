using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrilhaApiDesafio.Models
{
    public class Tarefa
    {
        private DateTime data;

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public DateTime Data
        {
            get => data;
            set => data = (DateTime)(value.Equals(DateTime.MinValue) ? DateTime.Now : value);
        }

        public EnumStatusTarefa Status { get; set; }
    }
}