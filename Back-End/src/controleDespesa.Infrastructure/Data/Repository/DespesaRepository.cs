using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Repositorys.Despesa.Interface;
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
    }
}
