using controleDespesa.Communication.Response;
using controleDespesa.Domain.Repositorys.Dashboard.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Infrastructure.Data.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApiContext _context;

        public DashboardRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<DashboardResponse> GetDashboard(int idUsuario)
        {
            var dataHoje = DateTime.Now;
            var totalDespesa = await _context.Despesas.AsNoTracking().Where(a => a.DataDespesa.Month == dataHoje.Month
            && a.DataDespesa.Year == dataHoje.Year && a.UsuarioId == idUsuario
            ).Select(a => a.ValorDespesa).SumAsync();

            var totalReceita = await _context.Receitas.AsNoTracking().
                Where(a => a.DataCadastro.Month == dataHoje.Month && a.DataCadastro.Year == dataHoje.Year && a.UsuarioId == idUsuario).
                Select(a => a.Valor).SumAsync();



            var despesas = await (
                   from despesa in _context.Despesas.AsNoTracking()
                   join tipo in _context.TipoDespesaReceitas.AsNoTracking()
                       on despesa.TipoDespesaReceitaId equals tipo.Id
                   where despesa.DataDespesa.Month == dataHoje.Month
                      && despesa.DataDespesa.Year == dataHoje.Year && despesa.UsuarioId == idUsuario
                   orderby despesa.DataDespesa descending
                   select new DespesaResponse
                   {
                       Descricao = despesa.Descricao,
                       Valor = despesa.ValorDespesa,
                       DataDespesa = despesa.DataDespesa,
                       Tipo = tipo.Nome,
                       DespesaFixa = despesa.DespesaFixa,
                   }
               )
               .Take(3)
               .ToListAsync();


            var receitas = await (
               from receita in _context.Receitas.AsNoTracking()
               join tipo in _context.TipoDespesaReceitas.AsNoTracking()
                   on receita.TipoDespesaReceitaId equals tipo.Id
               where receita.DataCadastro.Month == dataHoje.Month
                  && receita.DataCadastro.Year == dataHoje.Year && receita.UsuarioId == idUsuario
               orderby receita.DataCadastro descending
               select new ReceitaResponse
               {
                   Descricao = receita.Descricao,
                   Valor = receita.Valor,
                   DataReceita = receita.DataCadastro,
                   Tipo = tipo.Nome,
                   ReceitaFixa = receita.ReceitaFixa

               }
           )
           .Take(3)
           .ToListAsync();

            var metaMes = await _context.MetaDespesas
                    .Where(a => a.UsuarioId == idUsuario)
                    .Select(a => a.Valor)
                     .SingleOrDefaultAsync();

            





            return new DashboardResponse()
            {
                MetaMes = metaMes == 0 ? 0: metaMes,
                TotalDespesas = totalDespesa,
                TotalReceitas = totalReceita,
                DespesaResponses = despesas,
                ReceitaResponses = receitas,

            };


        }

        public async Task<List<DespesaResponse>> GetDashboardDespesa(int idUsuario)
        {
            var dataHoje = DateTime.Now;

            var despesas = await (
                from despesa in _context.Despesas.AsNoTracking()
                join tipo in _context.TipoDespesaReceitas.AsNoTracking()
                    on despesa.TipoDespesaReceitaId equals tipo.Id
                where despesa.DataDespesa.Month == dataHoje.Month
                   && despesa.DataDespesa.Year == dataHoje.Year && despesa.UsuarioId == idUsuario
                orderby despesa.DataDespesa descending
                select new DespesaResponse
                {
                    Descricao = despesa.Descricao,
                    Valor = despesa.ValorDespesa,
                    DataDespesa = despesa.DataDespesa,
                    Tipo = tipo.Nome,
                    DespesaFixa = despesa.DespesaFixa,
                }
            )
            .Take(3)
            .ToListAsync();

            return despesas;

        }


        public async Task<List<ReceitaResponse>> GetDashboardReceita(int idUsuario)
        {
            var dataHoje = DateTime.Now;
            var receitas = await (
                from receita in _context.Receitas.AsNoTracking()
                join tipo in _context.TipoDespesaReceitas.AsNoTracking()
                    on receita.TipoDespesaReceitaId equals tipo.Id
                where receita.DataCadastro.Month == dataHoje.Month
                   && receita.DataCadastro.Year == dataHoje.Year && receita.UsuarioId == idUsuario
                orderby receita.DataCadastro descending
                select new ReceitaResponse
                {
                    Descricao = receita.Descricao,
                    Valor = receita.Valor,
                    DataReceita = receita.DataCadastro,
                    Tipo = tipo.Nome,
                    ReceitaFixa = receita.ReceitaFixa
                  
                }
            )
            .Take(3)
            .ToListAsync();



          

            return receitas;
        }
    }
}
