using System.Linq.Expressions;
using MyWarsha_DTOs.ProductTypeDTOs;
using MyWarsha_Models.Models;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IProductTypeRepository : IRepository<ProductType>
    {
        Task<ProductType?> GetById(int id);
        Task<ProductTypeDto?> Get(Expression<Func<ProductType, bool>> predicate);
        Task<IEnumerable<ProductTypeDto>> GetAll();
    }
}