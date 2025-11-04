using controleDespesa.Application.DTOs;
using controleDespesa.Exceptions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Application.Validation
{
    public class DespesaValidator : AbstractValidator<DespesaDTO>
    {
        public DespesaValidator()
        {

            RuleFor(despesa => despesa.Descricao).NotEmpty().WithMessage(MenssagesException.DESCRICAO_DESPESA_VAZIO);
            RuleFor(despesa => despesa.ValorDespesa).GreaterThan(0).WithMessage(MenssagesException.VALOR_DESPESA_INVALIDO);
          
            RuleFor(despesa => despesa.TipoDespesaReceitaId).GreaterThan(0).WithMessage(MenssagesException.TIPO_DESPESA_INVALIDO);
           


        }
    }
}
