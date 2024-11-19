using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.ClientDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        private readonly AppDbContext _context;
        public ClientRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ClientDto?> Get(Expression<Func<Client, bool>> predicate)
        {
            return await _context.Client.Where(predicate).Include(c => c.Phones)
            .Include(c => c.Cars)
                .ThenInclude(car => car.CarInfo)
                    .ThenInclude(carInfo => carInfo.CarMaker)
            .Include(c => c.Cars)
                .ThenInclude(car => car.CarInfo)
                    .ThenInclude(carInfo => carInfo.CarModel)
            .Include(c => c.Cars)
                .ThenInclude(car => car.CarInfo)
                    .ThenInclude(carInfo => carInfo.CarGeneration)
            .Include(c => c.Cars)
                .ThenInclude(car => car.CarImages)
            .Select(client => ClientDto.ToClientDto(client))
            .AsNoTracking()
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ClientDtoMulti>> GetAll(Expression<Func<Client, bool>> predicate, PaginationPropreties paginationPropreties)
        {
            var query = _context.Client.Where(predicate)
            .Select(x => ClientDtoMulti.ToClientDtoMulti(x));

            return await paginationPropreties.ApplyPagination(query)
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<Client?> GetById(int id)
        {
            return await _context.Client.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> HasCar(int clientId, int carId)
        {
            return await _context.Client
            .Where(c => c.Id == clientId)
            .Include(c => c.Cars)
            .AnyAsync(c => c.Id == carId);
        }

        public async Task<bool> HasPhone(int clientId, int phoneId)
        {
            return await _context.Client
            .Where(c => c.Id == clientId)
            .Include(c => c.Phones)
            .AnyAsync(c => c.Id == phoneId);
        }

        public async Task<int> FilterCount(Expression<Func<Client, bool>> predicate)
        {
            int count = await _context.Client.CountAsync(predicate);
            return count;
        }
    }
}