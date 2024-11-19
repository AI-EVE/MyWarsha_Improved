using System.ComponentModel.DataAnnotations;

namespace MyWarsha_Models.Models
{
    public class ProductBrand
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}