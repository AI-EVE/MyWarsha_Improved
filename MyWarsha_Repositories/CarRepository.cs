using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.CarDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Repositories
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CarDto?> Get(Expression<Func<Car, bool>> predicate)
        {
            return await _context.Car.Where(predicate).Include(c => c.CarInfo)
                .ThenInclude(carInfo => carInfo.CarMaker)
                .Include(c => c.CarInfo)
                .ThenInclude(carInfo => carInfo.CarModel)
                .Include(c => c.CarInfo)
                .ThenInclude(carInfo => carInfo.CarGeneration)
                .Include(c => c.CarImages)
                .Select(x => CarDto.ToCarDto(x))
                .FirstOrDefaultAsync();
        }

       

        public async Task<IEnumerable<CarDto>> GetAll(Expression<Func<Car, bool>> predicate, PaginationPropreties paginationPropreties)
        {
            var query = _context.Car.Where(predicate)
                .Include(c => c.CarInfo)
                .ThenInclude(carInfo => carInfo.CarMaker)
                .Include(c => c.CarInfo)
                .ThenInclude(carInfo => carInfo.CarModel)
                .Include(c => c.CarInfo)
                .ThenInclude(carInfo => carInfo.CarGeneration)
                .Include(c => c.CarImages)
                .Select(x => CarDto.ToCarDto(x));

            return await paginationPropreties.ApplyPagination(query).ToListAsync();
        }

        public Task<Car?> GetById(int id)
        {
            return _context.Car.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> HasClient(int carId, int clientId)
        {
            Car? car = await _context.Car.FirstOrDefaultAsync(c => c.Id == carId && c.ClientId == clientId);

            return car != null;
        }

        public async Task<int> CountFilter(Expression<Func<Car, bool>> predicate) {
            int count = await _context.Car.CountAsync(predicate);
            return count;
        }

        public async Task<int> CountWithClientId(int id)
        {
            return await _context.Car.CountAsync(x => x.ClientId == id);
        }
    }
}