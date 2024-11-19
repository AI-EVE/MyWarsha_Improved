using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.ProductsRestockingBillDTOs
{
    public class ProductsRestockingBillDtoMulti
    {
        public int Id { get; set; }

        public string ShopName { get; set; } = null!;
   
        public string DateOfOrder { get; set; } = null!;

        public static ProductsRestockingBillDtoMulti ToProductsRestockingBillDtoMulti(ProductsRestockingBill productsRestockingBill)
        {
            return new ProductsRestockingBillDtoMulti
            {
                Id = productsRestockingBill.Id,
                ShopName = productsRestockingBill.ShopName,
                DateOfOrder = productsRestockingBill.DateOfOrder.ToString("yyyy-MM-dd")
            };
        }
    }
}