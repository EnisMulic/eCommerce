using AutoMapper;
using Product.Contracts.Requests;
using Product.Contracts.Responses;
using Product.Core.Interfaces;
using Product.Database;
using Product.Domain;

namespace Product.Services
{
    public class CategoryService : 
        CrudService<CategoryResponse, object, Category, CategoryUpsertRequest, CategoryUpsertRequest>, 
        ICategoryService
    {
        public CategoryService(ProductDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
