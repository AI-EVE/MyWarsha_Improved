using System.ComponentModel.DataAnnotations;

namespace MyWarsha_DTOs.ProductToSellDTOs
{
    public class ProductToSellCreateDto
    {
        [Range(0, int.MaxValue)]
        [Required]
        public decimal PricePerUnit { get; set; }

        [Range(0, int.MaxValue)]
        public decimal Discount { get; set; }

        [Range(1, int.MaxValue)]
        [Required]
        public int Count { get; set; }
        public string? Note { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int ServiceId { get; set; }
    }
}