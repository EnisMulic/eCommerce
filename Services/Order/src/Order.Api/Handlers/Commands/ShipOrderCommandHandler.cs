using MediatR;
using Order.Contracts.Responses;
using Order.Core.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Api.Handlers.Commands
{
    public class ShipOrderCommandHandler : IRequestHandler<ShipOrderCommand, IResponse>
    {
        public Task<IResponse> Handle(ShipOrderCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
