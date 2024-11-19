namespace MyWarsha_Models.Models
{
    public class CarInfoProduct
    {
        public int CarInfoId { get; set; }
        public CarInfo CarInfo { get; set; } = null!;
        
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}