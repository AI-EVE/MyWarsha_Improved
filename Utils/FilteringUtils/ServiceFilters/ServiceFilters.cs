using System.Linq.Expressions;
using MyWarsha_Models.Models;

namespace Utils.FilteringUtils.ServiceFilters
{
    public class ServiceFilters
    {
        public string? DateFrom { get; set; }
        public string? DateTo { get; set; }
        public int? ClientId { get; set; }
        public int? CarId { get; set; }
        public int? ServiceStatusId { get; set; }

        public Expression<Func<Service, bool>> ToExpression()
        {

            var IsDateFromValid = DateOnly.TryParse(DateFrom, out DateOnly dateFrom);
            var IsDateToValid = DateOnly.TryParse(DateTo, out DateOnly dateTo);
            var dateToLargerThanDateFrom = dateTo >= dateFrom;

            return service =>
                (!IsDateFromValid || service.Date >= dateFrom) &&
                (!IsDateToValid || service.Date <= dateTo) &&
                (!IsDateFromValid || !IsDateToValid || dateToLargerThanDateFrom) &&
                (ClientId == null || service.ClientId == ClientId) &&
                (CarId == null || service.CarId == CarId) &&
                (ServiceStatusId == null || service.ServiceStatusId == ServiceStatusId);
            
        }
    }
}