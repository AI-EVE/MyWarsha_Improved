using System.Linq.Expressions;
using MyWarsha_DTOs.ProductDTOs;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ProductFilters;
using Utils.PageUtils;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<ProductDto?> Get(Expression<Func<Product, bool>> predicate);
        Task<IEnumerable<ProductDtoMulti>> GetAll(PaginationPropreties paginationPropreties, Expression<Func<Product, bool>> predicate);
        Task<Product?> GetById(int id);
        Task<int> Count(Expression<Func<Product, bool>> predicate);
        Task<int> Stock(int id);
        Task<string?> GetProductName(int id);
    }
}