using CommonTestUtilies.RequisicaoDtos;
using controleDespesa.Application.Validation;
using controleDespesa.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validacao.Test.Usuario.Cadastro
{
    public class UsuarioValidatorTest
    {
        [Fact(DisplayName ="Teste para validar Campos do Usuario")]
        public void Sucesso()
        {
            var validator = new UsuarioValidator();

            var usuarioDTO = UsuarioDTOBuilder.Build();

            var resultado = validator.Validate(usuarioDTO);

            //Assert 

            Assert.True(resultado.IsValid);

        }

        [Fact(DisplayName = "Teste para validar Campos do Usuario")]
        public void Error_Nome_Vazio()
        {
            var validator = new UsuarioValidator();

            var usuarioDTO = UsuarioDTOBuilder.Build();

            usuarioDTO.Nome = string.Empty;

            var resultado = validator.Validate(usuarioDTO);

            //Assert 

            Assert.False(resultado.IsValid);
            Assert.Contains(resultado.Errors, e => e.PropertyName == "Nome" && e.ErrorMessage.Contains(MenssagesException.NOME_VAZIO));
        }

        [Fact(DisplayName = "Teste para validar Campos do Usuario")]
        public void Error_Email_Vazio()
        {
            var validator = new UsuarioValidator();

            var usuarioDTO = UsuarioDTOBuilder.Build();

            usuarioDTO.Email = string.Empty;

            var resultado = validator.Validate(usuarioDTO);

            //Assert 

            Assert.False(resultado.IsValid);
            Assert.Contains(resultado.Errors, e => e.PropertyName == "Email" && e.ErrorMessage.Contains(MenssagesException.EMAIL_VAZIO));
        }

        [Fact(DisplayName = "Teste para validar Campos do Usuario")]
        public void Error_Email_Invalido()
        {
            var validator = new UsuarioValidator();

            var usuarioDTO = UsuarioDTOBuilder.Build();

            usuarioDTO.Email = "email.com";

            var resultado = validator.Validate(usuarioDTO);

            //Assert 

            Assert.False(resultado.IsValid);
            Assert.Contains(resultado.Errors, e => e.PropertyName == "Email" && e.ErrorMessage.Contains(MenssagesException.EMAIL_INVALIDO));
        }
        [Theory(DisplayName ="Teste das Senhas")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
     
        public void Error_Senha_Invalido(int tamanhoSenha)
        {
            var validator = new UsuarioValidator();

            var usuarioDTO = UsuarioDTOBuilder.Build(tamanhoSenha);


            var resultado = validator.Validate(usuarioDTO);

            //Assert 

            Assert.False(resultado.IsValid);
            Assert.Contains(resultado.Errors, e => e.PropertyName == "Password.Length" && e.ErrorMessage.Contains(MenssagesException.SENHA_CURTA));
        }
    }
}
