using System.ComponentModel.DataAnnotations;

namespace MyWarsha_DTOs.CarDTOs
{
    public class CarUpdateDto
    {
        public string? Color { get; set; }
        public int? CarInfoId { get; set; }
        public string? PlateNumber { get; set; } = null!;
        public string? ChassisNumber { get; set; }
        public string? MotorNumber { get; set; }
        public string? Notes { get; set; }
    }
}