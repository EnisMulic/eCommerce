using AutoMapper;
using Product.Contracts.Responses;
using Product.Core.Interfaces;
using Product.Database;
using Product.Domain;
using System;
using System.Threading.Tasks;

namespace Product.Services
{
    public class CrudService<TModel, TSearch, TDatabase, TInsert, TUpdate> :
        BaseService<TModel, TSearch, TDatabase>, ICrudService<TModel, TSearch, TInsert, TUpdate>
        where TDatabase : class, IEntity<Guid>
        where TSearch : class
    {
        public CrudService(ProductDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public virtual async Task<IResponse> InsertAsync(TInsert request)
        {
            var entity = _mapper.Map<TDatabase>(request);

            await _context.Set<TDatabase>().AddAsync(entity);
            await _context.SaveChangesAsync();

            var response = _mapper.Map<TModel>(entity);
            return new Response<TModel>(response);
        }

        public virtual async Task<IResponse> UpdateAsync(Guid id, TUpdate request)
        {
            var entity = await _context.Set<TDatabase>().FindAsync(id);

            if(entity == null)
            {
                return default;
            }

            _mapper.Map(request, entity);

            _context.Set<TDatabase>().Update(entity);
            await _context.SaveChangesAsync();

            var response = _mapper.Map<TModel>(entity);
            return new Response<TModel>(response);
        }

        public virtual async Task<IResponse> DeleteAsync(Guid id)
        {
            var entity = await _context.Set<TDatabase>().FindAsync(id);

            _context.Set<TDatabase>().Remove(entity);
            var result = await _context.SaveChangesAsync();

            return new Response<bool>(result != 0);
        }
    }
}
