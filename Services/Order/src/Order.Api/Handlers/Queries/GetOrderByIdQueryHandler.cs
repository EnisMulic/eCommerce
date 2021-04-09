using AutoMapper;
using MediatR;
using Order.Contracts.Responses;
using Order.Core.Queries;
using Order.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Api.Handlers.Queries
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse>
    {
        private readonly OrderDbContext _context;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(OrderDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Set<Domain.Order>().FindAsync(request.Id);
            return _mapper.Map<OrderResponse>(order);
        }
    }
}
