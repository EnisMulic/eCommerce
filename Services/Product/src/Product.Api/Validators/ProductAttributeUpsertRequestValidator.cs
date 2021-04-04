using FluentValidation;
using Product.Contracts.Requests;

namespace Product.Api.Validators
{
    public class ProductAttributeUpsertRequestValidator : AbstractValidator<ProductAttributeUpsertRequest>
    {
        public ProductAttributeUpsertRequestValidator()
        {
            RuleFor(i => i.Name)
                .NotEmpty();
        }
    }
}
