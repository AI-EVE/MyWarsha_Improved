using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.CarModelDTOs
{
    public class CarModelDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; 
        public string? Notes { get; set; }

        public static CarModelDto ToCarModelDto(CarModel carModel)
        {
            if (carModel == null)
            {
                return null!;
            }

            return new CarModelDto
            {
                Id = carModel.Id,
                Name = carModel.Name,
                Notes = carModel.Notes
            };
        }
    }
}