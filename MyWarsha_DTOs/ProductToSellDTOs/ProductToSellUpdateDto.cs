using System.ComponentModel.DataAnnotations;

namespace MyWarsha_DTOs.ProductToSellDTOs
{
    public class ProductToSellUpdateDto
    {
        [Range(0, int.MaxValue)]
        public decimal? PricePerUnit { get; set; }

        [Range(0, int.MaxValue)]
        public decimal? Discount { get; set; }

        [Range(1, int.MaxValue)]
        public int? Count { get; set; }
        public bool? IsReturned { get; set; }

        public string? Note { get; set; }

    }
}