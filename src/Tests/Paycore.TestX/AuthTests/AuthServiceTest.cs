using AutoMapper;
using Moq;
using PayCore.Application.AutoMapperProfiles;
using PayCore.Application.Dtos.Auth;
using PayCore.Application.Interfaces.Jwt;
using PayCore.Application.Interfaces.RabbitMQ;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Models;
using PayCore.Application.Utilities.Results;
using PayCore.BusinessService.HelperServices;
using PayCore.Domain.Entities;
using PayCore.Domain.Jwt;
using System;
using System.Linq.Expressions;
using Xunit;

namespace Paycore.TestX.AuthTests
{
    public class AuthServiceTest
    {
        private readonly Mock<IUserService> _userSeviceMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<IPublisherService> _publisherServiceMock;
        private readonly IMapper _mapper;

        public LoginDto loginDtoTest { get; set; } = new LoginDto {Email ="test@test.com",Password = "testtest" };
        public UserModel userModelTest { get; set; } 
            = new UserModel {Id = 0, Email = "test@test.com", Password = "44B85C98E94039C8A0A015F6D3A3449E", FirstName = "test", LastName = "XUnit" };
        public RegisterDto registerDtoTest { get; set; }
            = new RegisterDto { Email = "test@test.com", Password = "testtest",FirstName = "test",LastName ="XUnit" };

        public TokenResponse TokenResponseTest { get; set; } = new TokenResponse { AccessToken = "testtesttesttesttest",Expiration = DateTime.Now};

        public AuthServiceTest()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new BaseMapperProfile()));
            IMapper mapper = new Mapper(configuration);
            _mapper = mapper;

            _userSeviceMock = new Mock<IUserService>();
            _tokenServiceMock = new Mock<ITokenService>();
            _publisherServiceMock = new Mock<IPublisherService>();
        }

        [Fact]
        public void Login_ShouldReturn_DataResult()
        {
            _userSeviceMock.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<UserEntity, bool>>>())).Returns(userModelTest);
            _userSeviceMock.Setup(x => x.Update(userModelTest)).Returns(new DataResult { IsSuccess = true});

            _tokenServiceMock.Setup(x=>x.GenerateToken(userModelTest))
                .Returns(new SuccessDataResult { IsSuccess = true,Message = "Login is successful" });

            var authService = new AuthService(_mapper,_publisherServiceMock.Object,_tokenServiceMock.Object,_userSeviceMock.Object);
            var result = authService.Login(loginDtoTest) as DataResult;

            Assert.NotNull(result);
            Assert.True(result!.IsSuccess);
        }

        [Fact]
        public void Register_ShouldReturn_DataResult()
        {
            _userSeviceMock.Setup(x => x.Add(It.IsAny<UserModel>())).Returns(new DataResult { IsSuccess = true ,Message = "registered"});

            var authService = new AuthService(_mapper, _publisherServiceMock.Object, _tokenServiceMock.Object, _userSeviceMock.Object);
            var result = authService.Register(registerDtoTest);

            Assert.NotNull(result);
            Assert.True(result!.IsSuccess);
            Assert.IsType<SuccessDataResult>(result);
            Assert.IsNotType<ErrorDataResult>(result);
        }
    }
}
