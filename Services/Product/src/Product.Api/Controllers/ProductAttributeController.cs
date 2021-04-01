using Microsoft.AspNetCore.Mvc;
using Product.Contracts;
using Product.Contracts.Requests;
using Product.Contracts.Responses;
using Product.Core.Interfaces;

namespace Product.Api.Controllers
{
    [Route(ApiRoutes.ProductAttribute.BaseRoute)]
    [ApiController]
    public class ProductAttributeController :
        CrudController<ProductAttributeResponse, object, ProductAttributeUpsertRequest, ProductAttributeUpsertRequest>
    {
        public ProductAttributeController(IProductAttributeService service) : base(service)
        {
        }
    }
}
