using System;

namespace Product.Contracts.Requests
{
    public class ProductAttributeValueInsertRequest
    {
        public Guid ProductAttributeId { get; set; }
        public string Value { get; set; }
    }
}
