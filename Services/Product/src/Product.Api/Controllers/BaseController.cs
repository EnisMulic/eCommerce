using Microsoft.AspNetCore.Mvc;
using Product.Contracts.Requests;
using Product.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Product.Api.Controllers
{
    [ApiController]
    public class BaseController<T, TSearch> : ControllerBase
    {
        private readonly IBaseService<T, TSearch> _service;

        public BaseController(IBaseService<T, TSearch> service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAsync([FromQuery] TSearch search, [FromQuery] PaginationQuery pagination, [FromQuery] SortQuery sort)
        {
            var response = await _service.GetAsync(search, pagination, sort);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var response = await _service.GetByIdAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
