using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.CarGenerationDtos
{
    public class CarGenerationDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Notes { get; set; }

        public static CarGenerationDto ToCarGenerationDto(CarGeneration carGeneration)
        {
            if (carGeneration == null)
            {
                return null!;
            }

            return new CarGenerationDto
            {
                Id = carGeneration.Id,
                Name = carGeneration.Name,
                Notes = carGeneration.Notes
            };
        }
    }
}