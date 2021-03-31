using Product.Contracts.Requests;
using Product.Contracts.Responses;

namespace Product.Core.Interfaces
{
    public interface IProductService : 
        ICrudService<ProductResponse, ProductSearchRequest, ProductInsertRequest, ProductUpdateRequest>
    {
    }
}
