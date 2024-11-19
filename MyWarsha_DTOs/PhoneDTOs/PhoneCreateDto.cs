using System.ComponentModel.DataAnnotations;

namespace MyWarsha_DTOs.PhoneDTOs
{
    public class PhoneCreateDto
    {
        [Required]
        [Phone]
        public string Number { get; set; } = null!;
        [Required]
        public int ClientId { get; set; }
    }
}