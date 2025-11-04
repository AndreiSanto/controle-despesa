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
    [Table("TipoDespesaReceita")]
    public class TipoDespesaReceita
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        public bool Ativo { get; set; } = true;

        [Column("tipodespesareceita")]
        public TipoDespesaReceitaEnum Tipo { get; set; }

        [JsonIgnore]
        public ICollection<Despesa> Despesas { get; set; }
        [JsonIgnore]
        public ICollection<Receita> Receitas { get; set; }

        public TipoDespesaReceita()
        {
            this.Despesas = new List<Despesa>();
            this.Receitas = new List<Receita>(); 
        }
    }
}
