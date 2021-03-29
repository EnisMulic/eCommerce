using AutoMapper;
using Product.Contracts.Requests;
using Product.Contracts.Responses;
using Product.Domain;

namespace Product.Api.Mappings
{
    public class ProductAttributeGroupProfile : Profile
    {
        public ProductAttributeGroupProfile()
        {
            CreateMap<ProductAttributeGroup, ProductAttributeGroupResponse>();
            CreateMap<ProductAttributeGroup, ProductAttributeGroupUpsertRequest>().ReverseMap();
        }
    }
}
