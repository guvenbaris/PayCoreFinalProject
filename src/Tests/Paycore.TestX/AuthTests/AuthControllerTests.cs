using Microsoft.AspNetCore.Mvc;
using Moq;
using Paycore.WebAPI.Controllers;
using PayCore.Application.Dtos.Auth;
using PayCore.Application.Interfaces.HelperServices;
using PayCore.Application.Utilities.Results;
using Xunit;

namespace Paycore.TestX.AuthTests
{
    public class AuthControllerTest
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AuthController _authController;

        public LoginDto LoginDtoTest { get; set; } = new LoginDto {Email="test@gmail.com",Password="test123test" };
            
        public RegisterDto RegisterDtoTest { get; set; } = new RegisterDto { Email = "test@gmail.com", Password = "test123test" };

        public AuthControllerTest()
        {
            _authServiceMock = new Mock<IAuthService>();
            _authController = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public void Login_ShouldReturn_DataResult()
        {
            _authServiceMock.Setup(x => x.Login(LoginDtoTest)).Returns(new DataResult { IsSuccess = true});
            var result = _authController.Login(LoginDtoTest) as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result!.Value);
            Assert.Equal(200, result!.StatusCode);
        }
        [Fact]
        public void Register_ShouldReturn_DataResult()
        {
            _authServiceMock.Setup(x => x.Register(RegisterDtoTest)).Returns(new DataResult { IsSuccess = true });
            var result = _authController.Register(RegisterDtoTest) as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result!.Value);
            Assert.Equal(200, result!.StatusCode);
        }
    }
}
