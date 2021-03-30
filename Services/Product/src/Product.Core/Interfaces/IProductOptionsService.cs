using Product.Contracts.Requests;
using Product.Contracts.Responses;

namespace Product.Core.Interfaces
{
    public interface IProductOptionsService : 
        ICrudService<ProductOptionResponse, object, ProductOptionUpsertRequest, ProductOptionUpsertRequest>
    {
    }
}
