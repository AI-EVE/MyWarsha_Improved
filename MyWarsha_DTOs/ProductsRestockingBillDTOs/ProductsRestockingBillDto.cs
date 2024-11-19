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

        public static ProductsRestockingBillDto ToProductsRestockingBillDto(ProductsRestockingBill productsRestockingBill)
        {
            return new ProductsRestockingBillDto
            {
                Id = productsRestockingBill.Id,
                ShopName = productsRestockingBill.ShopName,
                DateOfOrder = productsRestockingBill.DateOfOrder.ToString("yyyy-MM-dd"),
                ProductsBought = productsRestockingBill.ProductsBought.Select(ProductBoughtDto.ToProductBoughtDto).ToList()
            };
        }
    }
}