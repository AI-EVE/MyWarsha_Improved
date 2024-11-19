using System.ComponentModel.DataAnnotations;

namespace MyWarsha_DTOs.ClientDTOs
{
    public class ClientUpdateDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}