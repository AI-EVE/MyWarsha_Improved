using System.ComponentModel.DataAnnotations;

namespace MyWarsha_Models.Models
{
    public class ServiceStatus
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string ColorLight { get; set; } = null!;
        [Required]
        public string ColorDark { get; set; } = null!;
        public string? Description { get; set; }
        public List<Service> Services { get; set; } = [];
    }
}