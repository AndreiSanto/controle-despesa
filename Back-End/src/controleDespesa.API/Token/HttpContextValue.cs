using controleDespesa.Domain.Security.Tokens;

namespace controleDespesa.API.Token
{
    public class HttpContextValue : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HttpContextValue(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string Value()
        {
            var autenticacao = _contextAccessor.HttpContext.Request.Headers.Authorization.ToString();

            return autenticacao["Bearer ".Length..].Trim();
        }
    }
}
