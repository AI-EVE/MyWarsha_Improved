using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MyWarsha_DTOs.CarImageDTOs
{
    public class CarImageCreateDto
    {
        [Required]
        public IFormFile Image { get; set; } = null!;
        [Required]
        public int CarId { get; set; }
        
        public bool IsMain { get; set; }
    }
}