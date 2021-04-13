using Basket.Domain;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Basket.Api.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(ConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(Guid customerId)
        {
            return await _database.KeyDeleteAsync(Convert.ToString(customerId));
        }

        public async Task<CustomerBasket> GetBasketAsync(Guid customerId)
        {
            var basket = await _database.StringGetAsync(Convert.ToString(customerId));

            if (basket.IsNullOrEmpty)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var serializedBasket = JsonConvert.SerializeObject(basket);

            await _database.StringSetAsync(Convert.ToString(basket.CustomerId), serializedBasket);

            return await GetBasketAsync(basket.CustomerId);
        }
    }
}
