using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWarsha_DTOs.ProductBoughtDTOs
{
    public class ProductBoughtCreateDto
    {
        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        [Range(0, double.MaxValue)]
        public decimal PricePerUnit { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0, double.MaxValue)]
        public decimal Discount { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Count { get; set; }
        public string? Note { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int ProductsRestockingBillId { get; set; }
    }
}