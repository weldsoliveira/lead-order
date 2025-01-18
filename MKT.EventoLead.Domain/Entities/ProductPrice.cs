using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MKT.EventoLead.Domain.Entities
{
    public class ProductPrice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdProdPrice { get; set; }

       // [Required]
        public long IdProduct { get; set; }

       // [Required]
       // [MaxLength(10)]
        public string Currency { get; set; }

      //  [MaxLength(100)]
        public string CurrencyDescription { get; set; }

        public decimal Price { get; set; }
        public decimal unitPriceRetail { get; set; }
        
        ///[Required]
        public DateTime DtIns { get; set; }

        public DateTime? DtUpd { get; set; }

        // Relação com Product
        [ForeignKey("IdProduct")]
        public virtual Entities.Product Product { get; set; }
    }
}
