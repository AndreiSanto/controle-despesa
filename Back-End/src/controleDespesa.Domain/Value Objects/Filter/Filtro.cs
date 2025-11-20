using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Value_Objects.Filter
{
    public class Filtro
    {

        public string Descricao { get; set; } = string.Empty;
        public DateTime? DataCadastroInicial { get; set; }
        public DateTime? DataCadastroFinal { get; set; }
    }
}
