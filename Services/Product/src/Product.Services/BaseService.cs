using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product.Contracts.Requests;
using Product.Contracts.Responses;
using Product.Core.Helpers;
using Product.Core.Interfaces;
using Product.Database;
using Product.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Product.Services
{
    public class BaseService<TModel, TSearch, TDatabase> : IBaseService<TModel, TSearch>
        where TDatabase : class, IEntity<Guid>
        where TSearch : class
    {
        protected readonly ProductDbContext _context;
        protected readonly IMapper _mapper;
        protected readonly IResponseBuilder<TDatabase> _responseBuilder;

        public BaseService(ProductDbContext context, IMapper mapper, IResponseBuilder<TDatabase> responseBuilder)
        {
            _context = context;
            _mapper = mapper;
            _responseBuilder = responseBuilder;
        }

        public virtual async Task<IResponse> GetAsync(TSearch search = null, PaginationQuery pagination = null)
        {
            var query = _context.Set<TDatabase>().AsQueryable();

            query = ApplyFilter(query, search);
            query = ApplyIncludes(query);
            query = query.AsNoTracking();

            if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                var list = await query.ToListAsync();
                return _responseBuilder.Create<TModel>(list);
            }

            query = ApplyPagination(query, pagination);
            return await GetPagedAsync(query, pagination);
        }

        public virtual async Task<IResponse> GetByIdAsync(Guid id)
        {
            var query = _context.Set<TDatabase>().AsQueryable();

            query = ApplyIncludes(query);
            query = ApplyExpression(query, i => i.Id == id);
            query = query.AsNoTracking();

            var entity = await query.SingleOrDefaultAsync();

            if (entity == null)
            {
                return default;
            }

            return _responseBuilder.Create<TModel>(entity);
        }

        protected virtual IQueryable<TDatabase> ApplyExpression(
            IQueryable<TDatabase> query, Expression<Func<TDatabase, bool>> expression = null)
        {
            if (expression != null)
            {
                return query.Where(expression);
            }

            return query;
        }

        protected virtual IQueryable<TDatabase> ApplyFilter(IQueryable<TDatabase> query, TSearch search)
        {
            return query;
        }

        protected virtual IQueryable<TDatabase> ApplyIncludes(IQueryable<TDatabase> query)
        {
            return query;
        }

        protected IQueryable<TDatabase> ApplyPagination(IQueryable<TDatabase> query, PaginationQuery pagination)
        {
            var skip = (pagination.PageNumber - 1) * pagination.PageSize;
            query = query.Skip(skip).Take(pagination.PageSize);

            return query;
        }

        protected async Task<PagedResponse<TModel>> GetPagedAsync(IQueryable<TDatabase> query, PaginationQuery pagination)
        {
            int totalCount = _context.Set<TDatabase>()
                .Select(i => i.Id)
                .Count();

            var paginatedList = await query.ToListAsync();

            return _responseBuilder.Create<TModel>(paginatedList, totalCount, pagination);
        }
    }
}
