using Microsoft.AspNetCore.Mvc;
using Product.Contracts.Requests;
using Product.Services;
using System;
using System.Threading.Tasks;

namespace Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAttributeGroupController : ControllerBase
    {
        private readonly IProductAttributeGroupService _service;

        public ProductAttributeGroupController(IProductAttributeGroupService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _service.GetAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid Id)
        {
            var response = await _service.GetByIdAsync(Id);
            
            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);

        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(ProductAttributeGroupUpsertRequest request)
        {
            var response = await _service.InsertAsync(request);
            return Created(nameof(GetByIdAsync), response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid Id, ProductAttributeGroupUpsertRequest request)
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

            if(response == false)
            {
                return NoContent();
            }

            return Ok();
        }
    }
}
