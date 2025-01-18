using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MKT.EventoLead.Domain.Entities;
using MKT.EventoLead.Domain.Interfaces.Repository;
using MKT.EventoLead.Infra.Context;

namespace MKT.EventoLead.Infra.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly B2BEuropeContext context;

        public ProductRepository(B2BEuropeContext context)
        {
            this.context = context;
        }

        public List<Domain.Entities.ProductPrice> GetAllProduct(string currency)
        {
            List<Domain.Entities.ProductPrice> productPrices = new List<Domain.Entities.ProductPrice>();
            try
            {
                productPrices = context.ProductPrice
                     .Include(pp => pp.Product) 
                    .Where(x=> x.Currency.Trim().ToUpper() == currency.Trim().ToUpper() && x.Price > 0).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir lead: {ex.Message}");

            }
            return productPrices;
        }

        public ProductPrice GetByIdProduct(long id)
        {
            var productprice = context.ProductPrice
                   .Include(pp => pp.Product)
                  .Where(x => x.IdProduct == id).FirstOrDefault();

            return productprice;
        }
    }
}
