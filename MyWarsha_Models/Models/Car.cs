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
        
        [ForeignKey("ClientId")]
        public Client Client { get; set; } = null!;
        public int ClientId { get; set; }

        [ForeignKey("CarInfoId")]
        public CarInfo CarInfo { get; set; } = null!;
        public int CarInfoId { get; set; }
    }
}