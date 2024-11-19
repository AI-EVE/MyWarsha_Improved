using Microsoft.AspNetCore.Http;

namespace MyWarsha_DTOs.CarMakerDtos
{
    public class CarMakerUpdateDto
    {
        public string? Name { get; set; }
        public string? Notes { get; set; }
        public IFormFile? Logo { get; set; }
    }
}