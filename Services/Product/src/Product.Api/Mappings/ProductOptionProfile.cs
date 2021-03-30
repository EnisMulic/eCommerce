using AutoMapper;
using Product.Contracts.Requests;
using Product.Contracts.Responses;
using Product.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Api.Mappings
{
    public class ProductOptionProfile : Profile
    {
        public ProductOptionProfile()
        {
            CreateMap<ProductOption, ProductOptionResponse>();
            CreateMap<ProductOption, ProductOptionUpsertRequest>().ReverseMap();
        }
    }
}
