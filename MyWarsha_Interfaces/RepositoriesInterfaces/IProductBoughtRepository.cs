using System.Linq.Expressions;
using MyWarsha_DTOs.ProductBoughtDTOs;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ProductBoughtFilters;
using Utils.PageUtils;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IProductBoughtRepository : IRepository<ProductBought>
    {
        Task<ProductBoughtDto?> Get(int id);
        Task<IEnumerable<ProductBoughtDto>> GetAll(PaginationPropreties paginationPropreties, ProductBoughtFilters filters);
        Task<ProductBought?> GetById(int id);
        Task<int> GetCount(ProductBoughtFilters filters);
        Task RemoveRangeAsync();
    }
}