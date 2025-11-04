using controleDespesa.Application.DTOs;
using controleDespesa.Application.Service.Interfaces;
using controleDespesa.Communication.Response;
using controleDespesa.Domain.Repositorys.Dashboard.Interface;
using controleDespesa.Domain.Repositorys.Despesa.Interface;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Application.Service
{
    public class DashboardAppService : IDashboardAppService
    {

        private readonly IDashboardRepository _dashboardRepository;
        private readonly IMemoryCache _cache;

        public DashboardAppService(IDashboardRepository dashboardRepository, IMemoryCache cache)
        {
            _dashboardRepository = dashboardRepository;
            _cache = cache;
        }

        public async Task<DashboardResponse> GetDashboard()
        {
            var cacheKey = "dashboard_cache";

            if (!_cache.TryGetValue(cacheKey, out DashboardResponse dashboard))
            {
                dashboard = await _dashboardRepository.GetDashboard();
                _cache.Set(cacheKey, dashboard, TimeSpan.FromMinutes(10));
            }

            return dashboard;
        }

        public async Task<List<DespesaResponse>> GetDashboardDespesa()
        {
            return await _dashboardRepository.GetDashboardDespesa();
        }

        public async Task<List<ReceitaResponse>> GetDashboardReceita()
        {
            return await _dashboardRepository.GetDashboardReceita();
        }
    }
}
