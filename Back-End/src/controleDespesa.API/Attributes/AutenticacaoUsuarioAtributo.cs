using controleDespesa.API.Filters;
using controleDespesa.Domain.Security.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace controleDespesa.API.Attributes
{
    public class AutenticacaoUsuarioAttributes : TypeFilterAttribute //vai verificar se ele é valido
    {
        public AutenticacaoUsuarioAttributes() : base(typeof(AutenticacaoUsuarioFilter))
        {
        }
    }
}
