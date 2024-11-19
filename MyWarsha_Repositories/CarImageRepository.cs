using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.CarImageDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;

namespace MyWarsha_Repositories
{
    public class CarImageRepository : Repository<CarImage>, ICarImageRepository
    {
        private readonly AppDbContext _context;

        public CarImageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CarImageDto>> GetAll(int carId)
        {
            return await _context.CarImage
                .Where(ci => ci.CarId == carId)
                .Select(ci => CarImageDto.ToCarImageDto(ci))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<CarImage>> GetAllEntities(int carId)
        {
            return await _context.CarImage
                .Where(ci => ci.CarId == carId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<CarImage?> GetById(int id)
        {
            return await _context.CarImage
                .FirstOrDefaultAsync(ci => ci.Id == id);
        }

        public async Task<CarImageDto?> GetMainImage(int carId)
        {
            return await _context.CarImage
                .Where(ci => ci.CarId == carId && ci.IsMain)
                .Select(ci => CarImageDto.ToCarImageDto(ci))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<CarImage?> GetMainImageEntity(int carId)
        {
            return await _context.CarImage
                .FirstOrDefaultAsync(ci => ci.CarId == carId && ci.IsMain);
        }

        public int Count(int carId)
        {
            return _context.CarImage
                .Count(ci => ci.CarId == carId);
        }
    }
}