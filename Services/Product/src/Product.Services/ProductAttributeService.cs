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
    public class ProductAttributeService :
        CrudService<ProductAttributeResponse, object, ProductAttribute, ProductAttributeUpsertRequest, ProductAttributeUpsertRequest>,
        IProductAttributeService
    {
        public ProductAttributeService(ProductDbContext context, IMapper mapper, IResponseBuilder<ProductAttribute> responseBuilder) 
            : base(context, mapper, responseBuilder)
        {
        }

        protected override IQueryable<ProductAttribute> ApplyIncludes(IQueryable<ProductAttribute> query)
        {
            return query.Include(i => i.ProductAttributeGroup);
        }
    }
}
