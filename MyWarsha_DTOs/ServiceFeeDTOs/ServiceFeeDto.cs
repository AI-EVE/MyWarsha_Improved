using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.ServiceFeeDTOs
{
    public class ServiceFeeDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPriceAfterDiscount { get; set; }
        public bool IsReturned { get; set; } = false;
        public string? Notes { get; set; }
        public int CategoryId { get; set; }
        public int ServiceId { get; set; }
    }
}