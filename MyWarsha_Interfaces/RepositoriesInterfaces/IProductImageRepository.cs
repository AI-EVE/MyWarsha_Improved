using System.Linq.Expressions;
using MyWarsha_DTOs.ProductImageDTOs;
using MyWarsha_Models.Models;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
        Task<IEnumerable<ProductImageDto>> GetAll(Expression<Func<ProductImage, bool>> predicate);

        Task<ProductImageDto?> Get(Expression<Func<ProductImage, bool>> predicate);

        Task<ProductImage?> GetById(int id);
        Task<ProductImage?> GetMainByProductId(int productId);
        Task<List<ProductImage>> GetAllByProductId(int productId);
        Task<int> DeleteAllByProductId(int productId);
    }
}