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
    public class UsuarioValidator : AbstractValidator<UsuarioDTO>
    {
        public UsuarioValidator() {

            RuleFor(usuario => usuario.Nome).NotEmpty().WithMessage(MenssagesException.NOME_VAZIO);
            RuleFor(usuario => usuario.Email).NotEmpty().WithMessage(MenssagesException.EMAIL_VAZIO); ;
            RuleFor(usuario => usuario.Email).EmailAddress().WithMessage(MenssagesException.EMAIL_INVALIDO); ;
            RuleFor(usuario => usuario.Password.Length).GreaterThanOrEqualTo(6).WithMessage(MenssagesException.SENHA_CURTA); ;
           
        
        }
    }
}
