using System.Linq.Expressions;
using MyWarsha_DTOs.ProductsRestockingBillDTOs;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ProductsRestockingBillFilters;
using Utils.PageUtils;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IProductsRestockingBillRepository : IRepository<ProductsRestockingBill>
    {
        Task<IEnumerable<ProductsRestockingBillDto>> GetAll(PaginationPropreties paginationPropreties, ProductsRestockingBillFilters filters);

        Task<ProductsRestockingBillDto?> Get(int id);
        Task<ProductsRestockingBill?> GetById(int id);
        Task<int> GetCount(ProductsRestockingBillFilters filters);
        Task RemoveRangeAsync();
    }
}