using MyWarsha_DTOs.PhoneDTOs;
using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.ClientDTOs
{
    public class ClientDtoForService
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public static ClientDtoForService ToClientDtoForService(Client client)
        {
            return new ClientDtoForService
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
            };
        }
    }
}