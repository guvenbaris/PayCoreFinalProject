using AutoMapper;
using FluentValidation;
using PayCore.Application.Constant.Auth;
using PayCore.Application.Dtos.Auth;
using PayCore.Application.Dtos.Email;
using PayCore.Application.Enums;
using PayCore.Application.Extensions;
using PayCore.Application.Interfaces.HelperServices;
using PayCore.Application.Interfaces.Jwt;
using PayCore.Application.Interfaces.RabbitMQ;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Models;
using PayCore.Application.Utilities.Hash;
using PayCore.Application.Utilities.Results;
using PayCore.Application.Validations.AuthValidation;
using PayCore.Application.ViewModel.User;
using PayCore.Domain.Jwt;

namespace PayCore.BusinessService.HelperServices;

public class AuthService : IAuthService
{
    private readonly IPublisherService _publisherService;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public AuthService(IMapper mapper, IPublisherService publisherService, ITokenService tokenService, IUserService userService)
    {
        _publisherService = publisherService;
        _tokenService = tokenService;
        _mapper = mapper;
        _userService = userService;
    }

    public IDataResult Login(LoginDto loginDto)
    {
        var loginValidator = new LoginDtoValidator();
        loginValidator.ValidateAndThrow(loginDto);

        var userFind = _userService.GetFirstOrDefault(x=>x.Email == loginDto.Email);

        if (userFind == null)
            return new ErrorDataResult { ErrorMessage = AuthConstants.PasswordOrMailError };

        var verifyPassword =  HashingHelper.CreatePasswordHash(loginDto.Password, loginDto.Email);

        if (!HashingHelper.VerifyPasswordHash(verifyPassword, userFind.Password))
            return new ErrorDataResult { ErrorMessage = AuthConstants.PasswordOrMailError };

        var accessControl = AccessRightControl(userFind);

        if (!accessControl.IsSuccess)
            return accessControl;

        var tokenResult =  _tokenService.GenerateToken(userFind);

        if (tokenResult.IsSuccess)
            return tokenResult;

        return new ErrorDataResult { ErrorMessage = "Login is not successful" };
    }

    public IDataResult Register(RegisterDto registerDto)
    {
        var validator = new RegisterDtoValidator();
        validator.ValidateAndThrow(registerDto);

        var user = new UserModel
        {
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName  = registerDto.LastName,
        };

        user.Role = "member";
        user.LastActivity = DateTime.UtcNow;
              
        var emailChecker = _userService.GetFirstOrDefault(x=>x.Email == registerDto.Email);

        if (emailChecker.IsNotNull())
            return new ErrorDataResult { ErrorMessage = AuthConstants.PasswordOrMailError };

        user.Password = HashingHelper.CreatePasswordHash(registerDto.Password,registerDto.Email);

        var userAdded = _userService.Add(user);

        if (!userAdded.IsSuccess)
            return userAdded;

        return new SuccessDataResult {Message = "Sign up successful", Data = _mapper.Map<UserViewModel>(userAdded.Data) };
    }
    private IDataResult AccessRightControl(UserModel userFind)
    {
        userFind.AccessFailedCount += 1;

        var updateResult = _userService.Update(userFind);
        if (!updateResult.IsSuccess)
            return updateResult;

        if (userFind.AccessFailedCount == 3)
        {
            userFind.LockoutEnabled = true;
            userFind.LastActivity = DateTime.UtcNow;

            var dataResult = _userService.Update(userFind);
            if (!dataResult.IsSuccess)
                return dataResult;

            var email = new EmailToSend
            {
                To = userFind.Email,
                Subject = "Account Blocked",
                Body = "Your account has been blocked for logging in 3 times wrong in a row."
            };
            _publisherService.PublishEmail(email, RabbitMqQueue.EmailSenderQueue.ToString());
            return new ErrorDataResult { ErrorMessage = "Your account has been blocked" };
        }
        return new SuccessDataResult();
    }
}
