using controleDespesa.Application.DTOs;
using controleDespesa.Communication.Response;
using controleDespesa.Communication.Response.Despesa;
using controleDespesa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Application.Service.Interfaces
{
    public interface IDespesaAppService
    {
        public Task<Despesa> Cadastro(DespesaDTO despesaDTO, int usuarioId);

        public Task<RetornoPaginacao<Despesa>> DespesaLista(int pagina, int totalPatina, FiltroDTO filtro);

        public Task<List<TipoDespesaReceitaResponse>> ListarCategoriasDespesa();

        public Task AtualizarAsync(DespesaDTO despesaDto);

        public Task<bool> ExcluirAsync(int id);

        public Task<Despesa> BuscarDespesa(int id);

        public Task<decimal> ObterTotalDoMesAsync();


    }
}
