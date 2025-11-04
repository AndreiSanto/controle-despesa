using controleDespesa.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Infrastructure.Seguranca.Tokens.Acesso.Validator
{
    public class JwtTokenValidator : JwtTokenHandler, IAcessTokenValidator
    {
        private readonly string _chaveAssinatura;

        public JwtTokenValidator(string chaveAssinatura)
        {
            _chaveAssinatura = chaveAssinatura;
        }

        public Guid ValidateUsuarioIdentificador(string token)
        {
            var validationParameter = new TokenValidationParameters
            {
                ClockSkew = new TimeSpan(0),
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = SecurityKey(_chaveAssinatura)
            };

            var tokenHanadle = new JwtSecurityTokenHandler();

            var principal = tokenHanadle.ValidateToken(token, validationParameter, out _);
           var usuarioIdentificador =  principal.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            return Guid.Parse(usuarioIdentificador);

        }
    }
}
