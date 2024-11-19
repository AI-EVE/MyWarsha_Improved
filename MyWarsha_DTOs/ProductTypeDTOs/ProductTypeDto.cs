using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.ProductTypeDTOs
{
    public class ProductTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public static ProductTypeDto ToProductTypeDto(ProductType productType)
        {
            return new ProductTypeDto
            {
                Id = productType.Id,
                Name = productType.Name
            };
        }
    }
}