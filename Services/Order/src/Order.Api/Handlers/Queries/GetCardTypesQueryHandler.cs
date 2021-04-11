using MediatR;
using Order.Contracts.Responses;
using Order.Core.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Api.Handlers.Queries
{
    public class GetCardTypesQueryHandler : IRequestHandler<GetCardTypesQuery, IResponse>
    {
        public Task<IResponse> Handle(GetCardTypesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
