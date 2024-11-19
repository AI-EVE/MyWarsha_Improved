using System.ComponentModel.DataAnnotations;

namespace MyWarsha_Models.Models
{
    public class ProductsRestockingBill
    {
        public int Id { get; set; }

        [Required]
        public string ShopName { get; set; } = null!;
   
        [Required]
        public DateOnly DateOfOrder { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public List<ProductBought> ProductsBought { get; set; } = [];
    }
}