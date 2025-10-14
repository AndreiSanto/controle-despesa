using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Repositorys.Despesa.Interface
{
    public  interface IDespesaRepository
    {
        public Task Add(Entities.Despesa despesa);

    }
}
