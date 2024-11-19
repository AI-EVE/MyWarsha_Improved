using System.Linq.Expressions;
using MyWarsha_Models.Models;

namespace Utils.FilteringUtils.ProductsRestockingBillFilters
{
    public class ProductsRestockingBillFilters
    {
        public string? ShopName { get; set; }
        public string? DateOfOrderFrom { get; set; }
        public string? DateOfOrderTo { get; set; }
        public decimal? MinTotalPrice { get; set; }
        public decimal? MaxTotalPrice { get; set; }

        public Expression<Func<ProductsRestockingBill, bool>> ToExpression()
        {

            var IsDateOfOrderFromValid = DateOnly.TryParse(DateOfOrderFrom, out DateOnly dateOfOrderFrom);
            var IsDateOfOrderToValid = DateOnly.TryParse(DateOfOrderTo, out DateOnly dateOfOrderTo);
            var dateToLargerThanDateFrom = dateOfOrderTo >= dateOfOrderFrom;

                return productsRestockingBill =>
                (ShopName == null || productsRestockingBill.ShopName.Contains(ShopName)) &&
                (!IsDateOfOrderFromValid || productsRestockingBill.DateOfOrder >= dateOfOrderFrom) &&
                (!IsDateOfOrderToValid || productsRestockingBill.DateOfOrder <= dateOfOrderTo) &&
                (!IsDateOfOrderFromValid || !IsDateOfOrderToValid || dateToLargerThanDateFrom) &&
                (MinTotalPrice > MaxTotalPrice || (MinTotalPrice == null || productsRestockingBill.ProductsBought.Select(x => (x.PricePerUnit * x.Count
                ) - x.Discount).Sum() >= MinTotalPrice) &&
                (MaxTotalPrice == null || productsRestockingBill.ProductsBought.Select(x => (x.PricePerUnit * x.Count
                ) - x.Discount).Sum() <= MaxTotalPrice));
            
        }
    }
}