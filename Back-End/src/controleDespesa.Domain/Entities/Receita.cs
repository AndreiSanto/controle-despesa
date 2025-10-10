using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Entities
{
    public class Receita
    {
        public int Id { get; set; }
        
        public decimal Valor { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public bool ReceitaFixa { get; set; } = false;

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        public int TipoDespesaReceitaId { get; set; }

        public TipoDespesaReceita TipoDespesaReceita { get; set; } = null!;




    }
}
