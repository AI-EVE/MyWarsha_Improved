using System.Linq.Expressions;
using MyWarsha_DTOs.ServiceDTOs;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ServiceFilters;
using Utils.PageUtils;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<IEnumerable<ServiceDto>> GetAll(PaginationPropreties paginationPropreties, ServiceFilters filters, decimal? minPrice, decimal? maxPrice);
        Task<ServiceDto?> Get(int id);
        Task<Service?> GetById(int id);
        Task<int> GetCount(ServiceFilters filters, decimal? minPrice, decimal? maxPrice);
    }
}