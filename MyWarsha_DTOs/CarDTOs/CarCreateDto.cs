using System.ComponentModel.DataAnnotations;

namespace MyWarsha_DTOs.CarDTOs
{
    public class CarCreateDto
    {
        public string? Color { get; set; }
        public string PlateNumber { get; set; } = null!;
        public string? ChassisNumber { get; set; }
        public string? MotorNumber { get; set; }
        public string? Notes { get; set; }
        [Required]   
        public int ClientId { get; set; }
        [Required]
        public int CarInfoId { get; set; }
    }
}