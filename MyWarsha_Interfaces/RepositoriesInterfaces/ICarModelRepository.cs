using System.Linq.Expressions;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface ICarModelRepository : IRepository<CarModel>
    {
        public Task<CarModel?> GetById(int id);
        public Task<CarModel?> Get(Expression<Func<CarModel, bool>> predicate);
        public Task<IEnumerable<CarModel>> GetAll(PaginationPropreties paginationPropreties);
        public Task<IEnumerable<CarModel>> GetAll(Expression<Func<CarModel, bool>> predicate, PaginationPropreties paginationPropreties);  
    }
}