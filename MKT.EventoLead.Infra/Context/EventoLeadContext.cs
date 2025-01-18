using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MKT.EventoLead.Domain.Entities;

namespace MKT.EventoLead.Infra.Context
{
    public class EventoLeadContext : DbContext
    {
        private readonly string connectionString;

        public EventoLeadContext(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("EventoLead");
        }

        public DbSet<Lead> Lead { get; set; } = null!;

        public DbSet<Campanha> Campanha { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if (optionBuilder.IsConfigured)
                return;

            optionBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventoLeadContext).Assembly);
        }
    }
}
