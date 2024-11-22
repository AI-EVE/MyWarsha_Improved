using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.ProductBrandDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ProductBrandFilters;

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
            .Select(pb => new ProductBrandDto
            {
                Id = pb.Id,
                Name = pb.Name
            })
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<ProductBrand?> GetById(int id)
        {
            return await _context.ProductBrand.FirstOrDefaultAsync(pb => pb.Id == id);
        }

        public async Task<ProductBrandDto?> Get(ProductBrandFilters filters)
        {
            var query = _context.ProductBrand.AsQueryable();
            if (filters.Name != null)
            {
                query = query.Where(pb => pb.Name == filters.Name);
            }

            return await query
            .Select(pb => new ProductBrandDto
            {
                Id = pb.Id,
                Name = pb.Name
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
        }

        public async Task<ProductBrandDto?> GetProductBrandDtoById(int id)
        {
            return await _context.ProductBrand
            .Where(pb => pb.Id == id)
            .Select(pb => new ProductBrandDto
            {
                Id = pb.Id,
                Name = pb.Name
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
        }

    }
}