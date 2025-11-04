using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Repositorys.TipoDespesaReceita
{
    public interface ITipoDespesaReceitaRepository
    {
        public Task Add(Entities.TipoDespesaReceita tipoDespesaReceita);
    }

}
