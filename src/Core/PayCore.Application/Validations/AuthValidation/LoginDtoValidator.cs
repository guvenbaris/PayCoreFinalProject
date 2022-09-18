using FluentValidation;
using PayCore.Application.Constant.Auth;
using PayCore.Application.Dtos.Auth;

namespace PayCore.Application.Validations.AuthValidation
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(c => c.Email).NotEmpty().WithMessage(AuthConstants.PasswordOrMailError);
            RuleFor(c => c.Password).NotEmpty().WithMessage(AuthConstants.PasswordOrMailError);
            RuleFor(c => c.Password).MinimumLength(8).MaximumLength(20).WithMessage(AuthConstants.PasswordCharacterError);
        }
    }
}
