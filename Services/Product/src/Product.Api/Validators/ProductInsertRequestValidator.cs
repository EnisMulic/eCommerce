﻿using FluentValidation;
using Product.Contracts.Requests;

namespace Product.Api.Validators
{
    public class ProductInsertRequestValidator : AbstractValidator<ProductInsertRequest>
    {
        public ProductInsertRequestValidator()
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
