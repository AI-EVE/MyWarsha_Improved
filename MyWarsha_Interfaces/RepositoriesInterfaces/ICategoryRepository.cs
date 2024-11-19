using System.Linq.Expressions;
using MyWarsha_DTOs.CategoryDTOs;
using MyWarsha_Models.Models;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category?> GetById(int id);
        Task<CategoryDto?> Get(Expression<Func<Category, bool>> predicate);
        Task<IEnumerable<CategoryDto>> GetAll();
    }
}