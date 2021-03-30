using Product.Contracts.Requests;
using Product.Contracts.Responses;

namespace Product.Core.Interfaces
{
    public interface IProductAttributeGroupService :
        ICrudService<ProductAttributeGroupResponse, object, ProductAttributeGroupUpsertRequest, ProductAttributeGroupUpsertRequest>
    {
    }
}
