using Microsoft.AspNetCore.Mvc;
using Product.Contracts;
using Product.Contracts.Requests;
using Product.Contracts.Responses;
using Product.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Product.Api.Controllers
{
    [Route(ApiRoutes.Category.BaseRoute)]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _service.GetAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid Id)
        {
            var response = await _service.GetByIdAsync(Id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);

        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CategoryUpsertRequest request)
        {
            var response = await _service.InsertAsync(request);
            return Created(nameof(GetByIdAsync), response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid Id, CategoryUpsertRequest request)
        {
            var response = await _service.UpdateAsync(Id, request);

            if (response == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var response = await _service.DeleteAsync(Id);

            if (response == false)
            {
                return NoContent();
            }

            return Ok();
        }
    }
}
