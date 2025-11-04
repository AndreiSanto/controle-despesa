using controleDespesa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace controleDespesa.Application.DTOs
{
    public class DespesaDTO
    {

        public int Id { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public decimal ValorDespesa { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataDespesa { get; set; }

      

        public bool DespesaFixa { get; set; } = false;

        public int TipoDespesaReceitaId { get; set; }
        [JsonIgnore]
        public TipoDespesaReceitaDTO? TipoDespesaReceita { get; set; }





    }
}
