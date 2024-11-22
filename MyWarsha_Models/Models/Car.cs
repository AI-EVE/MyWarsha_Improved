using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWarsha_Models.Models
{
    public class Car
    {
        public int Id { get; set; }

        public string? Color { get; set; }
        [Required]
        public string PlateNumber { get; set; } = null!;
        public string? ChassisNumber { get; set; }
        public string? MotorNumber { get; set; }
        public string? Notes { get; set; }
        public List<CarImage> CarImages { get; set; } = [];

        public List<Service> Services { get; set; } = [];

        [ForeignKey("ClientId")]
        public Client Client { get; set; } = null!;
        public int ClientId { get; set; }

        [ForeignKey("CarGenerationId")]
        public CarGeneration CarGeneration { get; set; } = null!;
        public int CarGenerationId { get; set; }
    }
}