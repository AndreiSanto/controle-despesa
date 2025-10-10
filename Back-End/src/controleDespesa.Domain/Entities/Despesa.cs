using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Entities
{
    public class Despesa
    {
        public int Id { get; set; }

        public string Descrição { get; set; } = string.Empty;

        public decimal ValorDespesa { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataVencimento { get; set; }

        public int? NumeroParcela { get; set; }

        public bool DespesaFixa { get; set; } = false;

        public int TipoDespesaReceitaId { get; set; }

        public TipoDespesaReceita TipoDespesaReceita { get; set; } = null!;




    }
}
