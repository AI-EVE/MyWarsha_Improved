namespace MyWarsha_DTOs.ProductDTOs
{
    public class ProductUpdateDto
    {
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public string? Description { get; set; }
        public decimal? ListPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public int? Stock { get; set; }
        public bool? IsAvailable { get; set; }
    }
}