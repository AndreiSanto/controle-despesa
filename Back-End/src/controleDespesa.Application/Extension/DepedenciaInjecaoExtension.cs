using controleDespesa.Application.Service;
using controleDespesa.Application.Service.Cryptografia;
using controleDespesa.Application.Service.Interfaces;
using controleDespesa.Domain.Interface;
using controleDespesa.Domain.Repositorys.Usuario.Interface;
using controleDespesa.Domain.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            services.AddScoped<IDespesaDomainService, DespesaDomainService>();

            services.AddScoped<PasswordEncripter>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());




        }
    }
}
