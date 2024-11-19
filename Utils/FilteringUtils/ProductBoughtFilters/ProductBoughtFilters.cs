using System.Linq.Expressions;
using MyWarsha_Models.Models;

namespace Utils.FilteringUtils.ProductBoughtFilters
{
    public class ProductBoughtFilters
    {
        
        public decimal? MinPricePerUnit { get; set; }
        public decimal? MaxPricePerUnit { get; set; }
        public decimal? Discount { get; set; }
        public int? Count { get; set; }
        public bool? IsReturned { get; set; }
        public int? ProductId { get; set; }
        public int? ProductsRestockingBillId { get; set; }

        public Expression<Func<ProductBought, bool>> GetExpression()
        {
            return productBought =>
                (MinPricePerUnit > MaxPricePerUnit || (MinPricePerUnit == null || productBought.PricePerUnit >= MinPricePerUnit) &&
                (MaxPricePerUnit == null || productBought.PricePerUnit <= MaxPricePerUnit)) &&
                (Discount == null || productBought.Discount == Discount) &&
                (Count == null || productBought.Count == Count) &&
                (IsReturned == null || productBought.IsReturned == IsReturned) &&
                (ProductId == null || productBought.ProductId == ProductId) &&
                (ProductsRestockingBillId == null || productBought.ProductsRestockingBillId == ProductsRestockingBillId);
        }
    }
}