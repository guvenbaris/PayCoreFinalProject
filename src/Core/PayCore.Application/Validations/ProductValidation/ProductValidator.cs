using FluentValidation;
using PayCore.Application.Models;

namespace PayCore.Application.Validations.ProductValidation
{
    public class ProductValidator : AbstractValidator<ProductModel>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Product name field is required.")
                .MaximumLength(100).WithMessage("Product name length must be less than 100 character.");

            RuleFor(x=>x.Description).NotEmpty().WithMessage("Description field is required")
                .MaximumLength(500).WithMessage("Description length must be less than 500 character.");

            RuleFor(x=>x.Price).NotNull().NotEmpty().WithMessage("Price field is required");
        }
    }
}
