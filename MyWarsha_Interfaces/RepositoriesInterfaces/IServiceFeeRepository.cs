using System.Linq.Expressions;
using MyWarsha_DTOs.ServiceFeeDTOs;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ServiceFreeFilters;
using Utils.PageUtils;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IServiceFeeRepository : IRepository<ServiceFee>
    {
        Task<IEnumerable<ServiceFeeDto>> GetAll(ServiceFeeFilters filters, PaginationPropreties paginationPropreties);
        Task<ServiceFeeDto?> Get(int id);
        Task<ServiceFee?> GetById(int id);
        Task<int> GetCount(ServiceFeeFilters filters);
    }
}