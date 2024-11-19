using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWarsha_Models.Models
{
    public class ProductBought
    {
        public int Id { get; set; }

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
        public bool IsReturned { get; set; } = false;
        public string? Note { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
        public int ProductId { get; set; }

        [ForeignKey("ProductsRestockingBillId")]
        public ProductsRestockingBill ProductsRestockingBill { get; set; } = null!;
        public int ProductsRestockingBillId { get; set; }
    }
}