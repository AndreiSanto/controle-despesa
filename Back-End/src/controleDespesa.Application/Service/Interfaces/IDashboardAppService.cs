using controleDespesa.Application.DTOs;
using controleDespesa.Communication.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Application.Service.Interfaces
{
    public interface IDashboardAppService
    {

        public Task<DashboardResponse> GetDashboard(int idUsuario);

        public Task<List<ReceitaResponse>> GetDashboardReceita(int idUsuario);

        public Task<List<DespesaResponse>> GetDashboardDespesa(int idUsuario);
    }
}
