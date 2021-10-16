using Product;
using System;

namespace Basket.Api.Grpc
{
    public interface IProductGrpcClient
    {
        ProductGrpcResponse GetProductById(Guid Id);
    }
}
