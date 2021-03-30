using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product.Contracts.Requests;
using Product.Contracts.Responses;
using Product.Core.Interfaces;
using Product.Database;
using Product.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ProductDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(ProductDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoryResponse>> GetAsync()
        {
            var list = await _context.Set<Category>().ToListAsync();
            return _mapper.Map<List<CategoryResponse>>(list);
        }

        public async Task<CategoryResponse> GetByIdAsync(Guid Id)
        {
            var entity = await _context.Set<Category>().FindAsync(Id);
            return _mapper.Map<CategoryResponse>(entity);
        }

        public async Task<CategoryResponse> InsertAsync(CategoryUpsertRequest request)
        {
            var entity = _mapper.Map<Category>(request);

            await _context.Set<Category>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryResponse>(entity);
        }

        public async Task<CategoryResponse> UpdateAsync(Guid Id, CategoryUpsertRequest request)
        {
            var entity = await _context.Set<Category>().FindAsync(Id);

            if (entity == null)
            {
                return null;
            }

            _context.Set<Category>().Attach(entity);
            _context.Set<Category>().Update(entity);

            _mapper.Map(request, entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryResponse>(entity);
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var entity = await _context.Set<Category>().FindAsync(Id);

            _context.Set<Category>().Remove(entity);
            var result = await _context.SaveChangesAsync();

            return result != 0;
        }
    }
}
