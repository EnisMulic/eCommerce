using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Contracts.Responses;
using Order.Core.Commands;
using Order.Core.Helpers;
using Order.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Api.Handlers.Commands
{
    public class ShipOrderCommandHandler : IRequestHandler<ShipOrderCommand, IResponse>
    {
        private readonly OrderDbContext _context;
        private readonly IResponseBuilder<Domain.Order> _responseBuilder;

        public ShipOrderCommandHandler(OrderDbContext context, IResponseBuilder<Domain.Order> responseBuilder)
        {
            _context = context;
            _responseBuilder = responseBuilder;
        }

        public async Task<IResponse> Handle(ShipOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<Domain.Order>()
                .SingleOrDefaultAsync(i => i.Id == request.Id);

            entity.SetShippedStatus();
            await _context.SaveChangesAsync();

            return _responseBuilder.Create<OrderResponse>(entity);
        }
    }
}
