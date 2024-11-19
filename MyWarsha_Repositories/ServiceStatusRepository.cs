using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;

namespace MyWarsha_Repositories
{
    public class ServiceStatusRepository : Repository<ServiceStatus>, IServiceStatusRepository
    {
        private readonly AppDbContext _context;
        public ServiceStatusRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ServiceStatus?> GetServiceStatusById(int id)
        {
            return await _context.ServiceStatus.FindAsync(id);
        }

        public async Task<ServiceStatus?> GetServiceStatusByName(string name)
        {
            return await _context.ServiceStatus.FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task<IEnumerable<ServiceStatus>> GetServiceStatuses()
        {
            return await _context.ServiceStatus.ToListAsync();
        }
    }
}