using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.ClientDTOs
{
    public class ClientDtoMulti
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CarsCount { get; set; }
        public string? Email { get; set; }

        public static ClientDtoMulti ToClientDtoMulti(Client? client)
        {
            if (client == null)
            {
                return null!;
            }

            return new ClientDtoMulti
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email
            };
        }
    }
}