using System.ComponentModel.DataAnnotations;

namespace MyWarsha_DTOs.ClientDTOs
{
    public class ClientCreateDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
    }
}