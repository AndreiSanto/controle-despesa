using controleDespesa.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Application.DTOs
{
    public class TipoDespesaReceitaDTO
    {

        public int Id { get; set; }

        public string Nome { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }

        public TipoDespesaReceitaEnum TipoDespesaReceitaEnum { get; set; }
    }
}
