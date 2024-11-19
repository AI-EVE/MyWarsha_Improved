using System.Linq.Expressions;
using MyWarsha_DTOs.ProductsRestockingBillDTOs;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IProductsRestockingBillRepository : IRepository<ProductsRestockingBill>
    {
        Task<IEnumerable<ProductsRestockingBillDto>> GetAll(PaginationPropreties paginationPropreties, Expression<Func<ProductsRestockingBill, bool>> predicate);

        Task<ProductsRestockingBillDto?> Get(Expression<Func<ProductsRestockingBill, bool>> predicate);
        Task<ProductsRestockingBill?> GetById(int id);
        Task<int> GetCount(Expression<Func<ProductsRestockingBill, bool>> predicate);
        Task RemoveRangeAsync();
    }
}