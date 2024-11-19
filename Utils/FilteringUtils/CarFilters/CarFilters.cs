namespace Utils.FilteringUtils.CarFilters
{
    public class CarFilters
    {
        public string? Color { get; set; }
        public string? PlateNumber { get; set; } = null!;
        public string? ChassisNumber { get; set; }
        public string? MotorNumber { get; set; }
        public int? ClientId { get; set; }
        public int? CarInfoId { get; set; }
    }
}