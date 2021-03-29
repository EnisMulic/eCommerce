using Product.Contracts.Requests;
using Product.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Services
{
    public interface IProductAttributeGroupService
    {
        public Task<List<ProductAttributeGroupResponse>> GetAsync();
        public Task<ProductAttributeGroupResponse> GetByIdAsync(Guid Id);
        public Task<ProductAttributeGroupResponse> InsertAsync(ProductAttributeGroupUpsertRequest request);
        public Task<ProductAttributeGroupResponse> UpdateAsync(Guid Id, ProductAttributeGroupUpsertRequest request);
        public Task<bool> DeleteAsync(Guid Id);
    }
}
