using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Communication.Response
{
    public class ReceitaListaResponse
    {

        public int Id { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public decimal ValorReceita { get; set; }

        public DateTime DataCadastro { get; set; }
        public string Tipo { get; set; } = string.Empty;
    }
}
