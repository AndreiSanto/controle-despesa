using CommonTestUtilies.RequisicaoDtos;
using controleDespesa.Application.Validation;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validacao.Test.Receita
{
    public class ReceitaValidatorTest
    {

        [Fact(DisplayName = "Validar Campos da Receita")]
        public void Sucesso_Cadastro()
        {

            var validador = new ReceitaValidator();

            var receitaDto = ReceitaDtoBuilder.Build();

            var resultado = validador.Validate(receitaDto);

            resultado.IsValid.Should().BeTrue("todos os campos obrigatórios foram preenchidos corretamente");

        }


        [Fact(DisplayName = "Validar Campos da Receita - Deve falhar quando a descrição estiver vazia")]
        public void Error_Descricao_Vazio()
        {

            var validador = new ReceitaValidator();

            var receitaDto = ReceitaDtoBuilder.Build();
            receitaDto.Descricao = string.Empty;


            var resultado = validador.Validate(receitaDto);


            resultado.IsValid.Should().BeFalse("a descrição da receita não pode estar vazia");

            resultado.Errors.Should()
                .Contain(e => e.PropertyName == nameof(receitaDto.Descricao)
                           && e.ErrorMessage == "A descrição da Receita, não pode ser vazio.");
        }


        [Fact(DisplayName = "Validar Campos da Receita - Deve falhar quando o valor for menor que ou igual a zero")]
        public void Error_Valor_Invalido()
        {

            var validador = new ReceitaValidator();

            var receitaDto = ReceitaDtoBuilder.Build();
            receitaDto.Valor = 0;


            var resultado = validador.Validate(receitaDto);


            resultado.IsValid.Should().BeFalse("o valor da receita é  invalido");

            resultado.Errors.Should()
                .Contain(e => e.PropertyName == nameof(receitaDto.Valor)
                           && e.ErrorMessage == "O valor da receita tem que ser maior do que 0");
        }



        [Fact(DisplayName = "Validar Campos da Despesa - Deve falhar quando o o tipo de despesa não foi selecionado")]
        public void Error_Tipo_Despesa_Invalido()
        {

            var validador = new ReceitaValidator();

            var receitaDto = ReceitaDtoBuilder.Build();
            receitaDto.TipoDespesaReceitaId = 0;


            var resultado = validador.Validate(receitaDto);


            resultado.IsValid.Should().BeFalse("o tipo de receita é invalido");

            resultado.Errors.Should()
                .Contain(e => e.PropertyName == nameof(receitaDto.TipoDespesaReceitaId)
                           && e.ErrorMessage == "Por favor selecionar um tipo de receita.");
        }

    }
}
