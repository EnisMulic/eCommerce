using AutoMapper;
using Product.Contracts.Requests;
using Product.Contracts.Responses;
using Product.Core.Interfaces;
using Product.Database;
using Product.Domain;

namespace Product.Services
{
    public class ProductOptionService :
        CrudService<ProductOptionResponse, object, ProductOption, ProductOptionUpsertRequest, ProductOptionUpsertRequest>,
        IProductOptionsService
    {
        public ProductOptionService(ProductDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
