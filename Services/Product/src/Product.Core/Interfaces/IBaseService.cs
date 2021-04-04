using Product.Contracts.Responses;
using System;
using System.Threading.Tasks;

namespace Product.Core.Interfaces
{
    public interface IBaseService<T, TSearch>
    {
        public Task<PagedResponse<T>> GetAsync(TSearch search);
        public Task<IResponse> GetByIdAsync(Guid id);
    }
}
