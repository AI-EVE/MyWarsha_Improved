using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.CategoryDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;

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
            .Select(c => CategoryDto.ToCategoryDto(c))
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<Category?> GetById(int id)
        {
            return await _context.Category.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CategoryDto?> Get(Expression<Func<Category, bool>> predicate)
        {
            return await _context.Category
            .Where(predicate)
            .Select(c => CategoryDto.ToCategoryDto(c))
            .AsNoTracking()
            .FirstOrDefaultAsync();
        }
    }
}