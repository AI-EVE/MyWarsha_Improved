using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using MyWarsha_Models.Models;

namespace Utils.FilteringUtils.ProductToSellFilters
{
    public class ProductToSellFilters
    {
        [Range(0, int.MaxValue)]
        public decimal? PricePerUnit { get; set; }
        [Range(0, int.MaxValue)]
        public decimal? Discount { get; set; }

        [Range(1, int.MaxValue)]
        public int? Count { get; set; }
        public bool? IsReturned { get; set; }
        public string? Note { get; set; }
        public int? ProductId { get; set; }
        public int? ServiceId { get; set; }

        public Expression<Func<ProductToSell, bool>> GetExpression()
        {
            return productToSell =>
                (PricePerUnit == null || productToSell.PricePerUnit == PricePerUnit) &&
                (Discount == null || productToSell.Discount == Discount) &&
                (Count == null || productToSell.Count == Count) &&
                (IsReturned == null || productToSell.IsReturned == IsReturned) &&
                (Note == null || productToSell.Note == Note) &&
                (ProductId == null || productToSell.ProductId == ProductId) &&
                (ServiceId == null || productToSell.ServiceId == ServiceId);
        }
    }
}