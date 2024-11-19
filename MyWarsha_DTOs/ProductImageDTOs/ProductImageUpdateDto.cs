using System.ComponentModel.DataAnnotations;

namespace MyWarsha_DTOs.ProductImageDTOs
{
    public class ProductImageUpdateDto
    {
        [Required]
        public int Id { get; set; }
        public bool IsMain { get; set; }
    }
}