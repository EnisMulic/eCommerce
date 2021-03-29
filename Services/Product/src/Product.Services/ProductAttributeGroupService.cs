using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product.Contracts.Requests;
using Product.Contracts.Responses;
using Product.Database;
using Product.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Services
{
    public class ProductAttributeGroupService : IProductAttributeGroupService
    {
        private readonly ProductDbContext _context;
        private readonly IMapper _mapper;

        public ProductAttributeGroupService(ProductDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProductAttributeGroupResponse>> GetAsync()
        {
            var list = await _context.ProductAttributeGroups.ToListAsync();
            return _mapper.Map<List<ProductAttributeGroupResponse>>(list);
        }

        public async Task<ProductAttributeGroupResponse> GetByIdAsync(Guid Id)
        {
            var entity = await _context.ProductAttributeGroups.FindAsync(Id);
            return _mapper.Map<ProductAttributeGroupResponse>(entity);
        }

        public async Task<ProductAttributeGroupResponse> InsertAsync(ProductAttributeGroupUpsertRequest request)
        {
            var entity = _mapper.Map<ProductAttributeGroup>(request);

            await _context.ProductAttributeGroups.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductAttributeGroupResponse>(entity);
        }

        public async Task<ProductAttributeGroupResponse> UpdateAsync(Guid Id, ProductAttributeGroupUpsertRequest request)
        {
            var entity = await _context.ProductAttributeGroups.FindAsync(Id);

            if(entity == null)
            {
                return null;
            }

            _context.ProductAttributeGroups.Attach(entity);
            _context.ProductAttributeGroups.Update(entity);

            _mapper.Map(request, entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<ProductAttributeGroupResponse>(entity);
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var entity = await _context.ProductAttributeGroups.FindAsync(Id);

            _context.ProductAttributeGroups.Remove(entity);
            var result = await _context.SaveChangesAsync();

            return result != 0;
        }
    }
}
