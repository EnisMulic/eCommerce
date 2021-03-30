using AutoMapper;
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

        public virtual async Task<TModel> InsertAsync(TInsert request)
        {
            var entity = _mapper.Map<TDatabase>(request);

            await _context.Set<TDatabase>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<TModel> UpdateAsync(Guid id, TUpdate request)
        {
            var entity = await _context.Set<TDatabase>().FindAsync(id);

            if(entity == null)
            {
                return default;
            }

            _mapper.Map(request, entity);

            _context.Set<TDatabase>().Update(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Set<TDatabase>().FindAsync(id);

            _context.Set<TDatabase>().Remove(entity);
            var result = await _context.SaveChangesAsync();

            return result != 0;
        }
    }
}
