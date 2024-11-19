using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MyWarsha_DTOs.ProductImageDTOs
{
    public class ProductImageCreateDto
    {
        [Required]
        public IFormFile Image { get; set; } = null!;
        [Required]
        public int ProductId { get; set; }
        
        public bool IsMain { get; set; }
    }
}