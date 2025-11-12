using controleDespesa.Application.DTOs;
using controleDespesa.Communication.Response;
using controleDespesa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Application.Service.Interfaces
{
    public interface IReceitaAppService
    {

        public Task<Receita> Cadastro(ReceitaDTO receitaDTO, int usuarioId);
        public Task<Receita> Editar(ReceitaDTO receitaDTO);
        public Task<bool> Excluir(int id);
        public Task<Receita> BuscarReceita(int id);
        public Task<List<Receita>> BuscarReceitas();
        public Task<List<TipoDespesaReceitaResponse>> ListarCategoriasReceita();

        public Task<RetornoPaginacao<Receita>> ReceitaLista(int pagina, int totalPatina);

        public Task AtualizarAsync(ReceitaDTO receitaDTO);




    }
}
