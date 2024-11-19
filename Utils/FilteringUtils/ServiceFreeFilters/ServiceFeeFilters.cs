using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using MyWarsha_Models.Models;

namespace Utils.FilteringUtils.ServiceFreeFilters
{
    public class ServiceFeeFilters
    {
        [Range(0, int.MaxValue)]
        public decimal? Price { get; set; }

        [Range(0, int.MaxValue)]
        public decimal? Discount { get; set; }
        public bool? IsReturned { get; set; }
        public string? Notes { get; set; }
        public int? CategoryId { get; set; }
        public int? ServiceId { get; set; }

        public Expression<Func<ServiceFee, bool>> GetExpression()
        {
            return serviceFee =>
                (Price == null || serviceFee.Price == Price) &&
                (Discount == null || serviceFee.Discount == Discount) &&
                (IsReturned == null || serviceFee.IsReturned == IsReturned) &&
                (Notes == null || serviceFee.Notes == Notes) &&
                (CategoryId == null || serviceFee.CategoryId == CategoryId) &&
                (ServiceId == null || serviceFee.ServiceId == ServiceId);
        }
    }
}