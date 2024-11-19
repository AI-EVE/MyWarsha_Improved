using MyWarsha_DTOs.CarDTOs;
using MyWarsha_DTOs.PhoneDTOs;
using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.ClientDTOs
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public List<PhoneDto> Phones { get; set; } = [];
        public List<CarDto> Cars { get; set; }  = [];

        public static ClientDto ToClientDto(Client client)
        {
            return new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                Phones = client.Phones.Select(PhoneDto.ToPhoneDto).ToList(),
                Cars = client.Cars.Select(CarDto.ToCarDto).ToList()
            };
        }
    }
}