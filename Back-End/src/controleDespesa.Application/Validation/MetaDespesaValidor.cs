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
    public class MetaDespesaValidor : AbstractValidator<MetaDespesaDTO>
    {
        public MetaDespesaValidor()
        {
            RuleFor(meta => meta.Valor).GreaterThan(0).WithMessage(MenssagesException.VALOR_META_DESPESA_INVALIDO);
        }
    }
}
