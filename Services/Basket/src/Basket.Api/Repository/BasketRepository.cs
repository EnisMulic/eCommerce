using Basket.Api.Grpc;
using Basket.Contracts.Requests;
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
        private readonly IProductGrpcClient _grpcClient;

        public BasketRepository(ConnectionMultiplexer redis, IProductGrpcClient grpcClient)
        {
            _redis = redis;
            _database = redis.GetDatabase();
            _grpcClient = grpcClient;
        }

        public async Task<CustomerBasket> AddItemAsync(string customerId, CustomerBasketUpsertRequest request)
        {
            var basket = await _database.StringGetAsync(customerId);

            if (basket.IsNullOrEmpty)
            {
                var product = _grpcClient.GetProductById(request.ProductId);

                if (product != null)
                {
                    var customerBasket = new CustomerBasket();
                    customerBasket.Items.Add(new BasketItem
                    {
                        ProductId = Guid.Parse(product.Id),
                        ProductName = product.Name,
                        ImageUrl = product.ImageUri,
                        UnitPrice = Convert.ToDecimal(product.Price),
                        Quantity = request.Quantity
                    });

                    var serializedBasket = JsonConvert.SerializeObject(customerBasket);

                    await _database.StringSetAsync(customerId, serializedBasket);

                    return customerBasket;
                }

                return null;
            }
            else
            {
                return await UpdateItemAsync(customerId, request);
            }
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

        public async Task<CustomerBasket> UpdateItemAsync(string customerId, CustomerBasketUpsertRequest request)
        {
            var basket = await _database.StringGetAsync(customerId);

            if (!basket.IsNullOrEmpty)
            {
                var customerBasket = JsonConvert.DeserializeObject<CustomerBasket>(basket);

                var item = customerBasket.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
                if (item != null)
                {
                    item.Quantity = request.Quantity;
                }

                return await UpdateBasketAsync(customerBasket);
            }

            return null;
        }


        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var serializedBasket = JsonConvert.SerializeObject(basket);

            await _database.StringSetAsync(Convert.ToString(basket.CustomerId), serializedBasket);

            return basket;
        }

        public async Task<bool> DeleteBasketAsync(string customerId)
        {
            return await _database.KeyDeleteAsync(customerId);
        }
    }
}
