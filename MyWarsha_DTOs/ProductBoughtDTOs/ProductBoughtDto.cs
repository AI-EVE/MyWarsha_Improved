using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.ProductBoughtDTOs
{
    public class ProductBoughtDto
    {
        public int Id { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal Discount { get; set; }
        public int Count { get; set; }
        public bool IsReturned { get; set; }
        public string? Note { get; set; }
        public decimal TotalPriceAfterDiscount { get; set; }
        public string? productName { get; set; }
        public int ProductId { get; set; }
        public int ProductsRestockingBillId { get; set; }
    }
}