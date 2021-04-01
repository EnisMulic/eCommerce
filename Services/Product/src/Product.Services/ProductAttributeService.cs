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
    public class ProductAttributeService :
        CrudService<ProductAttributeResponse, object, ProductAttribute, ProductAttributeUpsertRequest, ProductAttributeUpsertRequest>,
        IProductAttributeService
    {
        public ProductAttributeService(ProductDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<PagedResponse<ProductAttributeResponse>> GetAsync(object search = null)
        {
            var list = await _context.Set<ProductAttribute>()
                .Include(i => i.ProductAttributeGroup)
                .ToListAsync();

            var response = _mapper.Map<List<ProductAttributeResponse>>(list);
            return new PagedResponse<ProductAttributeResponse>(response);
        }

        public override async Task<ProductAttributeResponse> GetByIdAsync(Guid id)
        {
            var entity = await _context.Set<ProductAttribute>()
                .Include(i => i.ProductAttributeGroup)
                .SingleOrDefaultAsync(i => i.Id == id);

            return _mapper.Map<ProductAttributeResponse>(entity);
        }
    }
}
