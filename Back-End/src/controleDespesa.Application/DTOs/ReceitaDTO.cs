using controleDespesa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Application.DTOs
{
    public class ReceitaDTO
    {

        public int Id { get; set; }

        public decimal Valor { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public bool ReceitaFixa { get; set; } = false;

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public int UsuarioId { get; set; }


        public int TipoDespesaReceitaId { get; set; }


    }
}
