using AutoMapper;
using Basket.Api.Authorization;
using Basket.Api.Extensions;
using Basket.Api.Repository;
using Basket.Contracts;
using Basket.Contracts.Requests;
using Basket.Contracts.Responses;
using Basket.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Basket.Api.Controllers
{
    [ApiController]
    [Authorize(Policy = PolicyConstants.BasketApiPolicy)]
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
            var basket = await _repository.GetBasketAsync(Convert.ToString(customerId));

            var response = _mapper.Map<CustomerBasketResponse>(basket);

            return Ok(response);
        }

        [HttpPost]
        [Route(ApiRoutes.Basket.Add)]
        public async Task<IActionResult> Insert(CustomerBasketUpsertRequest request)
        {
            var customerId = HttpContext.GetCustomerId();

            var response = await _repository.AddItemAsync(customerId.ToString(), request);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }


        [HttpPatch]
        [Route(ApiRoutes.Basket.Update)]
        public async Task<IActionResult> Update(CustomerBasketUpsertRequest request)
        {
            var customerId = HttpContext.GetCustomerId();
            var response = await _repository.UpdateItemAsync(customerId.ToString(), request);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete]
        [Route(ApiRoutes.Basket.Delete)]
        public async Task<IActionResult> Delete()
        {
            var customerId = HttpContext.GetCustomerId();
            var response = await _repository.DeleteBasketAsync(Convert.ToString(customerId));

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
            var basket = await _repository.GetBasketAsync(Convert.ToString(customerId));

            if(basket == null)
            {
                return BadRequest();
            }

            // process checkout

            return Accepted();
        }
    }
}
