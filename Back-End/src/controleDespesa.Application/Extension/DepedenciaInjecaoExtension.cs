using controleDespesa.Application.Service;
using controleDespesa.Application.Service.Cryptografia;
using controleDespesa.Application.Service.Interfaces;

using Microsoft.Extensions.DependencyInjection;


namespace controleDespesa.Application.Extension
{
    public static class DepedenciaInjecaoExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddServicesApplication(services);

        }

        private static void AddServicesApplication(IServiceCollection services)
        {

            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            services.AddScoped<IDespesaAppService, DespesaAppService>();
            services.AddScoped<ITipoDespesaReceitaAppService, TipoDespesaReceitaAppService>();
            services.AddScoped<IReceitaAppService, ReceitaAppService>();
            services.AddScoped<IDashboardAppService, DashboardAppService>();
            services.AddScoped<ILoginAppService, LoginAppService>();

            services.AddScoped<PasswordEncripter>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());




        }
    }
}
