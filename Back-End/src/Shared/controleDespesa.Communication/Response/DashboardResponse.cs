using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Communication.Response
{
    public class DashboardResponse
    {
        public DashboardResponse()
        {
            this.DespesaResponses = new List<DespesaResponse>();
            this.ReceitaResponses = new List<ReceitaResponse>();
        }

        public decimal TotalDespesas { get; set; }

        public decimal TotalReceitas { get; set; }
        public decimal MetaMes { get; set; }

        public ICollection<DespesaResponse> DespesaResponses { get; set; }

        public ICollection<ReceitaResponse> ReceitaResponses { get; set; }

    }
}
