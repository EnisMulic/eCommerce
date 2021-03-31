using AutoMapper;
using Product.Contracts.Responses;
using Product.Domain;

namespace Product.Api.Mappings
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<Image, ImageResponse>();
        }
    }
}
