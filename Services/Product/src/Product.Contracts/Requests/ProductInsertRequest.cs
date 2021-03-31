using Microsoft.AspNetCore.Http;

namespace Product.Contracts.Requests
{
    public class ProductInsertRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
    }
}
