using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Communication.Response
{
    public class ReceitaResponse
    {

        public string Descricao { get; set; } = string.Empty;

        public decimal Valor { get; set; }

        public DateTime DataReceita { get; set; }

        public string Tipo { get; set; } = string.Empty;

        public bool ReceitaFixa { get; set; }
    }
}
