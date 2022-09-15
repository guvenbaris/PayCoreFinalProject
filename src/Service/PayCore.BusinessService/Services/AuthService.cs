using AutoMapper;
using Microsoft.Extensions.Options;
using PayCore.Application.Constant.RabbitMQ;
using PayCore.Application.Dtos.Auth;
using PayCore.Application.Dtos.Email;
using PayCore.Application.Exceptions;
using PayCore.Application.Interfaces.Jwt;
using PayCore.Application.Interfaces.RabbitMQ;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Application.Utilities.Appsettings;
using PayCore.Application.Utilities.Hash;
using PayCore.Application.Utilities.Results;
using PayCore.Application.Validations.AuthValidation;
using PayCore.Domain.Entities;
using System.Text;

namespace PayCore.BusinessService.Services
{
    public class AuthService : BusinessService<UserEntity, UserModel>, IUserService,IAuthService
    {
        private readonly IPublisherService _publisherService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IOptions<PayCoreAppSettings> _payCoreAppSettings;

        public AuthService(IUnitOfWork<UserEntity, UserModel> unitOfWork, IMapper mapper, IPublisherService publisherService, ITokenService tokenService,IOptions<PayCoreAppSettings> payCoreAppSettings) 
            : base(unitOfWork, mapper)
        {
   
            _publisherService = publisherService;
            _tokenService = tokenService;
            _mapper = mapper;
            _payCoreAppSettings = payCoreAppSettings;
        }

        public async Task<IDataResult> Login(LoginDto loginDto)
        {
            var loginValidator = new LoginDtoValidator();
            await loginValidator.ValidateAsync(loginDto);

            var userFind = base.GetFirstOrDefault(x=>x.Email == loginDto.Email);

            if (userFind == null)
                return new ErrorDataResult { ErrorMessage = "Please validate your informations that you provided." };

            byte[] passwordSalt = Encoding.UTF8.GetBytes(_payCoreAppSettings.Value.HashSettings.Salt);
            byte[] passwordHash = Encoding.UTF8.GetBytes(userFind.Password);

            var checkPassword = HashingHelper.VerifyPasswordHash(loginDto.Password, passwordHash, passwordSalt);

            if (!checkPassword)
            {
                var email = new EmailToSend
                {
                    To = userFind.Email,
                    Subject = "Welcome",
                    Body = "Hope you have a good time on our site",
                };
                _publisherService.Publish(email, RabbitMqConstants.MailSendQueue);

                return new ErrorDataResult { ErrorMessage = "Please validate your informations that you provided." };
            }

            var userEntity = _mapper.Map<UserEntity>(userFind);
            userEntity.LastActivity = DateTime.Now;

            var updateResult = base.Update(_mapper.Map<UserModel>(userEntity));

            if (!updateResult.IsSuccess)
                return updateResult;

            return _tokenService.GenerateToken(userEntity);
        }

        public async Task<IDataResult> Register(RegisterDto registerDto)
        {
            var validator = new RegisterDtoValidator();
            await validator.ValidateAsync(registerDto);

            var user = new UserModel
            {
                Email = registerDto.Email,
                Role = "member"
            };

            var emailChecker = GetFirstOrDefault(x=>x.Email == registerDto.Email);

            if (emailChecker is not null)
                throw new CustomException("Email already exists");

            byte[] passwordHash;
            byte[] passwordSalt = Encoding.UTF8.GetBytes(_payCoreAppSettings.Value.HashSettings.Salt);
            HashingHelper.CreatePasswordHash(registerDto.Password, out passwordHash,passwordSalt);

            user.Password = Encoding.UTF8.GetString(passwordHash, 0, passwordHash.Length);

            return base.Add(user);
        }
    }
}
