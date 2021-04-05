namespace Product.Contracts.Requests
{
    public class ProductSearchRequest
    {
        public string Name { get; set; }
        public decimal PriceRangeBottom { get; set; } = 0;
        public decimal PriceRangeTop { get; set; } = 9999;
    }
}
