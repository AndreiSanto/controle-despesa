using controleDespesa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Repositorys.Usuarios.Interface
{
    public interface IUsuarioRepository
    {
        public  Task Add(Usuario usuario);
        public  Task<bool> ExisteEmailCadastrado(string email);

        public Task<Usuario?> GetUsuarioAsync(string email, string Password);
        public Task<Usuario?> GetUsuarioIdAsync(int id);

         
    }
}
