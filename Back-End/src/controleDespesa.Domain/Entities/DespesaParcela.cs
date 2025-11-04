using controleDespesa.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Entities
{
    [Table("DespesaParcela")]
    public class DespesaParcela
    {
        

        public int Id { get; set; }

        public decimal Valor { get; set; }

        public DateTime DataVencimento { get; set; }

        public int NumeroDaParcela { get; set; }


        public SituacaoParcelaEnum SituacaoParcela { get; set; }

        public int DespesaId { get; set; }
        [JsonIgnore]
        public Despesa Despesa { get; set; } = null!;

        


    }
}
