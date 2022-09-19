using FluentValidation;
using PayCore.Application.Models;

namespace PayCore.Application.Validations.ColorValidation;

public class ColorValidator : AbstractValidator<ColorModel>
{
    public ColorValidator()
    {
        RuleFor(o => o.ColorName).NotNull().WithMessage("Color name field is required.");
        RuleFor(o => o.ColorName).MaximumLength(100).WithMessage("Color name length must be less than 100 character.");
    }
}
