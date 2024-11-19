using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Repositories
{
    public class CarMakerRepository : Repository<CarMaker>, ICarMakerRepository
    {
        private readonly AppDbContext _context;
        public CarMakerRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CarMaker?> Get(Expression<Func<CarMaker, bool>> predicate)
        {
            return await _context.CarMaker.Include(ele => ele.CarModels).ThenInclude(ele => ele.CarGenerations).FirstOrDefaultAsync(predicate);
        }

        public async Task<CarMaker?> GetById(int id)
        {
            return await _context.CarMaker.Include(ele => ele.CarModels).ThenInclude(ele => ele.CarGenerations).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<CarMaker>> GetAll(PaginationPropreties paginationPropreties)
        {
            var query = _context.CarMaker.Include(ele => ele.CarModels).ThenInclude(ele => ele.CarGenerations);

            return await paginationPropreties.ApplyPagination(query).ToListAsync();

            // return await _context.CarMaker.Include(ele => ele.CarModels).ThenInclude(ele => ele.CarGenerations).Skip(paginationPropreties.Skip())
            //     .Take(paginationPropreties.PageSize).ToListAsync();
        }

        public async Task<IEnumerable<CarMaker>> GetAll(Expression<Func<CarMaker, bool>> predicate, PaginationPropreties paginationPropreties)
        {
            var query = _context.CarMaker.Include(ele => ele.CarModels).ThenInclude(ele => ele.CarGenerations).Where(predicate);

            return await paginationPropreties.ApplyPagination(query).ToListAsync();

            // return await _context.CarMaker.Include(ele => ele.CarModels).ThenInclude(ele => ele.CarGenerations).Where(predicate).Skip(paginationPropreties.Skip())
            //     .Take(paginationPropreties.PageSize).ToListAsync();
        }
    }
}