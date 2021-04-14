using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Contracts.Requests;
using Order.Contracts.Responses;
using Order.Core.Helpers;
using Order.Core.Queries;
using Order.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Api.Handlers.Queries
{
    public class GetOrdersByBuyerIdQueryHandler : IRequestHandler<GetOrdersByBuyerIdQuery, IResponse>
    {
        private readonly OrderDbContext _context;
        private readonly IResponseBuilder<Domain.Order> _responseBuilder;

        public GetOrdersByBuyerIdQueryHandler(OrderDbContext context, IResponseBuilder<Domain.Order> responseBuilder)
        {
            _context = context;
            _responseBuilder = responseBuilder;
        }

        public async Task<IResponse> Handle(GetOrdersByBuyerIdQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Set<Domain.Order>()
                .Include(i => i.OrderItems)
                .Include(i => i.OrderStatus)
                .Where(i => i.BuyerId == request.BuyerId)
                .AsNoTracking()
                .AsQueryable();

            var total = await query.CountAsync(cancellationToken);

            query = query.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);

            var list = await query.ToListAsync(cancellationToken);

            var pagination = new PaginationQuery(request.PageNumber, request.PageSize);
            return _responseBuilder.Create<PagedResponse<OrderResponse>>(list, total, pagination);
        }
    }
}
