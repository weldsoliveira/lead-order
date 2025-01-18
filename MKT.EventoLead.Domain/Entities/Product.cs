using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MKT.EventoLead.Domain.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idProduct { get; set; }

        public string sku { get; set; } = string.Empty;
        public string description { get; set; }
        public string setDiscount {  get; set; }
    }
}
