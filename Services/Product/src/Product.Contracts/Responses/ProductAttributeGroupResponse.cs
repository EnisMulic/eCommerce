using System;
using System.Collections.Generic;

namespace Product.Contracts.Responses
{
    public class ProductAttributeGroupResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<ProductAttributeResponse> ProductAttributes { get; set; }
        public class ProductAttributeResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }
}
