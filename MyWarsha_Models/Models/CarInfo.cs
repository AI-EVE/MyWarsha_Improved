using System.ComponentModel.DataAnnotations.Schema;

namespace MyWarsha_Models.Models
{
    public class CarInfo
    {
        public int Id { get; set; }

        [ForeignKey("CarMakerId")]
        public CarMaker CarMaker { get; set; } = null!;
        public int CarMakerId { get; set; }

        [ForeignKey("CarModelId")]        
        public CarModel CarModel { get; set; }  = null!;
        public int CarModelId { get; set; }
        
        [ForeignKey("CarGenerationId")]
        public CarGeneration CarGeneration { get; set; } = null!;
        public int CarGenerationId { get; set; }
        public List<CarInfoProduct> CarInfoProduct { get; set; } = [];
    }
}