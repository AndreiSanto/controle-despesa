using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Service
{
    public class DespesaDomainService : IDespesaDomainService
    {
        public List<DespesaParcela> GerarParcelas(Despesa despesa)
        {
            if (despesa == null)
                throw new ArgumentNullException(nameof(despesa), "Erro ao gerar as parcelas");

            if (despesa.Parcelado && despesa.NumeroDeParcela <= 0)
                throw new ValidationException("O número de parcelas deve ser maior que zero.");

            var parcelas = new List<DespesaParcela>();

            decimal valorBase = Math.Round(despesa.ValorDespesa / despesa.NumeroDeParcela, 2);
            decimal totalParcelado = 0;

            if (despesa.Parcelado)
            {
                var primeiroVencimento = despesa.DataVencimento.Value.AddMonths(1);

                for (int i = 1; i <= despesa.NumeroDeParcela; i++)
                {
                    decimal valorParcela = valorBase;

                    // Última parcela recebe o ajuste (diferença de centavos)
                    if (i == despesa.NumeroDeParcela)
                        valorParcela = despesa.ValorDespesa - totalParcelado;

                    parcelas.Add(new DespesaParcela
                    {
                        NumeroDaParcela = i,
                        SituacaoParcela = Enums.SituacaoParcelaEnum.A_VENCER,
                        DataVencimento = primeiroVencimento.AddMonths(i - 1).Date,
                        Valor = valorParcela
                    });

                    totalParcelado += valorParcela;
                }
            }

            return parcelas;
        }
    }

}
