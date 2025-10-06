using controleDespesa.Application.DTOs;
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

            RuleFor(usuario => usuario.Nome).NotEmpty();
            RuleFor(usuario => usuario.Email).NotEmpty();
            RuleFor(usuario => usuario.Email).EmailAddress();
            RuleFor(usuario => usuario.Password.Length).GreaterThanOrEqualTo(6);
           
        
        }
    }
}
