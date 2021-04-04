using FluentValidation;
using Product.Contracts.Requests;

namespace Product.Api.Validators
{
    public class ProductAttributeGroupUpsertRequestValidator : AbstractValidator<ProductAttributeGroupUpsertRequest>
    {
        public ProductAttributeGroupUpsertRequestValidator()
        {
            RuleFor(i => i.Name)
                .NotEmpty();
        }
    }
}
