using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.ProductTypeDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;

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
            .Select(pt => ProductTypeDto.ToProductTypeDto(pt))
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<ProductType?> GetById(int id)
        {
            return await _context.ProductType.FirstOrDefaultAsync(pt => pt.Id == id);
        }

        public async Task<ProductTypeDto?> Get(Expression<Func<ProductType, bool>> predicate)
        {
            return await _context.ProductType
            .Where(predicate)
            .Select(pt => ProductTypeDto.ToProductTypeDto(pt))
            .AsNoTracking()
            .FirstOrDefaultAsync();
        }
    }
}