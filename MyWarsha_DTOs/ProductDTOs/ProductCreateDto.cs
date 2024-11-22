using System.ComponentModel.DataAnnotations;

namespace MyWarsha_DTOs.ProductDTOs
{
    public class ProductCreateDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int ProductTypeId { get; set; }
        [Required]
        public int ProductBrandId { get; set; }
        public string? Description { get; set; }
        [Required]
        public decimal ListPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int Stock { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}