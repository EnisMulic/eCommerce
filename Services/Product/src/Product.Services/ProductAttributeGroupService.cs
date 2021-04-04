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
    public class ProductAttributeGroupService : 
        CrudService<ProductAttributeGroupResponse, object, ProductAttributeGroup, ProductAttributeGroupUpsertRequest, ProductAttributeGroupUpsertRequest>, 
        IProductAttributeGroupService
    {

        public ProductAttributeGroupService(ProductDbContext context, IMapper mapper) : base(context, mapper)
        {
           
        }

        public override async Task<PagedResponse<ProductAttributeGroupResponse>> GetAsync(object search = null)
        {
            var list = await _context.Set<ProductAttributeGroup>()
                .Include(i => i.ProductAttributes)
                .ToListAsync();

            var response = _mapper.Map<List<ProductAttributeGroupResponse>>(list);
            return new PagedResponse<ProductAttributeGroupResponse>(response);
        }

        public override async Task<IResponse> GetByIdAsync(Guid id)
        {
            var entity = await _context.Set<ProductAttributeGroup>()
                .Include(i => i.ProductAttributes)
                .SingleOrDefaultAsync(i => i.Id == id);

            var response = _mapper.Map<ProductAttributeGroupResponse>(entity);
            return new Response<ProductAttributeGroupResponse>(response);
        }
    }
}
