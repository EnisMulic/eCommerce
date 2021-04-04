using FluentValidation;
using Product.Contracts.Requests;

namespace Product.Api.Validators
{
    public class ProductAttributeValueInsertRequestValidator : AbstractValidator<ProductAttributeValueInsertRequest>
    {
        public ProductAttributeValueInsertRequestValidator()
        {
            RuleFor(i => i.Value)
                .NotEmpty();
        }
    }
}
