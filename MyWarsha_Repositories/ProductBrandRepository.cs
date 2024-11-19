using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.ProductBrandDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;

namespace MyWarsha_Repositories
{
    public class ProductBrandRepository : Repository<ProductBrand>, IProductBrandRepository
    {
        private readonly AppDbContext _context;

        public ProductBrandRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductBrandDto>> GetAll()
        {
            return await _context.ProductBrand
            .Select(pb => ProductBrandDto.ToProductBrandDto(pb))
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<ProductBrand?> GetById(int id)
        {
            return await _context.ProductBrand.FirstOrDefaultAsync(pb => pb.Id == id);
        }

        public async Task<ProductBrandDto?> Get(Expression<Func<ProductBrand, bool>> predicate)
        {
            return await _context.ProductBrand
            .Where(predicate)
            .Select(pb => ProductBrandDto.ToProductBrandDto(pb))
            .AsNoTracking()
            .FirstOrDefaultAsync();
        }
    }
}