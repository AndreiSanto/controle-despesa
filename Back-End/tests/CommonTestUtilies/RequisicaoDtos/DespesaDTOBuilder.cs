using Bogus;
using controleDespesa.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilies.RequisicaoDtos
{
    public  class DespesaDTOBuilder
    {


        public static DespesaDTO Build()
        {
            return new Faker<DespesaDTO>("pt_BR")
            .RuleFor(d => d.Id, f => f.IndexFaker + 1)
            .RuleFor(d => d.Descricao, f => f.Commerce.ProductName())
            .RuleFor(d => d.ValorDespesa, f => f.Finance.Amount(10, 2000))
            .RuleFor(d => d.DataCadastro, f => f.Date.Recent(30))
            .RuleFor(d => d.DataDespesa, f => f.Date.Past(1))
            .RuleFor(d => d.DespesaFixa, f => f.Random.Bool(0.3f))
            .RuleFor(d => d.TipoDespesaReceitaId, f => f.Random.Int(1, 5));


        }
    }
}
