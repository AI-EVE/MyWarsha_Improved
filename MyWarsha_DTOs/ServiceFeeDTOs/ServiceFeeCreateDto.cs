using System.ComponentModel.DataAnnotations;

namespace MyWarsha_DTOs.ServiceFeeDTOs
{
    public class ServiceFeeCreateDto
    {

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public decimal Discount { get; set; }

        public string? Notes { get; set; }
        
        [Required]
        public int ServiceId { get; set; }
    }
}