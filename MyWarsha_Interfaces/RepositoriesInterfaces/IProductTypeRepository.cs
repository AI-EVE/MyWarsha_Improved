using MyWarsha_DTOs.ProductTypeDTOs;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ProductTypeFilters;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IProductTypeRepository : IRepository<ProductType>
    {
        Task<ProductType?> GetById(int id);
        Task<ProductTypeDto?> Get(ProductTypeFilters filters);
        Task<IEnumerable<ProductTypeDto>> GetAll();
        Task<ProductTypeDto?> GetDtoById(int id);
    }
}