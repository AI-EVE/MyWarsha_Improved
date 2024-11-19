using System.ComponentModel.DataAnnotations;

namespace MyWarsha_DTOs.ProductBoughtDTOs
{
    public class ProductBoughtUpdateDto
    {
        [Range(0, int.MaxValue)]
        public decimal? PricePerUnit { get; set; }

        [Range(0, int.MaxValue)]
        public decimal? Discount { get; set; }
        public int? Count { get; set; }

        public bool? IsReturned { get; set; }
        public string? Note { get; set; }
    }
}