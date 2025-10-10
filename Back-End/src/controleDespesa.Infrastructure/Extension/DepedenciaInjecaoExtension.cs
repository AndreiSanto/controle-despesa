using controleDespesa.Domain.Interface;
using controleDespesa.Domain.Repositorys.Usuario.Interface;
using controleDespesa.Infrastructure.Data;
using controleDespesa.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Infrastructure.Extension
{
    public static class DepedenciaInjecaoExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);
            AddApiContext(services, configuration);

        }
        private static void AddApiContext(IServiceCollection services, IConfiguration configuration) {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApiContext>(options =>
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    b => b.MigrationsAssembly("controleDespesa.Application") // opcional
                )
            );

        }
        private static void AddRepositories(IServiceCollection services) {

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


        }
    }
}
