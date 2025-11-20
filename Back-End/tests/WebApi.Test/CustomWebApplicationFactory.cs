using controleDespesa.Domain.Entities;
using controleDespesa.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(service =>
                {

                    var descriptor = service.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApiContext>));
                    if (descriptor is not null)
                        service.Remove(descriptor);

                    var provider = service.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                    service.AddDbContext<ApiContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        
                        options.UseInternalServiceProvider(provider);


                    });

                    
                    using (var scope = service.BuildServiceProvider().CreateScope())
                    {
                        var db = scope.ServiceProvider.GetRequiredService<ApiContext>();
                        db.Database.EnsureCreated();
                        DatabaseSeeder.Seed(db);
                    }
                });
        }

       
        }
    }

