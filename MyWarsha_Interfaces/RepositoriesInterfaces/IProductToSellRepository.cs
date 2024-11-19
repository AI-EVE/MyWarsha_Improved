using System.Linq.Expressions;
using MyWarsha_DTOs.ProductToSellDTOs;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IProductToSellRepository : IRepository<ProductToSell>
    {
        
        Task<IEnumerable<ProductToSellDto>> GetAll(Expression<Func<ProductToSell, bool>> predicate, PaginationPropreties paginationPropreties);

        Task<ProductToSellDto?> Get(Expression<Func<ProductToSell, bool>> predicate);

        Task<ProductToSell?> GetById(int id);

        Task<int> GetCount(Expression<Func<ProductToSell, bool>> predicate);
    }
}