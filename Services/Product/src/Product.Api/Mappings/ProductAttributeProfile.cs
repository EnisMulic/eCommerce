using AutoMapper;
using Product.Contracts.Requests;
using Product.Contracts.Responses;
using Product.Domain;

namespace Product.Api.Mappings
{
    public class ProductAttributeProfile : Profile
    {
        public ProductAttributeProfile()
        {
            CreateMap<ProductAttribute, ProductAttributeResponse>();
            CreateMap<ProductAttributeUpsertRequest, ProductAttribute>();
            CreateMap<ProductAttributeGroup, ProductAttributeResponse.ProductAttributeGroupResponse>();
        }
    }
}
