using Product.Contracts.Requests;
using Product.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Core.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<CategoryResponse>> GetAsync();
        public Task<CategoryResponse> GetByIdAsync(Guid Id);
        public Task<CategoryResponse> InsertAsync(CategoryUpsertRequest request);
        public Task<CategoryResponse> UpdateAsync(Guid Id, CategoryUpsertRequest request);
        public Task<bool> DeleteAsync(Guid Id);
    }
}
