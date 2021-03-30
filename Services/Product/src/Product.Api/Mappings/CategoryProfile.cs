using AutoMapper;
using Product.Contracts.Requests;
using Product.Contracts.Responses;
using Product.Domain;

namespace Product.Api.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<CategoryUpsertRequest, Category>();
        }
    }
}
