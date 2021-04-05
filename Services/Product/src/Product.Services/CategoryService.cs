using AutoMapper;
using Product.Contracts.Requests;
using Product.Contracts.Responses;
using Product.Core.Helpers;
using Product.Core.Interfaces;
using Product.Database;
using Product.Domain;

namespace Product.Services
{
    public class CategoryService : 
        CrudService<CategoryResponse, object, Category, CategoryUpsertRequest, CategoryUpsertRequest>, 
        ICategoryService
    {
        public CategoryService(ProductDbContext context, IMapper mapper, IResponseBuilder<Category> responseBuilder) 
            : base(context, mapper, responseBuilder)
        {
        }
    }
}
