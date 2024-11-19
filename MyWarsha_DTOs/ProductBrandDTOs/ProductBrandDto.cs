using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.ProductBrandDTOs
{
    public class ProductBrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public static ProductBrandDto ToProductBrandDto(ProductBrand productBrand)
        {
            return new ProductBrandDto
            {
                Id = productBrand.Id,
                Name = productBrand.Name
            };
        }
    }
}