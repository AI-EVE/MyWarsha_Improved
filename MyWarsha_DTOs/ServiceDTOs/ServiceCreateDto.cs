using System.ComponentModel.DataAnnotations;
using MyWarsha_DTOs.ProductToSellDTOs;
using MyWarsha_DTOs.ServiceFeeDTOs;

namespace MyWarsha_DTOs.ServiceDTOs
{
    public class ServiceCreateDto
    {

        [Required]
        public int ClientId { get; set; }
        [Required]
        public int CarId { get; set; }
        [Required]
        public int ServiceStatusId { get; set; } = 1;
        public string? KmCount { get; set; }
        public string? Note { get; set; }

        [Required]
        public List<ProductToSellCreateDto> ProductsToSell { get; set; } = [];
        [Required]
        public List<ServiceFeeCreateDto> ServiceFees { get; set; } = [];
    }
}