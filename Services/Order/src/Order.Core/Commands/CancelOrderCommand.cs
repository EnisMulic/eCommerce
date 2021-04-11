using MediatR;
using Order.Contracts.Responses;
using System;

namespace Order.Core.Commands
{
    public class CancelOrderCommand : IRequest<IResponse>
    {
        public Guid Id { get; }

        public CancelOrderCommand(Guid id)
        {
            Id = id;
        }
    }
}
