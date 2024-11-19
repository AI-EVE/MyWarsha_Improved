using System.ComponentModel.DataAnnotations;

namespace MyWarsha_DTOs.CarModelDTOs
{
    public class CarModelCreateDto
    {
        [Required]
        public string Name { get; set; } = null!;
        
        [Required]
        public int CarMakerId { get; set; }
        public string? Notes { get; set; }
    }
}