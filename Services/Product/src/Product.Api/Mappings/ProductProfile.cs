using AutoMapper;
using Product.Contracts.Requests;
using Product.Contracts.Responses;

namespace Product.Api.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Domain.Product, ProductResponse>();
            CreateMap<ProductInsertRequest, Domain.Product>()
                .ForMember(i => i.Image, opt => opt.Ignore());
            CreateMap<ProductUpdateRequest, Domain.Product>()
                .ForMember(i => i.Image, opt => opt.Ignore());
        }
    }
}
