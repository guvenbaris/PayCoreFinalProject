using FluentValidation;
using PayCore.Application.Constant.Auth;
using PayCore.Application.Dtos.Auth;

namespace PayCore.Application.Validations.AuthValidation
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(c => c.Email).NotEmpty().WithMessage(AuthConstants.PasswordOrMailError);
            RuleFor(c => c.Password).NotEmpty().WithMessage(AuthConstants.PasswordOrMailError);
            RuleFor(c => c.Password).MinimumLength(8).MaximumLength(20).WithMessage(AuthConstants.PasswordCharacterError);
            RuleFor(c => c.FirstName).NotEmpty().WithMessage("First name field is required.").MaximumLength(50).WithMessage("First name must be less than 50 characters.");
            RuleFor(c => c.LastName).NotEmpty().WithMessage("Last name field is required.").MaximumLength(50).WithMessage("Last name must be less than 50 characters.");
            RuleFor(c=>c.Email).MaximumLength(150).WithMessage("Email must be less than 150 characters.").EmailAddress();
        }
    }
}
