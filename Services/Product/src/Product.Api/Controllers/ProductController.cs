using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Authorization;
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
        public override Task<IActionResult> GetAsync([FromQuery] ProductSearchRequest search, PaginationQuery pagination, SortQuery sort)
        {
            return base.GetAsync(search, pagination, sort);
        }

        [HttpGet]
        [Route(ApiRoutes.Product.GetById)]
        public override Task<IActionResult> GetByIdAsync(Guid id)
        {
            return base.GetByIdAsync(id);
        }

        [HttpPost]
        [Route(ApiRoutes.Product.Post)]
        [Authorize(Policy = PolicyConstants.ProductApiWritePolicy)]
        public override async Task<IActionResult> InsertAsync([FromQuery] ProductInsertRequest request)
        {
            return await base.InsertAsync(request);
        }

        [HttpPut]
        [Route(ApiRoutes.Product.Put)]
        [Authorize(Policy = PolicyConstants.ProductApiWritePolicy)]
        public override async Task<IActionResult> UpdateAsync(Guid id, [FromQuery] ProductUpdateRequest request)
        {
            return await base.UpdateAsync(id, request);
        }

        [HttpDelete]
        [Route(ApiRoutes.Product.Delete)]
        [Authorize(Policy = PolicyConstants.ProductApiDeletePolicy)]
        public override Task<IActionResult> DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        [HttpPost]
        [Route(ApiRoutes.Product.AddAttribute)]
        [Authorize(Policy = PolicyConstants.ProductApiWritePolicy)]
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
        [Authorize(Policy = PolicyConstants.ProductApiWritePolicy)]
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
        [Authorize(Policy = PolicyConstants.ProductApiDeletePolicy)]
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
        [Authorize(Policy = PolicyConstants.ProductApiWritePolicy)]
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
        [Authorize(Policy = PolicyConstants.ProductApiDeletePolicy)]
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
