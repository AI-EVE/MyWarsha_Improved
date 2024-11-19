using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.ServiceFeeDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Repositories
{
    public class ServiceFeeRepository : Repository<ServiceFee>, IServiceFeeRepository
    {
        private readonly AppDbContext _context;

        public ServiceFeeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceFeeDto>> GetAll(Expression<Func<ServiceFee, bool>> predicate, PaginationPropreties paginationPropreties)
        {
            var query = _context.ServiceFee
                .Where(predicate)
                .Select(x => ServiceFeeDto.FromServiceFee(x));

            return await paginationPropreties.ApplyPagination(query).ToListAsync();
        }

        public async Task<ServiceFeeDto?> Get(Expression<Func<ServiceFee, bool>> predicate)
        {
            var serviceFee = await _context.ServiceFee
                .Where(predicate)
                .Select(x => ServiceFeeDto.FromServiceFee(x))
                .FirstOrDefaultAsync();

            return serviceFee;
        }

        public async Task<ServiceFee?> GetById(int id)
        {
            return await _context.ServiceFee.FindAsync(id);
        }

        public async Task<int> GetCount(Expression<Func<ServiceFee, bool>> predicate)
        {
            return await _context.ServiceFee.CountAsync(predicate);
        }
    }
}