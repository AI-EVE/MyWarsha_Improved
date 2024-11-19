using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWarsha_Models.Models
{
    public class CarGeneration
    {
        public int Id { get; set; }   
       
        [Required]
        public string Name { get; set; } = null!;
        public string? Notes { get; set; }

        [ForeignKey("CarModelId")]
        public CarModel CarModel { get; set; } = null!;
        public int CarModelId { get; set; }
    }
}