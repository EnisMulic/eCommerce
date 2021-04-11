using MediatR;
using Order.Contracts.Responses;
using System;

namespace Order.Core.Commands
{
    public class ShipOrderCommand : IRequest<IResponse>
    {
        public Guid Id { get; }

        public ShipOrderCommand(Guid id)
        {
            Id = id;
        }
    }
}
