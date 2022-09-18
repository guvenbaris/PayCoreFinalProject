using Microsoft.AspNetCore.Mvc;
using Moq;
using Paycore.WebAPI.Controllers;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Models;
using PayCore.Application.Utilities.Results;
using System.Collections.Generic;
using Xunit;

namespace Paycore.TestX.OfferTests
{
    public class OfferControllerTest
    {
        private readonly Mock<IOfferService> _offerServiceMock;
        private readonly OfferController _offerController;

        public List<OfferModel> OfferTestModels { get; set; } = new List<OfferModel> 
        {
            new OfferModel { Id = 1,OfferedPrice = 150,ProductId = 1,UserId = 0 },
            new OfferModel { Id = 2, OfferedPrice = 250, ProductId = 1, UserId = 0 } 
        };
        public OfferModel OfferTest { get; set; } = new OfferModel { Id = 1, OfferedPrice = 150, ProductId = 1, UserId = 0 };

        public OfferControllerTest()
        {
            _offerServiceMock = new Mock<IOfferService>();
            _offerController = new OfferController(_offerServiceMock.Object);
        }

        [Fact]
        public void GetAll_ShouldReturn_OfferList()
        {
            _offerServiceMock.Setup(x => x.GetAll()).Returns(OfferTestModels);
            var result = _offerController.GetAll() as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result!.Value);
            Assert.Equal(200, result!.StatusCode);
        }
        [Fact]
        public void GetById_ShouldReturn_Offer()
        {
            _offerServiceMock.Setup(x => x.GetById(1)).Returns(OfferTest);
            var result = _offerController.GetById(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result!.Value);
            Assert.Equal(200, result!.StatusCode);
        }

        [Fact]
        public void Add_ShouldReturn_AddedEntity()
        {
            _offerServiceMock.Setup(x => x.Add(OfferTest)).Returns(new DataResult { IsSuccess = true, Data = OfferTest });
            var result = _offerController.Add(OfferTest) as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result!.Value);
            Assert.Equal(200, result!.StatusCode);
        }
        [Fact]
        public void Update_ShouldReturn_UpdatedEntity()
        {
            _offerServiceMock.Setup(x => x.Update(OfferTest)).Returns(new DataResult { IsSuccess = true, Data = OfferTest });
            var result = _offerController.Update(OfferTest) as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result!.Value);
            Assert.Equal(200, result!.StatusCode);
        }
        [Fact]
        public void Delete_ShouldReturn_DeletedEntity()
        {
            _offerServiceMock.Setup(x => x.Delete(OfferTest.Id!.Value)).Returns(new DataResult { IsSuccess = true, Data = OfferTest });
            var result = _offerController.Delete(OfferTest.Id!.Value) as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result!.Value);
            Assert.Equal(200, result!.StatusCode);
        }

        [Fact]
        public void AproveTheOffer_ShouldReturn_DataResult()
        {
            _offerServiceMock.Setup(x => x.ApproveTheOffer(OfferTest.Id!.Value)).Returns(new DataResult { IsSuccess = true , Data = OfferTest });
            var result = _offerController.ApproveTheOffer(OfferTest.Id!.Value) as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result!.Value);
            Assert.Equal(200, result!.StatusCode);
        }
        [Fact]
        public void RejectTheOffer_ShouldReturn_DataResult()
        {
            _offerServiceMock.Setup(x => x.RejectTheOffer(OfferTest.Id!.Value)).Returns(new DataResult { IsSuccess = true, Data = OfferTest });
            var result = _offerController.RejectTheOffer(OfferTest.Id!.Value) as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result!.Value);
            Assert.Equal(200, result!.StatusCode);
        }
    }
}
