using Product.Contracts.Requests;
using Product.Contracts.Responses;
using System;
using System.Threading.Tasks;

namespace Product.Core.Interfaces
{
    public interface IBaseService<T, TSearch>
    {
        public Task<IResponse> GetAsync(TSearch search, PaginationQuery pagination = null);
        public Task<IResponse> GetByIdAsync(Guid id);
    }
}
