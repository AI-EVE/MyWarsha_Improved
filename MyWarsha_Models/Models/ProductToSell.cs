using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWarsha_Models.Models
{
    public class ProductToSell
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0, int.MaxValue)]
        [Required]
        public decimal PricePerUnit { get; set; }
        [Range(0, int.MaxValue)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount { get; set; }

        [Range(1, int.MaxValue)]
        [Required]
        public int Count { get; set; }
        public bool IsReturned { get; set; } = false;
        
        public string? Note { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
        public int ProductId { get; set; }

        [ForeignKey("ServiceId")]
        public Service Service { get; set; } = null!;
        public int ServiceId { get; set; }
    }
}