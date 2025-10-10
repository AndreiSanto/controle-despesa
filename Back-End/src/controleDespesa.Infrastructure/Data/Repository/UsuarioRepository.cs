using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Repositorys.Usuario.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Infrastructure.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApiContext _apiContext;

        public UsuarioRepository(ApiContext apiContext) => _apiContext = apiContext;


        public async Task Add(Usuario usuario)
        {

            await _apiContext.Usuarios.AddAsync(usuario);
        }

        public async Task<bool> ExisteEmailCadastrado(string email)
        {
            return await _apiContext.Usuarios.AnyAsync(a => a.Email.Equals(email) && a.Ativo);
        }

    }
}
