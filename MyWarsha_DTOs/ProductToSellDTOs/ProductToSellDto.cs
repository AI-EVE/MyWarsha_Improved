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
        
        public static ProductToSellDto FromProductToSell(ProductToSell productToSell)
        {
            
            
            return new ProductToSellDto
            {
                
                Id = productToSell.Id,
                PricePerUnit = productToSell.PricePerUnit,
                Discount = productToSell.Discount,
                Count = productToSell.Count,
                IsReturned = productToSell.IsReturned,
                Note = productToSell.Note,
                Product = ProductDto.ToProductDto(productToSell.Product),
                TotalPriceAfterDiscount = (productToSell.PricePerUnit * productToSell.Count) - productToSell.Discount
            };
        }        
    }
}