using FluentValidation;
using Product.Contracts.Requests;

namespace Product.Api.Validators
{
    public class ProductSearchRequestValidator : AbstractValidator<ProductSearchRequest>
    {
        public ProductSearchRequestValidator()
        {
            RuleFor(i => i.PriceRangeBottom)
                .GreaterThanOrEqualTo(0)
                .LessThan(i => i.PriceRangeTop);

            RuleFor(i => i.PriceRangeTop)
                .GreaterThan(i => i.PriceRangeBottom);
        }
    }
}
