using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.ProductTypeDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ProductTypeFilters;

namespace MyWarsha_Repositories
{
    public class ProductTypeRepository : Repository<ProductType>, IProductTypeRepository
    {
        private readonly AppDbContext _context;

        public ProductTypeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductTypeDto>> GetAll()
        {
            return await _context.ProductType
            .Select(pt => new ProductTypeDto
            {
                Id = pt.Id,
                Name = pt.Name
            })
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<ProductType?> GetById(int id)
        {
            return await _context.ProductType.FirstOrDefaultAsync(pt => pt.Id == id);
        }

        public async Task<ProductTypeDto?> Get(ProductTypeFilters filters)
        {
            var query = _context.ProductType.AsQueryable();

            if (filters.Name != null)
            {
                query = query.Where(pt => pt.Name == filters.Name);
            }

            return await query
            .Select(pt => new ProductTypeDto
            {
                Id = pt.Id,
                Name = pt.Name
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
        }
        public async Task<ProductTypeDto?> GetDtoById(int id)
        {
            return await _context.ProductType.Where(pt => pt.Id == id).Select(pt => new ProductTypeDto
            {
                Id = pt.Id,
                Name = pt.Name
            }).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}