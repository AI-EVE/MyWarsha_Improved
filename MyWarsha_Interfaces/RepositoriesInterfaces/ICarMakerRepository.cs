using System.Linq.Expressions;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface ICarMakerRepository : IRepository<CarMaker>
    {
        public Task<CarMaker?> GetById(int id);
        public Task<CarMaker?> Get(Expression<Func<CarMaker, bool>> predicate);
        public Task<IEnumerable<CarMaker>> GetAll(PaginationPropreties paginationPropreties);
        public Task<IEnumerable<CarMaker>> GetAll(Expression<Func<CarMaker, bool>> predicate, PaginationPropreties paginationPropreties);  
    }
}