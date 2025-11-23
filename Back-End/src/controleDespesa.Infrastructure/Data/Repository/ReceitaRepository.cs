using controleDespesa.Communication.Response;
using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Repositorys.Receita.Interface;
using controleDespesa.Domain.Value_Objects.Filter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Infrastructure.Data.Repository
{
    public class ReceitaRepository : IReceitaRepository
    {
        private readonly ApiContext _apiContext;

        public ReceitaRepository(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task Add(Receita receita) => await _apiContext.Receitas.AddAsync(receita);

        public void AtualizarAsync(Receita receita)
        {
            _apiContext.Receitas.Update(receita);
        }

        public async Task<Receita> BuscarReceita(int id)
        {
            return await _apiContext.Receitas.FindAsync(id);
        }

        public async Task<bool> Excluir(int id)
        {
            var receita = await _apiContext.Receitas.FindAsync(id);

            if(receita == null)
            {
                return false;
            }

            _apiContext.Remove(receita);

            return true;

        }

        public async Task<List<TipoDespesaReceitaResponse>> ListarCategoriaReceita()
        {
            return await _apiContext.TipoDespesaReceitas.Where(b => b.Ativo && b.Tipo == Domain.Enums.TipoDespesaReceitaEnum.RECEITA).AsNoTracking()
            .Select(a => new TipoDespesaReceitaResponse()
            {
                Id = a.Id,
                Nome = a.Nome
            }).ToListAsync();
        }

      

        public async Task<RetornoPaginacao<ReceitaListaResponse>> ReceitaLista(int pagina, int totalPatina, Filtro filtro, int usuarioId)
        {

            var query = _apiContext.Receitas.Where(a=> a.UsuarioId == usuarioId).Include(a => a.TipoDespesaReceita).AsQueryable().AsNoTracking();


            if (!string.IsNullOrWhiteSpace(filtro.Descricao))
            {
                query = query.Where(a => a.Descricao.Contains(filtro.Descricao));
            }




            if (filtro.DataCadastroInicial.HasValue)
            {
                query = query.Where(a => a.DataCadastro >= filtro.DataCadastroInicial.Value);
            }


            if (filtro.DataCadastroFinal.HasValue)
            {
                query = query.Where(a => a.DataCadastro <= filtro.DataCadastroFinal.Value);
            }
            var queryLista = query.Select(a => new ReceitaListaResponse
            {
                Id = a.Id,
                Descricao = a.Descricao,
                DataCadastro = a.DataCadastro,
                Tipo = a.TipoDespesaReceita.Nome,
                ValorReceita = a.Valor

            });




            return await RetornoPaginacao<ReceitaListaResponse>.CriarAsync(pagina, totalPatina, queryLista);

            
        }
    }
}
