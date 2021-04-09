using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Contracts;
using Order.Core.Queries;
using System;
using System.Threading.Tasks;

namespace Order.Api.Controllers
{
    [Route(ApiRoutes.Order.BaseRoute)]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var query = new GetOrderByIdQuery(id);
            var response = await _mediator.Send(query);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);

        }
    }
}
