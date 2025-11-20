using AutoMapper;
using controleDespesa.API.Token;
using controleDespesa.Application.Extension;
using controleDespesa.Application.Service;
using controleDespesa.Application.Service.Cryptografia;
using controleDespesa.Domain.Security.Tokens;
using controleDespesa.Infrastructure;
using controleDespesa.Infrastructure.Extension;
using controleDespesa.Infrastructure.Jobs;
using controleDespesa.Infrastructure.Migration;
using Hangfire;
using Hangfire.MySql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using Serilog;
using System.Text;
using DepedenciaInjecaoExtension = controleDespesa.Application.Extension.DepedenciaInjecaoExtension;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
         Enter 'Bearer' [space] and then your token in the text input below.
         Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"

    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header
        },
        new List<string>()
    }
});

});

builder.Services.AddHangfire(config =>
    config.UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseStorage(
              new MySqlStorage(
                  builder.Configuration.GetConnectionString("DefaultConnection"),
                  new MySqlStorageOptions
                  {
                      TablesPrefix = "Hangfire_",          
                      QueuePollInterval = TimeSpan.FromSeconds(15)
                  }
              )
          )
);
builder.Services.AddHangfireServer();






builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = TokenHelpers.GetTokenValidationParameters(builder.Configuration);
    });

builder.Services.AddAuthorization();



Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .WriteTo.File(
        "logs/error-.txt",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error,
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddMemoryCache();


builder.Host.UseSerilog(); // usa o Serilog como logger principal

builder.Services.AddScoped<ITokenProvider, HttpContextValue> ();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.AllowAnyOrigin()
      .AllowAnyHeader()
      .AllowAnyMethod();
    });
});

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseHangfireDashboard("/hangfire");


app.MapControllers();

MigrateDatabase();

using (var scope = app.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    DepedenciaInjecaoExtension.ConfigureJobs(recurringJobManager);
}



app.Run();

void MigrateDatabase()
{

    var conexao = builder.Configuration.GetConnectionString("DefaultConnection");
    DatabaseMigration.Migrate(conexao);

}

public partial class Program { }
