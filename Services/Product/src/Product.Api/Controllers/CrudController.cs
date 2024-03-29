﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Authorization;
using Product.Contracts.Responses;
using Product.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Product.Api.Controllers
{
    [ApiController]
    public class CrudController<T, TSearch, TInsert, TUpdate> : BaseController<T, TSearch>
    {
        private readonly ICrudService<T, TSearch, TInsert, TUpdate> _service = null;
        public CrudController(ICrudService<T, TSearch, TInsert, TUpdate> service) : base(service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Policy = PolicyConstants.ProductApiWritePolicy)]
        public virtual async Task<IActionResult> InsertAsync(TInsert request)
        {
            var response = await _service.InsertAsync(request);
            if (response == null)
            {
                return BadRequest();
            }

            PathString path = HttpContext.Request.Path;
            return Created(path, response);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = PolicyConstants.ProductApiWritePolicy)]
        public virtual async Task<IActionResult> UpdateAsync(Guid id, TUpdate request)
        {
            var response = await _service.UpdateAsync(id, request);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyConstants.ProductApiDeletePolicy)]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _service.DeleteAsync(id);

            if (response is ErrorResponse)
            {
                return NoContent();
            }

            return Ok(response);
        }
    }
}
