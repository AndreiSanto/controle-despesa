using CommonTestUtilies.RequisicaoDtos;
using controleDespesa.Application.DTOs;
using controleDespesa.Application.Validation;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validacao.Test.Despesa
{
    public class DespesaValidatorTest
    {

        [Fact(DisplayName = "Validar Campos da Despesa")]
        public void Sucesso_Cadastro()
        {

            var validador = new DespesaValidator();

            var despesaDto = DespesaDTOBuilder.Build();

            var resultado = validador.Validate(despesaDto);

            resultado.IsValid.Should().BeTrue("todos os campos obrigatórios foram preenchidos corretamente");

        }


        [Fact(DisplayName = "Validar Campos da Despesa - Deve falhar quando a descrição estiver vazia")]
        public void Error_Descricao_Vazio()
        {
           
            var validador = new DespesaValidator();
            var despesaDto = DespesaDTOBuilder.Build();
            despesaDto.Descricao = string.Empty; 

           
            var resultado = validador.Validate(despesaDto);

            
            resultado.IsValid.Should().BeFalse("a descrição da despesa não pode estar vazia");

            resultado.Errors.Should()
                .Contain(e => e.PropertyName == nameof(DespesaDTO.Descricao)
                           && e.ErrorMessage == "A descrição da despesa, não pode ser vazio.");
        }


        [Fact(DisplayName = "Validar Campos da Despesa - Deve falhar quando o valor for menor que ou igual a zero")]
        public void Error_Valor_Invalido()
        {

            var validador = new DespesaValidator();
            var despesaDto = DespesaDTOBuilder.Build();
            despesaDto.ValorDespesa = 0;


            var resultado = validador.Validate(despesaDto);


            resultado.IsValid.Should().BeFalse("o valor da despesa invalido");

            resultado.Errors.Should()
                .Contain(e => e.PropertyName == nameof(DespesaDTO.ValorDespesa)
                           && e.ErrorMessage == "O valor da despesa tem que ser maior do que 0");
        }



        [Fact(DisplayName = "Validar Campos da Despesa - Deve falhar quando o o tipo de despesa não foi selecionado")]
        public void Error_Tipo_Despesa_Invalido()
        {

            var validador = new DespesaValidator();
            var despesaDto = DespesaDTOBuilder.Build();
            despesaDto.TipoDespesaReceitaId = 0;


            var resultado = validador.Validate(despesaDto);


            resultado.IsValid.Should().BeFalse("o tipo de despesa é  invalido");

            resultado.Errors.Should()
                .Contain(e => e.PropertyName == nameof(DespesaDTO.TipoDespesaReceitaId)
                           && e.ErrorMessage == "Por favor, selecione um tipo de Despesa.");
        }

    }
}
