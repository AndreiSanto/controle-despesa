using controleDespesa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Interface
{
    public interface IDespesaDomainService
    {
        public List<DespesaParcela> GerarParcelas(Despesa despesa);


    }
}
