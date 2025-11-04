using controleDespesa.Communication.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Repositorys.Dashboard.Interface
{
    public interface IDashboardRepository
    {
        public Task<DashboardResponse> GetDashboard();

        public Task<List<ReceitaResponse>> GetDashboardReceita();

        public Task<List<DespesaResponse>> GetDashboardDespesa();

    }
}
