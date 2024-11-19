using System.Linq.Expressions;
using MyWarsha_Models.Models;

namespace Utils.FilteringUtils.ProductFilters
{
    public class ProductFilters
    {
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public int? ProductTypeId { get; set; }
        public int? ProductBrandId { get; set; }
        public bool? IsAvailable { get; set; }

        public Expression<Func<Product, bool>> GetExpression()
        {
            return product =>
                (Name == null || product.Name.Contains(Name)) &&
                (CategoryId == null || product.CategoryId == CategoryId) &&
                (ProductTypeId == null || product.ProductTypeId == ProductTypeId) &&
                (ProductBrandId == null || product.ProductBrandId == ProductBrandId) &&
                (IsAvailable == null || product.IsAvailable == IsAvailable);
        }
    }
}