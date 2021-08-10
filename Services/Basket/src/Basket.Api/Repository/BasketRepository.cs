using Basket.Domain;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.Api.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        private readonly ConnectionMultiplexer _redis;

        public BasketRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string customerId)
        {
            return await _database.KeyDeleteAsync(customerId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string customerId)
        {
            var basket = await _database.StringGetAsync(customerId);

            if (basket.IsNullOrEmpty)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<CustomerBasket>(basket);
        }

        public IEnumerable<string> GetUsers()
        {
            var endpoint = _redis.GetEndPoints();
            var server = _redis.GetServer(endpoint.First());

            var data = server.Keys();
            return data?.Select(i => i.ToString());
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var serializedBasket = JsonConvert.SerializeObject(basket);

            await _database.StringSetAsync(Convert.ToString(basket.CustomerId), serializedBasket);

            return await GetBasketAsync(Convert.ToString(basket.CustomerId));
        }
    }
}
