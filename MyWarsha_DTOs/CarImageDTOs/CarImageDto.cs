using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.CarImageDTOs
{
    public class CarImageDto
    {
        public int Id { get; set; }
        public string ImagePath { get; set; } = null!;
        public bool IsMain { get; set; }
        public int CarId { get; set; }

        public static CarImageDto ToCarImageDto(CarImage carImage)
        {
            return new CarImageDto
            {
                Id = carImage.Id,
                ImagePath = carImage.ImagePath,
                IsMain = carImage.IsMain,
                CarId = carImage.CarId
            };
        }
    }
}