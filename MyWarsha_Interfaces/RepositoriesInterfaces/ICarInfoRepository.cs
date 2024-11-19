using System.Linq.Expressions;
using MyWarsha_DTOs.CarInfoDTOs;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface ICarInfoRepository : IRepository<CarInfo>
    {
        public Task<CarInfo?> GetById(int id);
        public Task<CarInfoDto?> Get(Expression<Func<CarInfo, bool>> predicate);
        // public Task<IEnumerable<CarInfoDto>> GetAll(PaginationPropreties paginationPropreties);
        public Task<IEnumerable<CarInfoDto>> GetAll(PaginationPropreties paginationPropreties, Expression<Func<CarInfo, bool>> predicate);  
    }
}