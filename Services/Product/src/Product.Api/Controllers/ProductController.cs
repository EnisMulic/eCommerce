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

        [HttpGet]
        [Route(ApiRoutes.Product.Get)]
        public override Task<IActionResult> GetAsync([FromQuery] ProductSearchRequest search)
        {
            return base.GetAsync(search);
        }

        [HttpGet]
        [Route(ApiRoutes.Product.GetById)]
        public override Task<IActionResult> GetByIdAsync(Guid id)
        {
            return base.GetByIdAsync(id);
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

        [HttpDelete]
        [Route(ApiRoutes.Product.Delete)]
        public override Task<IActionResult> DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        [HttpPost]
        [Route(ApiRoutes.Product.AddAttribute)]
        public async Task<IActionResult> AddAttributeAsync(Guid id, List<ProductAttributeValueInsertRequest> request)
        {
            var response = await _service.AddAttributesAsync(id, request);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPatch]
        [Route(ApiRoutes.Product.PatchAttribute)]
        public async Task<IActionResult> PatchAttributeAsync(Guid id, Guid attributeValueId, ProductAttributePatchRequest request)
        {
            var response = await _service.PatchAttributeAsync(id, attributeValueId, request);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete]
        [Route(ApiRoutes.Product.DeleteAttribute)]
        public async Task<IActionResult> DeleteAttributeAsync(Guid id, ProductAttributeValueDeleteRequest request)
        {
            var response = await _service.DeleteAttributesAsync(id, request);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        [Route(ApiRoutes.Product.AddCategories)]
        public async Task<IActionResult> AddCategoriesAsync(Guid id, List<Guid> request)
        {
            var response = await _service.AddCategoriesAsync(id, request);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete]
        [Route(ApiRoutes.Product.DeleteCategories)]
        public async Task<IActionResult> DeleteCategoriesAsync(Guid id, List<Guid> request)
        {
            var response = await _service.DeleteCategoriesAsync(id, request);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
