namespace Utils.FilteringUtils.ProductBoughtFilters
{
    public class ProductBoughtFilters
    {

        public decimal? MinPricePerUnit { get; set; }
        public decimal? MaxPricePerUnit { get; set; }
        public decimal? Discount { get; set; }
        public int? Count { get; set; }
        public bool? IsReturned { get; set; }
        public int? ProductId { get; set; }
        public int? ProductsRestockingBillId { get; set; }
    }
}