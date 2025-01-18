using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MKT.EventoLead.Domain.Entities;

namespace MKT.EventoLead.Infra.Context
{
    public class B2BEuropeContext : DbContext
    {
        private readonly string connectionString;

        public B2BEuropeContext(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("B2BEurope");
        }

        public DbSet<Product> Product { get; set; } = null!;
        public DbSet<ProductPrice> ProductPrice { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if (optionBuilder.IsConfigured)
                return;

            optionBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(B2BEuropeContext).Assembly);
        }
    }
}
