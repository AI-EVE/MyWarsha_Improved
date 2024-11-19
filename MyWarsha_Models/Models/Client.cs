using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWarsha_Models.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [EmailAddress]
        public string? Email { get; set; }
        [NotMapped]
        public decimal TotalPaid { get; set; }
        [NotMapped]
        public decimal TotalUnpaid { get; set; }
        public List<Car> Cars  { get; set; } = [];
        public List<Phone> Phones { get; set; } = [];
    }
}