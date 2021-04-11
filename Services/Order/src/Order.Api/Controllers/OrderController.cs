using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Api.Extensions;
using Order.Contracts;
using Order.Contracts.Requests;
using Order.Core.Queries;
using System;
using System.Threading.Tasks;

namespace Order.Api.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route(ApiRoutes.Order.Get)]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationQuery pagination)
        {
            var buyerId = HttpContext.GetBuyerId();

            if(buyerId == Guid.Empty)
            {
                return BadRequest();
            }

            var query = new GetOrdersByBuyerIdQuery(buyerId, pagination.PageNumber, pagination.PageSize);
            var response = await _mediator.Send(query);
            return Ok(response);
        }


        [HttpGet]
        [Route(ApiRoutes.Order.GetById)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var query = new GetOrderByIdQuery(id);
            var response = await _mediator.Send(query);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPatch]
        [Route(ApiRoutes.Order.Cancel)]
        public async Task<IActionResult> CancelAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        [Route(ApiRoutes.Order.Ship)]
        public async Task<IActionResult> ShipAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route(ApiRoutes.Order.GetCardTypes)]
        public async Task<IActionResult> GetCardTypesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
