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
    public class JwtTokenGenerator : JwtTokenHandler, IAcessTokenGenerator
    {
        private readonly uint _tempoExpiracao;
        private readonly string _chaveAssinatura;

        public JwtTokenGenerator(uint tempoExpiracao, string chaveAssinatura)
        {
            _tempoExpiracao = tempoExpiracao;
            _chaveAssinatura = chaveAssinatura;
        }

        public string GenerateToken(Guid idIdentificador, int idUsuario)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.Sid, idIdentificador.ToString()),
        new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString())
    };

            var tokenDescricao = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_tempoExpiracao),
                Issuer = "ControleDespesa", 
                Audience = "ControleDespesa",
                SigningCredentials = new SigningCredentials(
                    SecurityKey(_chaveAssinatura),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescricao);
            return tokenHandler.WriteToken(securityToken);
        }


        public string GerarRefreshToken()
        {
            return Guid.NewGuid().ToString("N");
        }

       
    }
}

