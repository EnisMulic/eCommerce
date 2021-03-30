using Microsoft.AspNetCore.Mvc;
using Product.Contracts;
using Product.Contracts.Requests;
using Product.Contracts.Responses;
using Product.Core.Interfaces;

namespace Product.Api.Controllers
{
    [Route(ApiRoutes.ProductAttributeGroup.BaseRoute)]
    [ApiController]
    public class ProductAttributeGroupController : 
        CrudController<ProductAttributeGroupResponse, object, ProductAttributeGroupUpsertRequest, ProductAttributeGroupUpsertRequest>
    {
        public ProductAttributeGroupController(IProductAttributeGroupService service) : base(service)
        {
          
        }
    }
}
