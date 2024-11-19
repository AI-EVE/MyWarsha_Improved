using System.ComponentModel.DataAnnotations;

namespace MyWarsha_DTOs.PhoneDTOs
{
    public class PhoneUpdateDto
    {
        [Phone]
        public string? Number { get; set; }
    }
}