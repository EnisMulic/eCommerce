using System;

namespace Product.Contracts.Requests
{
    public class ProductAttributeUpsertRequest
    {
        public string Name { get; set; }
        public Guid ProductAttributeGroupId { get; set; }
    }
}
