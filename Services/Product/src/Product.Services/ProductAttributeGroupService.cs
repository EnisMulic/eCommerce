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
    }
}
