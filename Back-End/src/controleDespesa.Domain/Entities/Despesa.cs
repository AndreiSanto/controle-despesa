using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Entities
{
    [Table("Despesa")]
    public class Despesa
    {
        public Despesa()
        {
            this.DespesaParcela = new List<DespesaParcela>();
        }

        public int Id { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public decimal ValorDespesa { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataVencimento { get; set; }

        public int NumeroDeParcela { get; set; }

        public bool Parcelado { get; set; }

        public bool DespesaFixa { get; set; } = false;


        public ICollection<DespesaParcela> DespesaParcela { get; set; }
       
        public int TipoDespesaReceitaId { get; set; }



        public TipoDespesaReceita TipoDespesaReceita { get; set; } = null!;




    }
}
