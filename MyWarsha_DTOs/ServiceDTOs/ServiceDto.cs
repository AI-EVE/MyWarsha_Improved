using MyWarsha_DTOs.CarDTOs;
using MyWarsha_DTOs.ClientDTOs;
using MyWarsha_DTOs.ProductToSellDTOs;
using MyWarsha_DTOs.ServiceFeeDTOs;
using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.ServiceDTOs
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public string Date { get; set; } = null!;
        public decimal TotalPriceAfterDiscount { get; set; }
        public ClientDtoForService Client { get; set; } = null!;
        public CarDto Car { get; set; } = null!;
        public ServiceStatus Status { get; set; } = null!;
        public string? Note { get; set; }
        public List<ProductToSellDto> ProductsToSell { get; set; } = [];
        public List<ServiceFeeDto> ServiceFees { get; set; } = [];

        public static ServiceDto ToServiceDto(Service service)
        {
            return new ServiceDto
            {
                Id = service.Id,
                Date = service.Date.ToString("yyyy-MM-dd"),
                Client = ClientDtoForService.ToClientDtoForService(service.Client),
                Car = CarDto.ToCarDto(service.Car),
                Status = service.Status,
                Note = service.Note,
                ProductsToSell = service.ProductsToSell.Select(ProductToSellDto.FromProductToSell).ToList(),
                ServiceFees = service.ServiceFees.Select(ServiceFeeDto.FromServiceFee).ToList()
            };
        }
    }
}