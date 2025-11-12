using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace controleDespesa.API.Token
{
    public static class TokenHelpers
    {
        public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection("Jwt");
            var jwtIssuer = jwtSection["Issuer"];
            var jwtKey = jwtSection["ChaveAssinatura"];

            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtIssuer,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
            };

        }
    }
}
