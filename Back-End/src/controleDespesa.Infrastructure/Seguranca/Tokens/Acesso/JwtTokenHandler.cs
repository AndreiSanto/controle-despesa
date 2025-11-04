using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Infrastructure.Seguranca.Tokens.Acesso
{
    public abstract class JwtTokenHandler
    {
        protected SymmetricSecurityKey SecurityKey(string chaveAssinatura)
        {
            var bytes = Encoding.UTF8.GetBytes(chaveAssinatura);
            return new SymmetricSecurityKey(bytes);


        }
    }
}
