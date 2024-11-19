using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MyWarsha_DTOs.CarMakerDtos
{
    public class CarMakerCreateDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Notes { get; set; }
        public IFormFile? Logo { get; set; }
    }
}