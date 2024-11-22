using System.Linq.Expressions;
using MyWarsha_DTOs.CategoryDTOs;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.CategoryFilters;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category?> GetById(int id);
        Task<CategoryDto?> Get(CategoryFilters filters);
        Task<IEnumerable<CategoryDto>> GetAll();
        Task<CategoryDto?> GetDtoById(int id);
    }
}