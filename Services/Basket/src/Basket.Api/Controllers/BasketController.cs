using AutoMapper;
using Basket.Api.Extensions;
using Basket.Api.Repository;
using Basket.Contracts;
using Basket.Contracts.Requests;
using Basket.Contracts.Responses;
using Basket.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Basket.Api.Controllers
{
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(ApiRoutes.Basket.Get)]
        public async Task<IActionResult> Get()
        {
            var customerId = HttpContext.GetCustomerId();
            var basket = await _repository.GetBasketAsync(customerId);

            var response = _mapper.Map<CustomerBasketResponse>(basket);

            return Ok(response);
        }

        [HttpPost]
        [Route(ApiRoutes.Basket.Update)]
        public async Task<IActionResult> Update(CustomerBasketUpdateRequest request)
        {
            var customerId = HttpContext.GetCustomerId();
            var basket = _mapper.Map<CustomerBasket>(request);
            basket.CustomerId = customerId;

            basket = await _repository.UpdateBasketAsync(basket);
            var response = _mapper.Map<CustomerBasketResponse>(basket);

            return Ok(response);
        }

        [HttpDelete]
        [Route(ApiRoutes.Basket.Delete)]
        public async Task<IActionResult> Delete()
        {
            var customerId = HttpContext.GetCustomerId();
            var response = await _repository.DeleteBasketAsync(customerId);

            if (!response)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        [Route(ApiRoutes.Basket.Checkout)]
        public async Task<IActionResult> Checkout(CheckoutRequest request)
        {
            var customerId = HttpContext.GetCustomerId();
            var basket = await _repository.GetBasketAsync(customerId);

            if(basket == null)
            {
                return BadRequest();
            }

            // process checkout

            return Accepted();
        }
    }
}
