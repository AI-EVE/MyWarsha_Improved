using System.ComponentModel.DataAnnotations;

namespace MyWarsha_DTOs.CarInfoDTOs
{
    public class CarInfoCreateDto
    {
        [Required]
        public int CarMakerId { get; set; }
        [Required]
        public int CarModelId { get; set; }
        [Required]
        public int CarGenerationId { get; set; }
    }
}