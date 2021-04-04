using FluentValidation;
using Product.Contracts.Requests;

namespace Product.Api.Validators
{
    public class CategoryUpsertRequestValidator : AbstractValidator<CategoryUpsertRequest>
    {
        public CategoryUpsertRequestValidator()
        {
            RuleFor(i => i.Name)
                .NotEmpty();
        }
    }
}
