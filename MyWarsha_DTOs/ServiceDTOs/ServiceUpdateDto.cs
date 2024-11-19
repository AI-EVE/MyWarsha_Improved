namespace MyWarsha_DTOs.ServiceDTOs
{
    public class ServiceUpdateDto
    {
        public string? Date { get; set; }
        public int? ClientId { get; set; }
        public int? CarId { get; set; }
        public int? ServiceStatusId { get; set; }
        public string? Note { get; set; }
    }
}