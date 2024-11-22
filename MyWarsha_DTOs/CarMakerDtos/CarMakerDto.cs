using System.Linq.Expressions;
using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.CarMakerDtos
{
    public class CarMakerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Notes { get; set; }
        public string? Logo { get; set; }
    }
}