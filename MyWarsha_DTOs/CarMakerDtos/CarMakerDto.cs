using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.CarMakerDtos
{
    public class CarMakerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Notes { get; set; }
        public string? Logo { get; set; }

        public static CarMakerDto ToCarMakerDto(CarMaker carMaker)
        {
            if (carMaker == null)
            {
                return null!;
            }

            return new CarMakerDto
            {
                Id = carMaker.Id,
                Name = carMaker.Name,
                Notes = carMaker.Notes,
                Logo = carMaker.Logo
            };
        }
    }
}