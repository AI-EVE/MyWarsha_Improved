using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.ProductImageDTOs
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool IsMain { get; set; }
        public int ProductId { get; set; }


        public static ProductImageDto ToProductImageDto(ProductImage productImage)
        {
            return new ProductImageDto
            {
                Id = productImage.Id,
                ImageUrl = productImage.ImageUrl,
                IsMain = productImage.IsMain,
                ProductId = productImage.ProductId
            };
        }        
    }
}