using controleDespesa.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApiContext _apiContext;

        public UnitOfWork(ApiContext apiContext) => _apiContext = apiContext;

        public async Task Commit() => await _apiContext.SaveChangesAsync();
    }
}
