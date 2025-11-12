using controleDespesa.Domain.Security.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace controleDespesa.API.Filters
{
    public class AutenticacaoUsuarioFilter : IAsyncAuthorizationFilter
    {
        private readonly IAcessTokenValidator _acessTokenValidator;

        public AutenticacaoUsuarioFilter(IAcessTokenValidator acessTokenValidator)
        {
            _acessTokenValidator = acessTokenValidator;

        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = TokenRequest(context);
                var usuarioIdentificacao = _acessTokenValidator.ValidateUsuarioIdentificador(token);


            }
            catch (SecurityTokenExpiredException)
            {
                context.Result = new UnauthorizedObjectResult("Token expirado");
            }
            catch (UnauthorizedAccessException ex)
            {
                context.Result = new UnauthorizedObjectResult("Token invalido");
            }


        }
        private static string TokenRequest(AuthorizationFilterContext context)
        {
            var autenticacao = context.HttpContext.Request.Headers.Authorization.ToString();

            if (string.IsNullOrWhiteSpace(autenticacao))
            {
                throw new UnauthorizedAccessException("Token invalido");
            }

            return autenticacao["Bearer ".Length..].Trim();

        }
    }
}
