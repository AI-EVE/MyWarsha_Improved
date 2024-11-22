using System.Linq.Expressions;
using MyWarsha_DTOs.ProductDTOs;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ProductFilters;
using Utils.PageUtils;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<ProductDto?> Get(int id);
        Task<IEnumerable<ProductDtoMulti>> GetAll(PaginationPropreties paginationPropreties, ProductFilters filters);
        Task<Product?> GetById(int id);
        Task<int> Count(ProductFilters filters);

        Task<string?> GetProductName(int id);
    }
}