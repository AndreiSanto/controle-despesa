using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Entities
{
    public class TipoDespesaReceita
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        public bool Ativo { get; set; } = true;

        public ICollection<Despesa> Despesas { get; set; }
        public ICollection<Receita> Receitas { get; set; }

        public TipoDespesaReceita()
        {
            this.Despesas = new List<Despesa>();
            this.Receitas = new List<Receita>(); 
        }
    }
}
