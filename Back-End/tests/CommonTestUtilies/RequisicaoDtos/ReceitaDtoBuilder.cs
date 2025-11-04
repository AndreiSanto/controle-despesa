using Bogus;
using controleDespesa.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilies.RequisicaoDtos
{
    public class ReceitaDtoBuilder
    {

        public static ReceitaDTO Build()
        {
            return new Faker<ReceitaDTO>("pt_BR")
            .RuleFor(d => d.Id, f => f.IndexFaker + 1)
            .RuleFor(d => d.Descricao, f => f.Commerce.ProductName())
            .RuleFor(d => d.Valor, f => f.Finance.Amount(10, 5000))
            .RuleFor(d => d.DataCadastro, f => f.Date.Recent(30))
            .RuleFor(d => d.ReceitaFixa, f => f.Random.Bool(0.3f))
            .RuleFor(d => d.TipoDespesaReceitaId, f => f.Random.Int(1, 5));


        }
    }
}
