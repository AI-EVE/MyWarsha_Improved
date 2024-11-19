using System.Linq.Expressions;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface ICarGenerationRepository : IRepository<CarGeneration>
    {
        public Task<CarGeneration?> GetById(int id);
        public Task<CarGeneration?> Get(Expression<Func<CarGeneration, bool>> predicate);
        public Task<IEnumerable<CarGeneration>> GetAll(PaginationPropreties paginationPropreties);
        public Task<IEnumerable<CarGeneration>> GetAll(Expression<Func<CarGeneration, bool>> predicate, PaginationPropreties paginationPropreties);  
    }
}