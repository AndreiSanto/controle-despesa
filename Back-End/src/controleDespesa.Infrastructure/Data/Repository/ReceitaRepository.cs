using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Repositorys.Receita.Interface;
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


    }
}
