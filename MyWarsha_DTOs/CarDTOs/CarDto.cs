using MyWarsha_DTOs.CarImageDTOs;
using MyWarsha_DTOs.CarInfoDTOs;
using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.CarDTOs
{
    public class CarDto
    {
        public int Id { get; set; }
        public string? Color { get; set; }
        public string PlateNumber { get; set; } = null!;
        public string? ChassisNumber { get; set; }
        public string? MotorNumber { get; set; }
        public string? Notes { get; set; }
        public List<CarImageDto> CarImages { get; set; } = [];
        public int ClientId { get; set; }
        public CarInfoDto CarInfo { get; set; } = null!;

        public static CarDto ToCarDto(Car car)
        {
            return new CarDto
            {
                Id = car.Id,
                Color = car.Color,
                PlateNumber = car.PlateNumber,
                ChassisNumber = car.ChassisNumber,
                MotorNumber = car.MotorNumber,
                Notes = car.Notes,
                ClientId = car.ClientId,
                CarInfo = CarInfoDto.ToCarInfoDto(car.CarInfo),
                CarImages = car.CarImages.Select(CarImageDto.ToCarImageDto).ToList()
            };
        }
    }
}