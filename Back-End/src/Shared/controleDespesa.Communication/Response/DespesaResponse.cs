using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Communication.Response
{
    public class DespesaResponse
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }

        public DateTime DataDespesa { get; set; }

        public bool DespesaFixa { get; set; }

        public string Tipo { get; set; } = string.Empty;

    }
}
