using AutoMapper;
using Basket.Contracts.Responses;
using Basket.Domain;

namespace Basket.Api.Mappings
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketItem, BasketItemResponse>();
            CreateMap<CustomerBasket, CustomerBasketResponse>();
        }
    }
}
