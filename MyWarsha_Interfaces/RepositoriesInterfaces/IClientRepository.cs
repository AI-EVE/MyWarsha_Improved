using System.Linq.Expressions;
using MyWarsha_DTOs.ClientDTOs;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ClientFilters;
using Utils.PageUtils;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client?> GetById(int id);
        Task<ClientDto?> Get(Expression<Func<Client, bool>> filter);
        Task<IEnumerable<ClientDtoMulti>> GetAll(ClientFilters filters, PaginationPropreties paginationPropreties);
        Task<bool> HasCar(int clientId, int carId);
        Task<bool> HasPhone(int clientId, int phoneId);
        Task<int> FilterCount(ClientFilters filters);

    }
}