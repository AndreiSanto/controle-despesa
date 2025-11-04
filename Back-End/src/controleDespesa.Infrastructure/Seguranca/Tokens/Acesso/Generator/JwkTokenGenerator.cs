using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Infrastructure.Seguranca.Tokens.Acesso.Generator
{
    public class JwkTokenGenerator : JwtTokenHandler, IAcessTokenGenerator
    {
        private readonly uint _tempoExpiracao;
        private readonly string _chaveAssinatura;

        public JwkTokenGenerator(uint tempoExpiracao, string chaveAssinatura)
        {
            _tempoExpiracao = tempoExpiracao;
            _chaveAssinatura = chaveAssinatura;
        }

        public string GenerateToken(Guid idIdenficador)
        {
            var claims = new[]
           {
                new Claim(ClaimTypes.Sid, idIdenficador.ToString())
               
            };

            var tokenDescricao = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_tempoExpiracao),
                SigningCredentials = new SigningCredentials(SecurityKey(_chaveAssinatura), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescricao);
            return tokenHandler.WriteToken(securityToken);
        }
        
    }
}
