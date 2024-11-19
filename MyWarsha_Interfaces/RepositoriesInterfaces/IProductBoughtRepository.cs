using System.Linq.Expressions;
using MyWarsha_DTOs.ProductBoughtDTOs;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IProductBoughtRepository : IRepository<ProductBought>
    {
        Task<ProductBoughtDto?> Get(Expression<Func<ProductBought, bool>> predicate);
        Task<IEnumerable<ProductBoughtDto>> GetAll(PaginationPropreties paginationPropreties, Expression<Func<ProductBought, bool>> predicate);
        Task<ProductBought?> GetById(int id);
        Task<int> GetCount(Expression<Func<ProductBought, bool>> predicate);
        Task RemoveRangeAsync();
    }
}