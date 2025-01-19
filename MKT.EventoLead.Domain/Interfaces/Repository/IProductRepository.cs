using MKT.EventoLead.Domain.Entities;

namespace MKT.EventoLead.Domain.Interfaces.Repository
{
    public interface IProductRepository
    {
        public List<Entities.ProductPrice> GetAllProduct(string currency);
        public Entities.ProductPrice GetByIdProduct(long id, string currency);
    }
}
