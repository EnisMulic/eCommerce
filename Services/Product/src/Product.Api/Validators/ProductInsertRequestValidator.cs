using FluentValidation;
using Product.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Api.Validators
{
    public class ProductInsertRequestValidator : AbstractValidator<ProductInsertRequest>
    {
        public ProductInsertRequestValidator()
        {
            RuleFor(i => i.Name)
                .NotEmpty();

            RuleFor(i => i.Price)
                .GreaterThan(0);
        }
    }
}
