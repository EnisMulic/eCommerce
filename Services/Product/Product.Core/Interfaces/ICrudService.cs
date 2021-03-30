using System;
using System.Threading.Tasks;

namespace Product.Core.Interfaces
{
    public interface ICrudService<T, TSearch, TInsert, TUpdate> : IBaseService<T, TSearch>
    {
        Task<T> InsertAsync(TInsert request);
        Task<T> UpdateAsync(Guid id, TUpdate request);
        Task<bool> DeleteAsync(Guid id);
    }
}
