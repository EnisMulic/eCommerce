using Basket.Domain;
using System;
using System.Threading.Tasks;

namespace Basket.Api.Repository
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(Guid customerId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(Guid customerId);
    }
}
