using System.ComponentModel.DataAnnotations.Schema;

namespace MyWarsha_Models.Models
{
    public class Service
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [ForeignKey("ClientId")]
        public Client Client { get; set; } = null!;
        public int ClientId { get; set; }

        [ForeignKey("CarId")]
        public Car Car { get; set; } = null!;
        public int CarId { get; set; }

        [ForeignKey("ServiceStatusId")]
        public ServiceStatus Status { get; set; } = null!;
        public int ServiceStatusId { get; set; } = 1;

        public string? Note { get; set; }

        public List<ProductToSell> ProductsToSell { get; set; } = [];

        public List<ServiceFee> ServiceFees { get; set; } = [];
    }
}