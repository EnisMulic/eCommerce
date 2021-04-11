using MediatR;
using Order.Contracts.Responses;
using Order.Core.Helpers;
using Order.Core.Queries;
using Order.Database;
using System;
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

        public Task<IResponse> Handle(GetOrdersByBuyerIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
