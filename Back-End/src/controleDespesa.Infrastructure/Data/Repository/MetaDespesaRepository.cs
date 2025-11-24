using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Repositorys.MetaDespesas.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Infrastructure.Data.Repository
{
    public class MetaDespesaRepository : IMetaDespesaRepository
    {
        private readonly ApiContext _apiContext;

        public MetaDespesaRepository(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public MetaDespesa Alterar(MetaDespesa metaDespesa)
        {
            _apiContext.MetaDespesas.Update(metaDespesa);
            return metaDespesa;
        }

        public async Task<MetaDespesa?> BuscarMeta(int UsuarioId)
        {
           return await _apiContext.MetaDespesas.AsNoTracking().Where(a => a.UsuarioId == UsuarioId).SingleOrDefaultAsync();
        }

        public async Task Cadastro(MetaDespesa metaDespesa)
        {
            await _apiContext.MetaDespesas.AddAsync(metaDespesa);
        }

       
    }
}
