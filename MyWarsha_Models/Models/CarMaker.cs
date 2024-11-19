using System.ComponentModel.DataAnnotations;

namespace MyWarsha_Models.Models
{
    public class CarMaker
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public List<CarModel> CarModels { get; set; } = [];
        public string? Notes { get; set; }
        public string? Logo { get; set; }

       
    }
}