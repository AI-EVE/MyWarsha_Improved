using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.ProductImageDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;

namespace MyWarsha_Repositories
{
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        private readonly AppDbContext _context;

        public ProductImageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductImageDto>> GetAll(Expression<Func<ProductImage, bool>> predicate)
        {
            return await _context.ProductImage
                .Where(predicate)
                .Select(pi => ProductImageDto.ToProductImageDto(pi))
                .ToListAsync();
        }

        public async Task<ProductImageDto?> Get(Expression<Func<ProductImage, bool>> predicate)
        {
            return await _context.ProductImage
                .Where(predicate)
                .Select(pi => ProductImageDto.ToProductImageDto(pi))
                .FirstOrDefaultAsync();
        }

        public async Task<ProductImage?> GetById(int id)
        {
            return await _context.ProductImage
                .FirstOrDefaultAsync(pi => pi.Id == id);
        }

        public async Task<ProductImage?> GetMainByProductId(int productId)
        {
            return await _context.ProductImage
                .Where(pi => pi.ProductId == productId && pi.IsMain)
                .FirstOrDefaultAsync();
        }

        public async Task<int> DeleteAllByProductId(int productId)
        {
            var productImages = await _context.ProductImage
                .Where(pi => pi.ProductId == productId)
                .ToListAsync();

            _context.ProductImage.RemoveRange(productImages);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductImage>> GetAllByProductId(int productId)
        {
            return await _context.ProductImage
                .Where(pi => pi.ProductId == productId)
                .ToListAsync();
        }

        public async Task<List<ProductImage>> GetByProductIds(List<int> ids)
        {
            return await _context.ProductImage
                .Where(pi => ids.Contains(pi.ProductId))
                .ToListAsync();
        }

        public void DeleteAll(List<ProductImage> productImages)
        {
            _context.ProductImage.RemoveRange(productImages);
        }
    }
}