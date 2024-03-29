﻿using MediatR;
using Order.Contracts.Responses;
using System;

namespace Order.Core.Queries
{
    public class GetOrderByIdQuery : IRequest<IResponse>
    {
        public Guid Id { get; }

        public GetOrderByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
