using CommonTestUtilies.Cryptografia;
using CommonTestUtilies.Mapper;
using CommonTestUtilies.Repositories;
using CommonTestUtilies.RequisicaoDtos;
using controleDespesa.Application.Service;
using controleDespesa.Application.Service.Cryptografia;
using controleDespesa.Exceptions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceTest.Usuario
{
    public class UsuarioAppServiceTest
    {
        [Fact(DisplayName ="Criação do Usuario")]
        public  async Task Sucesso()
        {

            var usuarioDTO = UsuarioDTOBuilder.Build();

            var usuario = this.Create();

            var resultado = await usuario.Cadastrar(usuarioDTO);

            resultado.Should().NotBeNull();
            resultado.Nome.Should().Be(usuarioDTO.Nome);


        }

        [Fact(DisplayName = "Erro ao cadastrar com um email já cadastrado")]
        public async Task Error_Email_Ja_Registrado()
        {

            var usuarioDTO = UsuarioDTOBuilder.Build();
            var usuario = this.Create(usuarioDTO.Email);

            Func<Task> act = async () => await usuario.Cadastrar(usuarioDTO);


            var exception = (await act.Should()
                .ThrowAsync<FluentValidation.ValidationException>())
                .Which;


            exception.Message.Should().Be("Email Já cadastrado");
        }

        [Fact(DisplayName = "Erro ao cadastrar um usuario com o campo nome vazio")]
        public async Task Error_Nome_Vazio()
        {

            var usuarioDTO = UsuarioDTOBuilder.Build();
            var usuario =  this.Create();
            usuarioDTO.Nome = string.Empty;
            

            Func<Task> act = async () => await usuario.Cadastrar(usuarioDTO);


            var exception = (await act.Should()
                .ThrowAsync<FluentValidation.ValidationException>())
                .Which;


            exception.Message.Should().Be(MenssagesException.NOME_VAZIO);
        }


        [Fact(DisplayName = "Erro ao cadastrar um usuario com o campo email vazio")]
        public async Task Error_Email_Vazio()
        {

            var usuarioDTO = UsuarioDTOBuilder.Build();
            var usuario = this.Create();
            usuarioDTO.Email = string.Empty;


            Func<Task> act = async () => await usuario.Cadastrar(usuarioDTO);


            var exception = (await act.Should()
                .ThrowAsync<FluentValidation.ValidationException>())
                .Which;


            exception.Message.Should().Be(MenssagesException.EMAIL_VAZIO);
        }

        [Fact(DisplayName = "Erro ao cadastrar um usuario com o campo email invalido")]
        public async Task Error_Email_Invalido()
        {

            var usuarioDTO = UsuarioDTOBuilder.Build();
            var usuario = this.Create();
            usuarioDTO.Email = "sadsadsa.com";


            Func<Task> act = async () => await usuario.Cadastrar(usuarioDTO);


            var exception = (await act.Should()
                .ThrowAsync<FluentValidation.ValidationException>())
                .Which;


            exception.Message.Should().Be(MenssagesException.EMAIL_INVALIDO);
        }

        [Fact(DisplayName = "Erro ao cadastrar um usuario com a senha co menos 6 caractere")]
        public async Task Error_Senha_Vazio()
        {

            var usuarioDTO = UsuarioDTOBuilder.Build();
            var usuario = this.Create();
            usuarioDTO.Password = "12345";


            Func<Task> act = async () => await usuario.Cadastrar(usuarioDTO);


            var exception = (await act.Should()
                .ThrowAsync<FluentValidation.ValidationException>())
                .Which;


            exception.Message.Should().Be(MenssagesException.SENHA_CURTA);
        }

        private UsuarioAppService Create(string? email = null)
        {
            var mapper = MapperBuilder.Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();

            var usuarioRepositoryBuild = new UsuarioRepositoryBuilder();

            var unitOfWork = UnitOfWorkBuilder.Build();

            if(string.IsNullOrEmpty(email) == false)
            {
                usuarioRepositoryBuild.ExisteEmailCadastrado(email);
            }

            return  new UsuarioAppService(mapper, passwordEncripter, usuarioRepositoryBuild.Build(), unitOfWork, null);

        }
    }
}
