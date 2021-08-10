using Basket.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.Api.Repository
{
    public interface IBasketRepository
    {
        IEnumerable<string> GetUsers();
        Task<CustomerBasket> GetBasketAsync(string customerId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string customerId);
    }
}
