using controleDespesa.Domain.Repositorys.Usuarios.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilies.Repositories
{
    public class UsuarioRepositoryBuilder
    {
        private readonly Mock<IUsuarioRepository> _repository;

        public UsuarioRepositoryBuilder()
        {
            _repository = new Mock<IUsuarioRepository>();
        }

        public void ExisteEmailCadastrado(string email)
        {
            _repository.Setup(a => a.ExisteEmailCadastrado(email)).ReturnsAsync(true);
        }

        public  IUsuarioRepository Build()
        {
            

            return _repository.Object;
        }
    }
}
