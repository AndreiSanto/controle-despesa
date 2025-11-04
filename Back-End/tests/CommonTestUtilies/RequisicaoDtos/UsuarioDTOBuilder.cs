using Bogus;
using controleDespesa.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilies.RequisicaoDtos
{
    public class UsuarioDTOBuilder
    {

        public static UsuarioDTO Build( int tamanhoSenha = 10)
        {
            return new Faker<UsuarioDTO>()
                .RuleFor(usuario => usuario.Nome, (f) => f.Person.FirstName)
                .RuleFor(usuario => usuario.Email, (f, u) => f.Internet.Email(u.Nome))
                .RuleFor(usuario => usuario.Password, (f) => f.Internet.Password(tamanhoSenha))
                
                ;


        }
    }
}
