using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.ProductsRestockingBillDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Repositories
{
    public class ProductsRestockingBillRepository : Repository<ProductsRestockingBill>, IProductsRestockingBillRepository
    {
        private readonly AppDbContext _context;

        public ProductsRestockingBillRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductsRestockingBillDto?> Get(Expression<Func<ProductsRestockingBill, bool>> predicate)
        {
            return await _context.ProductsRestockingBill
                .Include(x => x.ProductsBought)
                .Where(predicate)
                .Select(x => ProductsRestockingBillDto.ToProductsRestockingBillDto(x))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductsRestockingBillDto>> GetAll(PaginationPropreties paginationPropreties, Expression<Func<ProductsRestockingBill, bool>> predicate)
        {
            var query = _context.ProductsRestockingBill
                .Include(x => x.ProductsBought)
                .Where(predicate)
                .Select(x => ProductsRestockingBillDto.ToProductsRestockingBillDto(x));

            return await paginationPropreties.ApplyPagination(query)
                .AsNoTracking()
                .ToListAsync();    
        }

        public Task<ProductsRestockingBill?> GetById(int id)
        {
            return _context.ProductsRestockingBill
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetCount(Expression<Func<ProductsRestockingBill, bool>> predicate)
        {
            return await _context.ProductsRestockingBill
                .CountAsync(predicate);
        }

        public async Task RemoveRangeAsync()
        {
            var allRecords = await _context.ProductsRestockingBill.ToListAsync();
            _context.ProductsRestockingBill.RemoveRange(allRecords);
        }
    }
}