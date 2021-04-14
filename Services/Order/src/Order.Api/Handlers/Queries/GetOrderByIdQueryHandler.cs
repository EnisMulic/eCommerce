using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Contracts.Responses;
using Order.Core.Helpers;
using Order.Core.Queries;
using Order.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Api.Handlers.Queries
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, IResponse>
    {
        private readonly OrderDbContext _context;
        private readonly IResponseBuilder<Domain.Order> _responseBuilder;

        public GetOrderByIdQueryHandler(OrderDbContext context, IResponseBuilder<Domain.Order> responseBuilder)
        {
            _context = context;
            _responseBuilder = responseBuilder;
        }

        public async Task<IResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Set<Domain.Order>()
                .Include(i => i.OrderItems)
                .Include(i => i.OrderStatus)
                .SingleOrDefaultAsync(i => i.Id == request.Id);

            return _responseBuilder.Create<OrderResponse>(order);
        }
    }
}
