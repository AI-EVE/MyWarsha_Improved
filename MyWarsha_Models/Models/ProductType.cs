using System.ComponentModel.DataAnnotations;

namespace MyWarsha_Models.Models
{
    public class ProductType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}