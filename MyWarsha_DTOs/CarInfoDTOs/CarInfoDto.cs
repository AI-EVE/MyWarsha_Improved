using MyWarsha_DTOs.CarGenerationDtos;
using MyWarsha_DTOs.CarMakerDtos;
using MyWarsha_DTOs.CarModelDTOs;
using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.CarInfoDTOs
{
    public class CarInfoDto
    {
        public int Id { get; set; }
        public CarMakerDto? CarMaker { get; set; }
        public CarModelDto? CarModel { get; set; }
        public CarGenerationDto? CarGeneration { get; set; }

        public static CarInfoDto ToCarInfoDto(CarInfo carInfo)
        {
            if (carInfo == null)
            {
                return null!;
            }

            return new CarInfoDto
            {
                Id = carInfo.Id,
                CarMaker = CarMakerDto.ToCarMakerDto(carInfo.CarMaker),
                CarModel = CarModelDto.ToCarModelDto(carInfo.CarModel),
                CarGeneration = CarGenerationDto.ToCarGenerationDto(carInfo.CarGeneration)
            };
        }

    }
}