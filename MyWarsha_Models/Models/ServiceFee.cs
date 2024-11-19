using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWarsha_Models.Models
{
    public class ServiceFee
    {
        public int Id { get; set; }


        [Required]
        [Range(0, int.MaxValue)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount { get; set; }
        public bool IsReturned { get; set; } = false;
        public string? Notes { get; set; }
        
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;
        public int CategoryId { get; set; }

        [ForeignKey("ServiceId")]
        public Service Service { get; set; } = null!;
        public int ServiceId { get; set; }
    }
}