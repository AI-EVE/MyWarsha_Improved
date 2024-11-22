using MyWarsha_DTOs.ProductToSellDTOs;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ProductToSellFilters;
using Utils.PageUtils;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IProductToSellRepository : IRepository<ProductToSell>
    {

        Task<IEnumerable<ProductToSellDto>> GetAll(ProductToSellFilters filters, PaginationPropreties paginationPropreties);

        Task<ProductToSellDto?> Get(int id);

        Task<ProductToSell?> GetById(int id);

        Task<int> GetCount(ProductToSellFilters filters);
    }
}