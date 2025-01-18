using MKT.EventoLead.Domain.Entities;
using MKT.EventoLead.Domain.Interfaces.Repository;
using MKT.EventoLead.Infra.Context;

namespace MKT.EventoLead.Infra.Repository
{
    public class LeadRepository : ILeadRepository
    {
        private readonly EventoLeadContext context;

        public LeadRepository(EventoLeadContext context)
        {
            this.context = context;
        }

        public void Insert(Lead lead)
        {
            try
            {
                context.Set<Lead>().Add(lead);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir lead: {ex.Message}");
            }
        }
    }
}
