using Microsoft.AspNetCore.Mvc;
using Moq;
using Paycore.WebAPI.Controllers;
using PayCore.Application.Interfaces.HelperServices;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Models;
using PayCore.BusinessService.HelperServices;
using PayCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace Paycore.TestX.AccountDetailTests
{
    public class AccountDetailServiceTest
    {
        private readonly Mock<IOfferService> _offerServiceMock;
        private readonly Mock<IProductService> _productServiceMock;

        public List<OfferModel> OfferModelTest { get; set; } =
            new List<OfferModel> { new OfferModel { Id = 1, OfferedPrice = 100, ProductId = 1, UserId = 0 }, new OfferModel { Id = 1, OfferedPrice = 100, ProductId = 1, UserId = 0 } };
        public List<ProductModel> ProductModelTest { get; set; } 
            = new List<ProductModel> { new ProductModel { Id = 1, }, new ProductModel { Id = 2, }, new ProductModel { Id = 3, } };
        public long UserIdTest { get; set; } = 1;

        public AccountDetailServiceTest()
        {
            _offerServiceMock = new Mock<IOfferService>();
            _productServiceMock = new Mock<IProductService>();
        }

        [Fact]
        public void GetUserOffersOnProducts_ShouldReturn_OfferModelList()
        {
            _productServiceMock.Setup(x => x.Where(It.IsAny<Expression<Func<ProductEntity, bool>>>())).Returns(ProductModelTest);
            _offerServiceMock.Setup(x=>x.SearchWithIn("product_id",It.IsAny<string>())).Returns(OfferModelTest);

            var accountDetailService = new AccountDetailService(_productServiceMock.Object,_offerServiceMock.Object);

            var result = accountDetailService.GetUserOffersOnProducts(UserIdTest);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }


        [Fact]
        public void GetUserProductOffers_ShouldReturn_OfferModelList()
        {
            _offerServiceMock.Setup(x => x.Where(It.IsAny<Expression<Func<OfferEntity, bool>>>())).Returns(OfferModelTest);

            var accountDetailService = new AccountDetailService(_productServiceMock.Object, _offerServiceMock.Object);

            var result = accountDetailService.GetUserProductOffers(UserIdTest);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

    }
}
