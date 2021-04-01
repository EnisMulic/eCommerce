using System;

namespace Product.Contracts.Responses
{
    public class ProductAttributeValueResponse
    {
        public Guid Id { get; set; }
        public ProductAttributeResponse ProductAttribute { get; set; } 
        public string Value { get; set; }
    }
}
