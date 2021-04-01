using Product.Contracts.Requests;
using Product.Contracts.Responses;

namespace Product.Core.Interfaces
{
    public interface IProductAttributeService
        : ICrudService<ProductAttributeResponse, object, ProductAttributeUpsertRequest, ProductAttributeUpsertRequest>
    {
    }
}
