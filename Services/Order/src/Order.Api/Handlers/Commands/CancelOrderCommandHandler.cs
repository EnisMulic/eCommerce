using MediatR;
using Order.Contracts.Responses;
using Order.Core.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Api.Handlers.Commands
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, IResponse>
    {
        public Task<IResponse> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
