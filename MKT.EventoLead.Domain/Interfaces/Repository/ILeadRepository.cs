using MKT.EventoLead.Domain.Entities;

namespace MKT.EventoLead.Domain.Interfaces.Repository
{
    public interface ILeadRepository
    {
        public void Insert(Lead lead);
    }
}
