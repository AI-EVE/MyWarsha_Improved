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
    }
}