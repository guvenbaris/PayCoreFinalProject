using Microsoft.AspNetCore.Mvc;
using Moq;
using Paycore.WebAPI.Controllers;
using PayCore.Application.Interfaces.HelperServices;
using PayCore.Application.Models;
using System.Collections.Generic;
using Xunit;

namespace Paycore.TestX.AccountDetailTests
{
    public class AccountDetailControllerTest
    {
        private readonly Mock<IAccountDetailService> _accountDetailServiceMock;
        private readonly AccountDetailController _accountDetailController;
        public List<OfferModel> AccountDetailTestModels { get; set; } =
            new List<OfferModel> { new OfferModel { Id = 1, OfferedPrice = 100,ProductId = 1,UserId = 0 }, new OfferModel { Id = 1, OfferedPrice = 100, ProductId = 1, UserId = 0 } };

        public UserModel userModelTest { get; set; } = new UserModel { Id = 0 };

        public AccountDetailControllerTest()
        {
            _accountDetailServiceMock = new Mock<IAccountDetailService>();
            _accountDetailController = new AccountDetailController(_accountDetailServiceMock.Object);
        }

        [Fact]
        public void GetUserProductOffer_ShouldReturn_OfferModelList() 
        {
            _accountDetailServiceMock.Setup(x=>x.GetUserProductOffers(userModelTest.Id!.Value)).Returns(AccountDetailTestModels);
            var result = _accountDetailController.GetUserProductOffers() as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result!.Value);
            Assert.Equal(200, result!.StatusCode);
        }

        [Fact]
        public void GetUserOffersOnProducts_ShouldReturn_OfferModelList()
        {
            _accountDetailServiceMock.Setup(x => x.GetUserOffersOnProducts(userModelTest.Id!.Value)).Returns(AccountDetailTestModels);
            var result = _accountDetailController.GetUserOffersOnProducts() as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result!.Value);
            Assert.Equal(200, result!.StatusCode);
        }
    }
}
