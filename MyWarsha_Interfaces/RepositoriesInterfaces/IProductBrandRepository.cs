using System.Linq.Expressions;
using MyWarsha_DTOs.ProductBrandDTOs;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ProductBrandFilters;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IProductBrandRepository : IRepository<ProductBrand>
    {
        Task<ProductBrand?> GetById(int id);
        Task<ProductBrandDto?> Get(ProductBrandFilters filters);
        Task<IEnumerable<ProductBrandDto>> GetAll();
        Task<ProductBrandDto?> GetProductBrandDtoById(int id);
    }
}