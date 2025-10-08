using controleDespesa.Application.Service;
using controleDespesa.Application.Service.Cryptografia;
using controleDespesa.Application.Service.Interfaces;
using controleDespesa.Domain.Repositorys.Usuario.Interface;
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
            services.AddScoped<PasswordEncripter>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());




        }
    }
}
