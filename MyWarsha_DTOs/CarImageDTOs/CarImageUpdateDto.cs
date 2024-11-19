using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MyWarsha_DTOs.CarImageDTOs
{
    public class CarImageUpdateDto
    {        
        [Required]
        public IFormFile Image { get; set; } = null!;
    }
}