using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Product.Contracts.Responses;
using Product.Core.Interfaces;
using Product.Database;
using System;
using System.Threading.Tasks;

namespace Product.Api.Grpc
{
    public class ProductGrpcService : ProductGrpc.ProductGrpcBase
    {
        private readonly ProductDbContext _context;
        public ProductGrpcService(ProductDbContext context)
        {
            _context = context;
        }

        public override async Task<ProductGrpcResponse> GetProductById(ProductGrpcRequest request, ServerCallContext context)
        {
            var product = await _context.Products.Include(i => i.Image)
                .SingleOrDefaultAsync(i => i.Id.ToString() == request.Id);

            if (product != null)
            {
                var response = new ProductGrpcResponse
                {
                    Id = product.Id.ToString(),
                    Description = product.Description,
                    ImageUri = product.Image.Uri,
                    Name = product.Name,
                    Price = Convert.ToDouble(product.Price)
                };

                return response;
            }

            return null;
        }
    }
}
