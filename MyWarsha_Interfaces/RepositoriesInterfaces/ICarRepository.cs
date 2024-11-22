using System.Linq.Expressions;
using MyWarsha_DTOs.CarDTOs;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.CarFilters;
using Utils.PageUtils;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<CarDto?> GetDtoById(int id);
        Task<Car?> GetById(int id);
        Task<IEnumerable<CarDto>> GetAll(CarFilters filters, PaginationPropreties paginationPropreties);
        Task<bool> HasClient(int carId, int clientId);
        Task<int> CountFilter(CarFilters filters);
    }
}