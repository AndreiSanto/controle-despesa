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
    public class TipoDespesaReceitaValidator : AbstractValidator<TipoDespesaReceitaDTO>
    {
        public TipoDespesaReceitaValidator()
        {
            RuleFor(tipos => tipos.Nome).NotEmpty().WithMessage(MenssagesException.NOME_VAZIO);
        }
    }
}
