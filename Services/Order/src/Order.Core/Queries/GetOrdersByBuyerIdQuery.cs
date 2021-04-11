using MediatR;
using Order.Contracts.Responses;
using System;

namespace Order.Core.Queries
{
    public class GetOrdersByBuyerIdQuery : IRequest<PagedResponse<OrderResponse>>
    {
        public Guid BuyerId { get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        public GetOrdersByBuyerIdQuery(Guid buyerId, int page, int size)
        {
            BuyerId = buyerId;
            PageSize = size;
            PageNumber = page;
        }
    }
}
