using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Entities
{
    [Table("Despesa")]
    public class Despesa
    {
       

        public int Id { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public decimal ValorDespesa { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataDespesa { get; set; }

        

        public bool DespesaFixa { get; set; } = false;

       
       
        public int TipoDespesaReceitaId { get; set; }


        [JsonIgnore]
        public TipoDespesaReceita? TipoDespesaReceita { get; set; } = null!;




    }
}
