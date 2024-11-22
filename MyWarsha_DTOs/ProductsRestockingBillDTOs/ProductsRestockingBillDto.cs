using MyWarsha_DTOs.ProductBoughtDTOs;
using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.ProductsRestockingBillDTOs
{
    public class ProductsRestockingBillDto
    {
        public int Id { get; set; }

        public string ShopName { get; set; } = null!;

        public string DateOfOrder { get; set; } = null!;


        public List<ProductBoughtDto> ProductsBought { get; set; } = [];
    }
}