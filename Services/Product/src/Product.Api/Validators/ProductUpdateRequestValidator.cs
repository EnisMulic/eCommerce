using FluentValidation;
using Product.Contracts.Requests;

namespace Product.Api.Validators
{
    public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
    {
        public ProductUpdateRequestValidator()
        {
            RuleFor(i => i.Name)
                .NotEmpty();

            RuleFor(i => i.Description)
                .NotEmpty();

            RuleFor(i => i.Price)
                .GreaterThan(0);

            RuleFor(i => i.Image)
                .NotEmpty();
        }
    }
}
