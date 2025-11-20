using controleDespesa.Domain.Interface;
using controleDespesa.Domain.Repositorys.Dashboard.Interface;
using controleDespesa.Domain.Repositorys.Despesa.Interface;
using controleDespesa.Domain.Repositorys.Login.Interface;
using controleDespesa.Domain.Repositorys.Receita.Interface;
using controleDespesa.Domain.Repositorys.TipoDespesaReceita;
using controleDespesa.Domain.Repositorys.Token;
using controleDespesa.Domain.Repositorys.Usuarios.Interface;
using controleDespesa.Domain.Security.Tokens;
using controleDespesa.Infrastructure.Data;
using controleDespesa.Infrastructure.Data.Repository;
using controleDespesa.Infrastructure.Seguranca.Tokens.Acesso.Generator;
using controleDespesa.Infrastructure.Seguranca.Tokens.Acesso.Refresh;
using controleDespesa.Infrastructure.Seguranca.Tokens.Acesso.Validator;
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
            AddTokens(services, configuration);

            if (IsUnitTestEnviroment(configuration))
            {
                return;

            }
            AddApiContext(services, configuration);
           
        }
        private static void AddApiContext(IServiceCollection services, IConfiguration configuration) {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApiContext>(options =>
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    b => b.MigrationsAssembly("controleDespesa.Application")
                )
            );

        }
        private static void AddRepositories(IServiceCollection services) {

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IDespesaRepository, DespesaRepository>();
            services.AddScoped<ITipoDespesaReceitaRepository, TipoDespesaReceitaRepository>();
            services.AddScoped<IReceitaRepository, ReceitaRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<ILoginRepository, LoginUsuarioRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();


        }

        private static void AddTokens(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

           
            var tempoExpiracaoStr = configuration["Jwt:TempoExpiracao"];
            var chaveAssinatura = configuration["Jwt:ChaveAssinatura"];
            var tempoRefreshStr = configuration["Jwt:TempoRefresh"];


            if (string.IsNullOrEmpty(chaveAssinatura))
                throw new ArgumentException("Chave de assinatura JWT não configurada");

            if (!uint.TryParse(tempoExpiracaoStr, out var tempoExpiracao))
                tempoExpiracao = 1;

            if (!uint.TryParse(tempoRefreshStr, out var tempoRefresh))
                tempoRefresh = 10080; 


            // Registra o generator no DI
            services.AddScoped<IAcessTokenGenerator>(sp => new JwtTokenGenerator(tempoExpiracao, chaveAssinatura));
            services.AddScoped<IAcessTokenValidator>(sp => new JwtTokenValidator(chaveAssinatura));

            services.AddScoped<IRefreshTokenGenerator>(sp =>
        new JwtRefreshTokenGenerator(tempoRefresh, chaveAssinatura));
        }

        private static bool IsUnitTestEnviroment(IConfiguration configuration)
        {
           return configuration.GetValue<bool>("InMemoryTest");
        }


    }

}
