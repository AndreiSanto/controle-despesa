using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Repositorys.TipoDespesaReceita;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Infrastructure.Data.Repository
{
    public class TipoDespesaReceitaRepository : ITipoDespesaReceitaRepository
    {
        private readonly ApiContext _apiContext;

        public TipoDespesaReceitaRepository(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task Add(TipoDespesaReceita tipoDespesaReceita)
        {
            await _apiContext.TipoDespesaReceitas.AddAsync(tipoDespesaReceita);
        }
    }
}
