using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Contracts;
using Product.Contracts.Requests;
using Product.Contracts.Responses;
using Product.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Product.Api.Controllers
{
    [Route(ApiRoutes.Product.BaseRoute)]
    [ApiController]
    public class ProductController :
        CrudController<ProductResponse, ProductSearchRequest, ProductInsertRequest, ProductUpdateRequest>
    {
        private readonly IProductService _service;
        public ProductController(IProductService service) : base(service)
        {
            _service = service;
        }

        [HttpPost]
        public override async Task<IActionResult> InsertAsync([FromQuery] ProductInsertRequest request)
        {
            return await base.InsertAsync(request);
        }

        public override async Task<IActionResult> UpdateAsync(Guid id, [FromQuery] ProductUpdateRequest request)
        {
            return await base.UpdateAsync(id, request);
        }
    }
}
