using Product.Contracts.Responses;
using System;
using System.Threading.Tasks;

namespace Product.Core.Interfaces
{
    public interface ICrudService<T, TSearch, TInsert, TUpdate> : IBaseService<T, TSearch>
    {
        Task<IResponse> InsertAsync(TInsert request);
        Task<IResponse> UpdateAsync(Guid id, TUpdate request);
        Task<IResponse> DeleteAsync(Guid id);
    }
}
