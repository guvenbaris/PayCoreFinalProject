﻿using FluentValidation;
using PayCore.Application.Dtos.Auth;

namespace PayCore.Application.Validations.AuthValidation
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(c => c.Email).NotEmpty().WithMessage("Please validate your informations that you provided.");
            RuleFor(c => c.Password).NotEmpty().WithMessage("Please validate your informations that you provided.");
            RuleFor(c => c.Password).MinimumLength(8).MaximumLength(20).NotEmpty();
        }
    }
}