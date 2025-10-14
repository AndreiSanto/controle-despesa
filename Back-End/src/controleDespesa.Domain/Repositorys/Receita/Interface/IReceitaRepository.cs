using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Repositorys.Receita.Interface
{
    public interface IReceitaRepository
    {
        public Task Add(Entities.Receita receita);
    }
}
