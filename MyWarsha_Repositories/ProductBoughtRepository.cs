using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.ProductBoughtDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Repositories
{
    public class ProductBoughtRepository : Repository<ProductBought>, IProductBoughtRepository
    {
        private readonly AppDbContext _context;

        public ProductBoughtRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductBoughtDto?> Get(Expression<Func<ProductBought, bool>> predicate)
        {
            return await _context.ProductBought
                .Where(predicate)
                .Select(x => ProductBoughtDto.ToProductBoughtDto(x))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductBoughtDto>> GetAll(PaginationPropreties paginationPropreties, Expression<Func<ProductBought, bool>> predicate)
        {
            var query = _context.ProductBought
                .Where(predicate)
                .Select(x => ProductBoughtDto.ToProductBoughtDto(x));

            return await paginationPropreties.ApplyPagination(query)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductBought?> GetById(int id)
        {
            return await _context.ProductBought
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetCount(Expression<Func<ProductBought, bool>> predicate)
        {
            return await _context.ProductBought
                .CountAsync(predicate);
        }

        public async Task RemoveRangeAsync()
        {
            var allRecords = await _context.ProductBought.ToListAsync();
            _context.ProductBought.RemoveRange(allRecords);
        }
    }
}