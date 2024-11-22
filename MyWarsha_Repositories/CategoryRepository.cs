using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.CategoryDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.CategoryFilters;

namespace MyWarsha_Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            return await _context.Category
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<Category?> GetById(int id)
        {
            return await _context.Category.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CategoryDto?> Get(CategoryFilters filters)
        {
            var query = _context.Category.AsQueryable();

            if (!string.IsNullOrEmpty(filters.Name))
            {
                query = query.Where(c => c.Name.Contains(filters.Name));
            }

            return await query
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            }).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<CategoryDto?> GetDtoById(int id)
        {
            return await _context.Category
            .Where(c => c.Id == id)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();

        }
    }
}