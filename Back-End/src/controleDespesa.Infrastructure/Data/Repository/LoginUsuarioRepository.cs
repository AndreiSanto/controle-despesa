using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Interface;
using controleDespesa.Domain.Repositorys.Login.Interface;
using controleDespesa.Domain.Security.Tokens;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Infrastructure.Data.Repository
{
    public class LoginUsuarioRepository : ILoginRepository
    {
        private readonly ApiContext _apiContext;
        private readonly ITokenProvider _tokenProvider;

        public LoginUsuarioRepository(ApiContext apiContext, ITokenProvider tokenProvider)
        {
            _apiContext = apiContext;
            _tokenProvider = tokenProvider;
        }

        public async Task<Usuario> Usuario()
        {
            var tokenString =  _tokenProvider.Value();
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenString);

            var userId = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userIdentificador = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            var identificador = Guid.Parse(userIdentificador);
            var id = int.Parse(userId);
            return await _apiContext.Usuarios.AsNoTracking()
                .FirstAsync(user => user.Ativo && user.Identificador == identificador
                && user.Id == id
                );
        }
    }
}
