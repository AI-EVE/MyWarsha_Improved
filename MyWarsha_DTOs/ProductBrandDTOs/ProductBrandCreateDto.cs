using System.ComponentModel.DataAnnotations;

namespace MyWarsha_DTOs.ProductBrandDTOs
{
    public class ProductBrandCreateDto
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}