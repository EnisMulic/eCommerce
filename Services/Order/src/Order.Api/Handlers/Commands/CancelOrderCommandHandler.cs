using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Contracts.Responses;
using Order.Core.Commands;
using Order.Core.Helpers;
using Order.Database;
using Order.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Api.Handlers.Commands
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, IResponse>
    {
        private readonly OrderDbContext _context;
        private readonly IResponseBuilder<Domain.Order> _responseBuilder;

        public CancelOrderCommandHandler(OrderDbContext context, IResponseBuilder<Domain.Order> responseBuilder)
        {
            _context = context;
            _responseBuilder = responseBuilder;
        }

        public async Task<IResponse> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<Domain.Order>()
                .SingleOrDefaultAsync(i => i.Id == request.Id);

            entity.SetCancelledStatus();
            await _context.SaveChangesAsync();

            return _responseBuilder.Create<OrderResponse>(entity);
        }
    }
}
