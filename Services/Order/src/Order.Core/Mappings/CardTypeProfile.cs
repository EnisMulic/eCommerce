using AutoMapper;
using Order.Contracts.Responses;
using Order.Domain;

namespace Order.Core.Mappings
{
    public class CardTypeProfile : Profile
    {
        public CardTypeProfile()
        {
            CreateMap<CardType, CardTypeResponse>();
        }
    }
}
