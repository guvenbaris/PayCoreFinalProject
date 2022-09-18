using AutoMapper;
using Moq;
using NHibernate;
using PayCore.Application.Dtos.Auth;
using PayCore.Application.Interfaces.Jwt;
using PayCore.Application.Interfaces.RabbitMQ;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.Sessions;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Application.Utilities.Results;
using PayCore.BusinessService.Services;
using PayCore.Domain.Entities;
using PayCore.Infrastructure.UnitOfWork;
using Xunit;

namespace Paycore.TestX
{
    public class AuthServiceTest 
    {
        [Fact]
        public void Register_ShouldReturn_SuccessDataResult()
        {
            //var _userServiceMoq = new Mock<IUserService>();
            //var _tokenServiceMoq = new Mock<ITokenService>();
            //var _publisherService = new Mock<IPublisherService>();
            //var _mapper = new Mock<IMapper>();
            //var authService = new Mock<IAuthService>();
            //var unitOfWork = new Mock<IUnitOfWork<UserEntity,UserModel>>();
            //var session = new Mock<ISession>();
            //var mapperSession = new Mock<IMapperSession<UserEntity>>();

            //var unitofWorkService = new UnitOfWork<UserEntity, UserModel>(session.Object);

            //var userService = new UserService(unitofWorkService, _mapper.Object);

            //var registerDto = new RegisterDto { Email = "test_test@gmail.com",Password = "12345678",FirstName ="test",LastName="test"};

            //var authServiceHub = authService.Setup(x => x.Register(registerDto)).Returns(new SuccessDataResult {Message = "Sign up successful"});

            //var deneme = new AuthService(_mapper.Object,_publisherService.Object,_tokenServiceMoq.Object, userService);
            //var result =  deneme.Register(registerDto);


        }
        
        [Fact]
        public void Login_ShouldReturn_Token()
        {

        }
    }
}
