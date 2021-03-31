using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product.Contracts.Responses;
using Product.Core.Interfaces;
using Product.Database;
using Product.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Services
{
    public class BaseService<TModel, TSearch, TDatabase> : IBaseService<TModel, TSearch>
        where TDatabase : class, IEntity<Guid>
        where TSearch : class
    {
        protected readonly ProductDbContext _context;
        protected readonly IMapper _mapper;

        public BaseService(ProductDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual async Task<PagedResponse<TModel>> GetAsync(TSearch search = null)
        {
            var list = await _context.Set<TDatabase>().ToListAsync();
            var response = _mapper.Map<List<TModel>>(list);
            return new PagedResponse<TModel>(response);
        }

        public virtual async Task<TModel> GetByIdAsync(Guid id)
        {
            var entity = await _context.Set<TDatabase>().FindAsync(id);
            return _mapper.Map<TModel>(entity);
        }
    }
}
