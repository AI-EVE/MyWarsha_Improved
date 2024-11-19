using MyWarsha_DTOs.CarInfoDTOs;
using MyWarsha_DTOs.CategoryDTOs;
using MyWarsha_DTOs.ProductBrandDTOs;
using MyWarsha_DTOs.ProductImageDTOs;
using MyWarsha_DTOs.ProductTypeDTOs;
using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.ProductDTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public CategoryDto Category { get; set; } = null!;
        public ProductTypeDto ProductType { get; set; } = null!;
        public ProductBrandDto ProductBrand { get; set; } = null!;
        public DateOnly DateAdded { get; set; }
        public string? Description { get; set; }
        public decimal ListPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int Stock { get; set; }
        public bool IsAvailable { get; set; }
        public List<CarInfoDto> CarInfos { get; set; } = [];
        public List<ProductImageDto> ProductImages { get; set; } = [];

        public static ProductDto ToProductDto(Product product)
        {
            if (product == null)
            {
                return null!;
            }

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Category = CategoryDto.ToCategoryDto(product.Category),
                ProductType = ProductTypeDto.ToProductTypeDto(product.ProductType),
                ProductBrand = ProductBrandDto.ToProductBrandDto(product.ProductBrand),
                DateAdded = product.DateAdded,
                Description = product.Description,
                ListPrice = product.ListPrice,
                SalePrice = product.SalePrice,
                Stock = product.Stock,
                IsAvailable = product.IsAvailable,
                CarInfos = product.CarInfoProduct.Select(cip => CarInfoDto.ToCarInfoDto(cip.CarInfo)).ToList(),
                ProductImages = product.ProductImages.Select(pi => ProductImageDto.ToProductImageDto(pi)).ToList()
            };
        }
    }
}