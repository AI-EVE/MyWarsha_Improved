using MyWarsha_DTOs.ProductDTOs;
using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.ProductToSellDTOs
{
    public class ProductToSellDto
    {
        public int Id { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal Discount { get; set; }
        public int Count { get; set; }
        public decimal TotalPriceAfterDiscount { get; set; }
        public bool IsReturned { get; set; }
        public string? Note { get; set; }
        public ProductDto Product { get; set; } = null!;
    }
}