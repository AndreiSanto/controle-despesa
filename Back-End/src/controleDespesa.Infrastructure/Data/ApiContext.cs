using controleDespesa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Infrastructure.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Receita> Receitas { get; set; }
        public DbSet<TipoDespesaReceita> TipoDespesaReceitas { get; set; }
        public DbSet<DespesaParcela> DespesaParcelas { get; set; }
        public DbSet<Despesa> Despesas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiContext).Assembly);
        }

    }
}
