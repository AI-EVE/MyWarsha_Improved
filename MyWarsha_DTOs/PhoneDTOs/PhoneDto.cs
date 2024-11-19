using MyWarsha_Models.Models;

namespace MyWarsha_DTOs.PhoneDTOs
{
    public class PhoneDto
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public int ClientId { get; set; }

        public static PhoneDto ToPhoneDto(Phone phone)
        {
            return new PhoneDto
            {
                Id = phone.Id,
                Number = phone.Number,
                ClientId = phone.ClientId
            };
        }
    }
}