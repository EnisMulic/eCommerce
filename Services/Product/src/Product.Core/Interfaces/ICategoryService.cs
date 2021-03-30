using Product.Contracts.Requests;
using Product.Contracts.Responses;

namespace Product.Core.Interfaces
{
    public interface ICategoryService : 
        ICrudService<CategoryResponse, object, CategoryUpsertRequest, CategoryUpsertRequest>
    {
    }
}
