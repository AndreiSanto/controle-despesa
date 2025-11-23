using controleDespesa.Communication.Response;
using controleDespesa.Communication.Response.Despesa;
using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Repositorys.Despesa.Interface;
using controleDespesa.Domain.Value_Objects.Filter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Infrastructure.Data.Repository
{
    public class DespesaRepository : IDespesaRepository
    {

        private readonly ApiContext _apiContext;

        public DespesaRepository(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task Add(Despesa despesa) => await _apiContext.Despesas.AddAsync(despesa);

        public  void AtualizarAsync(Despesa despesa)
        {
              _apiContext.Despesas.Update(despesa);
        }

        public async Task<Despesa> BuscarDespesa(int id)
        {
            return await _apiContext.Despesas.FindAsync(id);
        }

        public async Task<RetornoPaginacao<DespesaListaResponse>> DespesaLista(
     int pagina,
     int totalPagina,
     Filtro filtro,int idUsuario)
        {
            var query = _apiContext.Despesas.Where(a => a.UsuarioId == idUsuario).Include(p => p.TipoDespesaReceita).AsQueryable().AsNoTracking();

            
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

            var queryResponse = query.Select(a => new DespesaListaResponse
            {
                DataCadastro = a.DataCadastro,
                Tipo = a.TipoDespesaReceita.Nome,
                Id = a.Id,
                ValorDespesa = a.ValorDespesa,
                Descricao = a.Descricao
                

            });

            
            return await RetornoPaginacao<DespesaListaResponse>.CriarAsync(
                pagina,
                totalPagina,
                queryResponse);
        }


        public async Task<bool> ExcluirAsync(int id)
        {
           var despesa = await _apiContext.Despesas.FindAsync(id);

            if(despesa == null)
            {
                return false;
            }
             _apiContext.Despesas.Remove(despesa);
            return true;
        }

        public async Task<List<TipoDespesaReceitaResponse>> ListarCategoriaReceita()
        {
            return await _apiContext.TipoDespesaReceitas.Where(b => b.Ativo && b.Tipo == Domain.Enums.TipoDespesaReceitaEnum.DESPESA).AsNoTracking()
            .Select(a => new TipoDespesaReceitaResponse()
            {
                Id = a.Id,
                Nome = a.Nome
            }).ToListAsync();
        }

        public async Task<decimal> ObterTotalDoMesAsync()
        {
            var hoje = DateTime.UtcNow;
            var inicioMes = new DateTime(hoje.Year, hoje.Month, 1);
            return await _apiContext.Despesas
                .Where(d => d.DataDespesa >= inicioMes)
                .SumAsync(d => d.ValorDespesa);
        }
    }
    
}
