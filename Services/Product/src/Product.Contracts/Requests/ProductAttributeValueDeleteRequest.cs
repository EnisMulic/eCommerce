using System;
using System.Collections.Generic;

namespace Product.Contracts.Requests
{
    public class ProductAttributeValueDeleteRequest
    {
        public List<Guid> AttributeValueIds { get; set; }
    }
}
