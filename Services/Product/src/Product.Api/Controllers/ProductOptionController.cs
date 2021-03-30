using Microsoft.AspNetCore.Mvc;
using Product.Contracts;
using Product.Contracts.Requests;
using Product.Contracts.Responses;
using Product.Core.Interfaces;

namespace Product.Api.Controllers
{
    [Route(ApiRoutes.ProductOption.BaseRoute)]
    [ApiController]
    public class ProductOptionController :
        CrudController<ProductOptionResponse, object, ProductOptionUpsertRequest, ProductOptionUpsertRequest>
    {
        public ProductOptionController(IProductOptionsService service) : base(service)
        {
        }
    }
}
