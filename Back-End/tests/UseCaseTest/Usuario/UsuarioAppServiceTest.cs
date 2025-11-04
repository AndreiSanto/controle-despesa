using CommonTestUtilies.Cryptografia;
using CommonTestUtilies.Mapper;
using CommonTestUtilies.Repositories;
using CommonTestUtilies.RequisicaoDtos;
using controleDespesa.Application.Service;
using controleDespesa.Application.Service.Cryptografia;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceTest.Usuario
{
    public class UsuarioAppServiceTest
    {
        [Fact]
        public  async Task Sucesso()
        {

            var usuarioDTO = UsuarioDTOBuilder.Build();

            var usuario = this.Create();

            var resultado = await usuario.Cadastrar(usuarioDTO);

            resultado.Should().NotBeNull();
            resultado.Nome.Should().Be(usuarioDTO.Nome);


        }
        private UsuarioAppService Create()
        {
            var mapper = MapperBuilder.Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();

            var usuarioRepository = new UsuarioRepositoryBuilder().Build();

            var unitOfWork = UnitOfWorkBuilder.Build();

            return  new UsuarioAppService(mapper, passwordEncripter, usuarioRepository, unitOfWork, null);

        }
    }
}
