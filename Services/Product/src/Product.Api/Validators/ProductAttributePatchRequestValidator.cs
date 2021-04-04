using FluentValidation;
using Product.Contracts.Requests;

namespace Product.Api.Validators
{
    public class ProductAttributePatchRequestValidator : AbstractValidator<ProductAttributePatchRequest>
    {
        public ProductAttributePatchRequestValidator()
        {
            RuleFor(i => i.Value)
                .NotEmpty();
        }
    }
}
