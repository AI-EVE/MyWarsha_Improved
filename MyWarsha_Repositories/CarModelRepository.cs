using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Repositories
{
    public class CarModelRepository : Repository<CarModel>, ICarModelRepository
    {
        private readonly AppDbContext _context;
        public CarModelRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CarModel?> Get(Expression<Func<CarModel, bool>> predicate)
        {
            return await _context.CarModel.Include(ele => ele.CarGenerations).FirstOrDefaultAsync(predicate);
        }

        public async Task<CarModel?> GetById(int id)
        {
            return await _context.CarModel.Include(ele => ele.CarGenerations).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<CarModel>> GetAll(PaginationPropreties paginationPropreties)
        {
            var query = _context.CarModel.Include(ele => ele.CarGenerations);

            return await paginationPropreties.ApplyPagination(query).ToListAsync();

            // return await _context.CarModel.Include(ele => ele.CarGenerations).Skip(paginationPropreties.Skip())
            //     .Take(paginationPropreties.PageSize).ToListAsync();
        }

        public async Task<IEnumerable<CarModel>> GetAll(Expression<Func<CarModel, bool>> predicate, PaginationPropreties paginationPropreties)
        {
            var query = _context.CarModel.Include(ele => ele.CarGenerations).Where(predicate);

            return await paginationPropreties.ApplyPagination(query).ToListAsync();

            // return await _context.CarModel.Include(ele => ele.CarGenerations).Where(predicate).Skip(paginationPropreties.Skip())
            //     .Take(paginationPropreties.PageSize).ToListAsync();
        }
    }
}