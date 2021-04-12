using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Contracts.Responses;
using Order.Core.Helpers;
using Order.Core.Queries;
using Order.Database;
using Order.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Api.Handlers.Queries
{
    public class GetCardTypesQueryHandler : IRequestHandler<GetCardTypesQuery, IResponse>
    {
        private readonly IResponseBuilder<CardType> _responseBuilder;
        private readonly OrderDbContext _context;

        public GetCardTypesQueryHandler(OrderDbContext context, IResponseBuilder<CardType> responseBuilder)
        {
            _context = context;
            _responseBuilder = responseBuilder;
        }

        public async Task<IResponse> Handle(GetCardTypesQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.Set<CardType>().ToListAsync();
            return _responseBuilder.Create<CardTypeResponse>(list);
        }
    }
}
