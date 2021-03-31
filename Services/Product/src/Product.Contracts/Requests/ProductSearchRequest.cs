namespace Product.Contracts.Requests
{
    public class ProductSearchRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PriceRangeBottom { get; set; }
        public decimal PriceRangeTop { get; set; }
    }
}
