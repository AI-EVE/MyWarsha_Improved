using System.Linq.Expressions;
using MyWarsha_DTOs.ServiceDTOs;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<IEnumerable<ServiceDto>> GetAll(PaginationPropreties paginationPropreties, Expression<Func<Service, bool>> predicate, decimal? minPrice, decimal? maxPrice);
        Task<ServiceDto?> Get(Expression<Func<Service, bool>> predicate);
        Task<Service?> GetById(int id);
        Task<int> GetCount(Expression<Func<Service, bool>> predicate);
    }
}