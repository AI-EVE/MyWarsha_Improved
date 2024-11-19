using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWarsha_Models.Models
{
    public class CarImage
    {
        public int Id { get; set; }
        [Required]
        public string ImagePath { get; set; } = null!;
        [Required]
        public bool IsMain { get; set; }
        
        [ForeignKey("CarId")]
        public Car Car { get; set; } = null!;
        public int CarId { get; set; }
    }
}