using Basket.Contracts.Requests;
using Basket.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.Api.Repository
{
    public interface IBasketRepository
    {
        IEnumerable<string> GetUsers();
        Task<CustomerBasket> GetBasketAsync(string customerId);
        Task<CustomerBasket> AddItemAsync(string customerId, CustomerBasketUpsertRequest request);
        Task<CustomerBasket> UpdateItemAsync(string customerId, CustomerBasketUpsertRequest request);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string customerId);
    }
}
