using System.Linq.Expressions;
using MyWarsha_DTOs.ServiceFeeDTOs;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IServiceFeeRepository : IRepository<ServiceFee>
    {
        Task<IEnumerable<ServiceFeeDto>> GetAll(Expression<Func<ServiceFee, bool>> predicate, PaginationPropreties paginationPropreties);
        Task<ServiceFeeDto?> Get(Expression<Func<ServiceFee, bool>> predicate);
        Task<ServiceFee?> GetById(int id);
        Task<int> GetCount(Expression<Func<ServiceFee, bool>> predicate);
    }
}