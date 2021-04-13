using Basket.Contracts;
using Basket.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Basket.Api.Controllers
{
    [ApiController]
    public class BasketController : ControllerBase
    {
        [HttpGet]
        [Route(ApiRoutes.Basket.Get)]
        public async Task<IActionResult> Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route(ApiRoutes.Basket.Update)]
        public async Task<IActionResult> Update(CustomerBasketUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route(ApiRoutes.Basket.Delete)]
        public async Task<IActionResult> Delete()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route(ApiRoutes.Basket.Checkout)]
        public async Task<IActionResult> Checkout(CheckoutRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
