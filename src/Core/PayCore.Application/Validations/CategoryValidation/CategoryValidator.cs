using FluentValidation;
using PayCore.Application.Models;

namespace PayCore.Application.Validations.CategoryValidation
{
    public class CategoryValidator : AbstractValidator<CategoryModel>
    {
        public CategoryValidator()
        {
            RuleFor(o => o.CategoryName).NotNull().WithMessage("Category name field is required.");
            RuleFor(o => o.CategoryName).MaximumLength(100).WithMessage("Category name length must be less than 100 character.");
        }
    }
}
