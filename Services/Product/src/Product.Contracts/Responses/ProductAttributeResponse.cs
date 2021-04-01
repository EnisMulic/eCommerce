using System;

namespace Product.Contracts.Responses
{
    public class ProductAttributeResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ProductAttributeGroupResponse ProductAttributeGroup { get; set; }

        public class ProductAttributeGroupResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }
}
