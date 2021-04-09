using AutoMapper;
using Order.Contracts.Responses;

namespace Order.Core.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Domain.Order, OrderResponse>();
        }
    }
}
