using FluentValidation;
using PayCore.Application.Models;

namespace PayCore.Application.Validations.OfferValidation
{
    public class OfferValidator : AbstractValidator<OfferModel>
    {
        public OfferValidator()
        {
            RuleFor(o=>o.OfferedPrice).NotNull().WithMessage("Offered price field is required.");
        }
    }
}
