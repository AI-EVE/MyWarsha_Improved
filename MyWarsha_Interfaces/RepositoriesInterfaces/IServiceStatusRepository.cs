using MyWarsha_Models.Models;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IServiceStatusRepository : IRepository<ServiceStatus>
    {
        Task<ServiceStatus?> GetServiceStatusByName(string name);
        Task<ServiceStatus?> GetServiceStatusById(int id);
        Task<IEnumerable<ServiceStatus>> GetServiceStatuses();
    }
}