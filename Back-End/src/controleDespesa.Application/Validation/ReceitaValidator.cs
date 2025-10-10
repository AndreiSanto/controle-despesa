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
    public class ReceitaValidator : AbstractValidator<ReceitaDTO>
    {
        public ReceitaValidator()
        {

            RuleFor(receita => receita.Descricao).NotEmpty().WithMessage(MenssagesException.DESCRICAO_RECEITA_VAZIO);
            RuleFor(receita => receita.TipoDespesaReceitaId).LessThanOrEqualTo(0).WithMessage(MenssagesException.TIPO_RECEITA_INVALIDO);
        }
    }
}
