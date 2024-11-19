using System.ComponentModel.DataAnnotations;

namespace MyWarsha_DTOs.CarGenerationDtos
{
    public class CarGenerationCreateDto
    {
       
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int CarModelId { get; set; }
        public string? Notes { get; set; }
    }
}