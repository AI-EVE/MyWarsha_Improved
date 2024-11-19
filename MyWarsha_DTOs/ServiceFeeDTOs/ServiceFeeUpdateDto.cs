using System.ComponentModel.DataAnnotations;

namespace MyWarsha_DTOs.ServiceFeeDTOs
{
    public class ServiceFeeUpdateDto
    {

        [Range(0, int.MaxValue)]
        public decimal? Price { get; set; }

        [Range(0, int.MaxValue)]
        public decimal? Discount { get; set; }
        public bool? IsReturned { get; set; }
        public string? Notes { get; set; }
        public int? CategoryId { get; set; }
    }
}