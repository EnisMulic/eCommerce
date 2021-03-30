using Microsoft.AspNetCore.Mvc;
using Product.Contracts;
using Product.Contracts.Requests;
using Product.Contracts.Responses;
using Product.Core.Interfaces;

namespace Product.Api.Controllers
{
    [Route(ApiRoutes.Category.BaseRoute)]
    [ApiController]
    public class CategoryController : 
        CrudController<CategoryResponse, object, CategoryUpsertRequest, CategoryUpsertRequest>
    {
        public CategoryController(ICategoryService service) : base(service)
        {
        }
    }
}
