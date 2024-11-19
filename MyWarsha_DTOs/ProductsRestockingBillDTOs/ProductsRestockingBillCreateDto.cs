using System.ComponentModel.DataAnnotations;

namespace MyWarsha_DTOs.ProductsRestockingBillDTOs
{
    public class ProductsRestockingBillCreateDto
    {
        [Required]
        public string ShopName { get; set; } = null!;
    }
}