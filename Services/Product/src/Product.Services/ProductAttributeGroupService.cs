using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product.Contracts.Requests;
using Product.Contracts.Responses;
using Product.Core.Helpers;
using Product.Core.Interfaces;
using Product.Database;
using Product.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Services
{
    public class ProductAttributeGroupService : 
        CrudService<ProductAttributeGroupResponse, object, ProductAttributeGroup, ProductAttributeGroupUpsertRequest, ProductAttributeGroupUpsertRequest>, 
        IProductAttributeGroupService
    {

        public ProductAttributeGroupService(ProductDbContext context, IMapper mapper, IResponseBuilder<ProductAttributeGroup> responseBuilder) 
            : base(context, mapper, responseBuilder)
        {
           
        }

        protected override IQueryable<ProductAttributeGroup> ApplyIncludes(IQueryable<ProductAttributeGroup> query)
        {
            return query.Include(i => i.ProductAttributes);
        }
    }
}
