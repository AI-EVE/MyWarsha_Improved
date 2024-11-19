using MyWarsha_DTOs.CarImageDTOs;
using MyWarsha_Models.Models;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface ICarImageRepository : IRepository<CarImage>
    {
        Task<IEnumerable<CarImageDto>> GetAll(int carId);
        Task<IEnumerable<CarImage>> GetAllEntities(int carId);
        Task<CarImageDto?> GetMainImage(int carId);
        Task<CarImage?> GetMainImageEntity(int carId);
        int Count(int carId);
        Task<CarImage?> GetById(int id);
    }
}