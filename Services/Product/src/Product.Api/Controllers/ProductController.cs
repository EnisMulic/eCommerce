using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Contracts;
using Product.Contracts.Requests;
using Product.Contracts.Responses;
using Product.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Api.Controllers
{
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
        [Route(ApiRoutes.Product.Post)]
        public override async Task<IActionResult> InsertAsync([FromQuery] ProductInsertRequest request)
        {
            return await base.InsertAsync(request);
        }

        [HttpPut]
        [Route(ApiRoutes.Product.Put)]
        public override async Task<IActionResult> UpdateAsync(Guid id, [FromQuery] ProductUpdateRequest request)
        {
            return await base.UpdateAsync(id, request);
        }

        [Route(ApiRoutes.Product.AddAttribute)]
        [HttpPost]
        public async Task<IActionResult> AddAttributeAsync(Guid id, List<ProductAttributeValueInsertRequest> request)
        {
            var response = await _service.AddAttributes(id, request);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
