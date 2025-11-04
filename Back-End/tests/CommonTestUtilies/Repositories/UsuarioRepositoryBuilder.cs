using controleDespesa.Domain.Repositorys.Usuario.Interface;
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

        public  IUsuarioRepository Build()
        {
            

            return _repository.Object;
        }
    }
}
