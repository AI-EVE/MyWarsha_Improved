using System.Globalization;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.ProductDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductDto?> Get(Expression<Func<Product, bool>> predicate)
        {
            return await _context.Product
                .Where(predicate)
                .Include(p => p.Category)
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductImages)
                .Include(p => p.CarInfoProduct)
                    .ThenInclude(cip => cip.CarInfo)
                        .ThenInclude(ci => ci.CarMaker)
                .Include(p => p.CarInfoProduct)
                    .ThenInclude(cip => cip.CarInfo)
                        .ThenInclude(ci => ci.CarModel)
                .Include(p => p.CarInfoProduct)
                    .ThenInclude(cip => cip.CarInfo)
                        .ThenInclude(ci => ci.CarGeneration)
                .Select(p => ProductDto.ToProductDto(p))
                .FirstOrDefaultAsync();
        }

        public async Task<string?> GetProductName(int id)
        {
            return await _context.Product.Where(x => x.Id == id).Select(x => x.Name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductDtoMulti>> GetAll(PaginationPropreties paginationPropreties, Expression<Func<Product, bool>> predicate)
        {
            var query = _context.Product
                .Where(predicate)
                .Include(p => p.ProductImages)
                .Select(p => ProductDtoMulti.ToProductDtoMulti(p));

            return await paginationPropreties.ApplyPagination(query).ToListAsync();
        }


        public Task<Product?> GetById(int id)
        {
            return _context.Product.FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task<int> Count(Expression<Func<Product, bool>> predicate)
        {
            return await _context.Product.CountAsync(predicate);
        }

        public async Task<int> Stock(int id)
        {
            var product = await _context.Product
                .Where(p => p.Id == id)
                .Include(p => p.ProductsToSell)
                .Include(pb => pb.ProductsBought)
                .FirstOrDefaultAsync();

            var productsToSell = product != null ? product.ProductsToSell.Sum(p => p.Count) : 0;

            var productsBought = product != null ? product.ProductsBought.Sum(p => p.Count) : 0;

            var stock = productsBought - productsToSell;

            if (stock < 0)
            {
                stock = 0;
            }

            return stock;

        }
    }
}