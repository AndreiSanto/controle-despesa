using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Repositorys.Usuarios.Interface;
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

        public async Task<Usuario?> GetUsuarioAsync(string email, string password)
        {
            
            var usuario = await _apiContext.Usuarios
                .SingleOrDefaultAsync(u => u.Email == email);

           

            return usuario;
        }

        public async Task<Usuario?> GetUsuarioIdAsync(int id)
        {
            return await _apiContext.Usuarios.FindAsync(id);
        }
    }
}
