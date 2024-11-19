using System.Linq.Expressions;
using MyWarsha_DTOs.ProductBrandDTOs;
using MyWarsha_Models.Models;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IProductBrandRepository : IRepository<ProductBrand>
    {
        Task<ProductBrand?> GetById(int id);
        Task<ProductBrandDto?> Get(Expression<Func<ProductBrand, bool>> predicate);
        Task<IEnumerable<ProductBrandDto>> GetAll();
    }
}