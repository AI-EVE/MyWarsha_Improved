using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.ProductToSellDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Repositories
{
    public class ProductToSellRepository : Repository<ProductToSell>, IProductToSellRepository
    {
        private readonly AppDbContext _context;

        public ProductToSellRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductToSellDto>> GetAll(Expression<Func<ProductToSell, bool>> predicate, PaginationPropreties paginationPropreties)
        {
            var query = _context.ProductToSell
                .Include(x => x.Product)
                    .ThenInclude(x => x.ProductBrand)
                .Include(x => x.Product)
                    .ThenInclude(x => x.ProductType)
                .Include(x => x.Product)
                    .ThenInclude(x => x.Category)
                .Include(x => x.Product)
                    .ThenInclude(x => x.ProductImages)
                .Include(x => x.Product)
                    .ThenInclude(x => x.CarInfoProduct)
                .Where(predicate)
                .Select(x => ProductToSellDto.FromProductToSell(x));

            return await paginationPropreties.ApplyPagination(query).ToListAsync();
        }

        public async Task<ProductToSellDto?> Get(Expression<Func<ProductToSell, bool>> predicate)
        {
            var productToSell = await _context.ProductToSell
                .Include(x => x.Product)
                    .ThenInclude(x => x.ProductBrand)
                .Include(x => x.Product)
                    .ThenInclude(x => x.ProductType)
                .Include(x => x.Product)
                    .ThenInclude(x => x.Category)
                .Include(x => x.Product)
                    .ThenInclude(x => x.ProductImages)
                .Include(x => x.Product)
                    .ThenInclude(x => x.CarInfoProduct)
                .Where(predicate)
                .Select(x => ProductToSellDto.FromProductToSell(x))
                .FirstOrDefaultAsync();

            return productToSell;
        }

        public async Task<ProductToSell?> GetById(int id)
        {
            return await _context.ProductToSell.FindAsync(id);
        }

        public async Task<int> GetCount(Expression<Func<ProductToSell, bool>> predicate)
        {
            return await _context.ProductToSell.CountAsync(predicate);
        }
    }
}