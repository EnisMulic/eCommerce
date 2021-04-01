using Product.Contracts.Requests;
using Product.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Core.Interfaces
{
    public interface IProductService : 
        ICrudService<ProductResponse, ProductSearchRequest, ProductInsertRequest, ProductUpdateRequest>
    {
        Task<ProductResponse> AddAttributes(Guid id, List<ProductAttributeValueInsertRequest> productAttributes);
    }
}
