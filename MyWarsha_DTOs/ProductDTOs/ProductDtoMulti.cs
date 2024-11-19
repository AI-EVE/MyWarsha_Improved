using MyWarsha_DTOs.ProductImageDTOs;
using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.ProductDTOs
{
    public class ProductDtoMulti
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public DateOnly DateAdded { get; set; }
        public string? Description { get; set; }
        public decimal ListPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int Stock { get; set; }
        public bool IsAvailable { get; set; }
        public ProductImageDto? MainProductImage { get; set; }

        public static ProductDtoMulti ToProductDtoMulti(Product product)
        {
            return new ProductDtoMulti
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                Name = product.Name,
                DateAdded = product.DateAdded,
                Description = product.Description,
                ListPrice = product.ListPrice,
                SalePrice = product.SalePrice,
                Stock = product.Stock,
                IsAvailable = product.IsAvailable,
                MainProductImage = product.ProductImages.Count > 0 ? product.ProductImages.Where(pi => pi.IsMain).Select(pi => ProductImageDto.ToProductImageDto(pi)).FirstOrDefault() : null
            };
        }
    }
}