using controleDespesa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Repositorys.MetaDespesas.Interface
{
    public interface IMetaDespesaRepository
    {
        public Task Cadastro(MetaDespesa metaDespesa);
        public MetaDespesa Alterar(MetaDespesa metaDespesa);
        
    }
}
