using controleDespesa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Repositorys.Usuario.Interface
{
    public interface IUsuarioRepository
    {
        public  Task Add(Entities.Usuario usuario);
        public  Task<bool> ExisteEmailCadastrado(string email);
    }
}
