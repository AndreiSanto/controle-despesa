using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Entities
{
    public class DespesaParcela
    {
        public int Id { get; set; }

        public decimal Valor { get; set; }

        public DateTime DataVencimento { get; set; }

        public bool Pago { get; set; } = false;
    }
}
