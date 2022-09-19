using FluentValidation;
using PayCore.Application.Models;

namespace PayCore.Application.Validations.BrandValidation;

public class BrandValidator : AbstractValidator<BrandModel>
{
    public BrandValidator()
    {
        RuleFor(o => o.BrandName).NotNull().WithMessage("Brand name field is required.");
        RuleFor(o => o.BrandName).MaximumLength(100).WithMessage("Brand name length must be less than 100 character.");
    }
}
