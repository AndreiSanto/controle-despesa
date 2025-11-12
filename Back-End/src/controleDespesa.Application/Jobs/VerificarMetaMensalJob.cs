using controleDespesa.Application.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Application.Jobs
{
    public class VerificarMetaMensalJob
    {
        private readonly IDespesaAppService _despesaService;
       // private readonly IMetaService _metaService;

        public VerificarMetaMensalJob(
            IDespesaAppService despesaService
            )
        {
            _despesaService = despesaService;
           
        }

        public async Task ExecutarAsync()
        {
            var totalDespesas = await _despesaService.ObterTotalDoMesAsync();
            

           
        }

    }
}
