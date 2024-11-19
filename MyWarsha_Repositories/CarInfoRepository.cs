using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.CarGenerationDtos;
using MyWarsha_DTOs.CarInfoDTOs;
using MyWarsha_DTOs.CarMakerDtos;
using MyWarsha_DTOs.CarModelDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Repositories
{
    public class CarInfoRepository : Repository<CarInfo>, ICarInfoRepository
    {
        private readonly AppDbContext _context;
        public CarInfoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CarInfoDto?> Get(Expression<Func<CarInfo, bool>> predicate)
        {
            return await _context.CarInfo.Where(predicate)
                .Include(c => c.CarMaker)
                .Include(c => c.CarModel)
                .Include(c => c.CarGeneration)
                .Select(x => CarInfoDto.ToCarInfoDto(x))
                .FirstOrDefaultAsync();
        }

        // public async Task<IEnumerable<CarInfoDto>> GetAll(PaginationPropreties paginationPropreties)
        // {
        //     return await _context.CarInfo .Include(c => c.CarMaker)
        //         .Include(c => c.CarModel)
        //         .Include(c => c.CarGeneration)
        //         .Select(x => CarInfoDto.ToCarInfoDto(x))
        //         .Skip(paginationPropreties.Skip())
        //         .Take(paginationPropreties.PageSize)
        //         .ToListAsync();
        // }

        public async Task<IEnumerable<CarInfoDto>> GetAll(PaginationPropreties paginationPropreties, Expression<Func<CarInfo, bool>> predicate)
        {
            var query = _context.CarInfo.Where(predicate)
                .Include(c => c.CarMaker)
                .Include(c => c.CarModel)
                .Include(c => c.CarGeneration)
                .Select(x => CarInfoDto.ToCarInfoDto(x));

            return await paginationPropreties.ApplyPagination(query).ToListAsync();

            // return await _context.CarInfo.Where(predicate) .Include(c => c.CarMaker)
            //     .Include(c => c.CarModel)
            //     .Include(c => c.CarGeneration)
            //     .Select(x => CarInfoDto.ToCarInfoDto(x))
            //     .Skip(paginationPropreties.Skip())
            //     .Take(paginationPropreties.PageSize)
            //     .ToListAsync();
        }

        public async Task<CarInfo?> GetById(int id)
        {
            return await _context.CarInfo.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}