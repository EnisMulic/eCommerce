using Basket.Api.Settings;
using Grpc.Net.Client;
using Product;
using System;

namespace Basket.Api.Grpc
{
    public class ProductGrpcClient : IProductGrpcClient
    {
        private readonly GrpcSettings _grpcSettings;

        public ProductGrpcClient(GrpcSettings grpcSettings)
        {
            _grpcSettings = grpcSettings;
        }

        public ProductGrpcResponse GetProductById(Guid Id)
        {
            var host = _grpcSettings.Clients.Products;
            var channel = GrpcChannel.ForAddress(host);
            var client = new ProductGrpc.ProductGrpcClient(channel);

            var request = new ProductGrpcRequest
            {
                Id = Id.ToString()
            };

            try
            {
                var product = client.GetProductById(request);
                return product;
            }
            catch
            {
                return null;
            }
        }
    }
}
