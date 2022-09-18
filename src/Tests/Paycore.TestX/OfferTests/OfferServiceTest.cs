using AutoMapper;
using Moq;
using NHibernate;
using PayCore.Application.AutoMapperProfiles;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Application.Utilities.Results;
using PayCore.BusinessService.Services;
using PayCore.Domain.Entities;
using PayCore.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace Paycore.TestX.OfferTests
{
    public class OfferServiceTest
    {
        private readonly Mock<UnitOfWork<OfferEntity, OfferModel>> _unitOfWorkCrud;
        private readonly Mock<IUnitOfWork<OfferEntity, OfferModel>> _unitOfWorkGet;
        private readonly IMapper _mapper;
        private readonly Mock<IProductService> _productService;

        public OfferModel OfferTestModel { get; set; } = new OfferModel { Id = 1, OfferedPrice = 150, ProductId = 1, UserId = 0 };
        public OfferEntity OfferTestEntity { get; set; } =
            new OfferEntity { Id = 1, OfferedPrice = 150, Product = new ProductEntity { Id = 1} , User = new UserEntity { Id = 1,} };

        public OfferServiceTest()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new BaseMapperProfile()));
            IMapper mapper = new Mapper(configuration);
            _mapper = mapper;

            var sessionMock = new Mock<NHibernate.ISession>();
            var transactionMock = new Mock<ITransaction>();

            _unitOfWorkCrud = new Mock<UnitOfWork<OfferEntity, OfferModel>>(sessionMock.Object);

            _unitOfWorkGet = new Mock<IUnitOfWork<OfferEntity, OfferModel>>();

            _unitOfWorkGet.Setup(x => x.Session.GetById(OfferTestModel.Id.Value)).Returns(OfferTestEntity);
            _productService = new Mock<IProductService>();
           
            sessionMock.Setup(x => x.BeginTransaction()).Returns(transactionMock.Object);
            transactionMock.Setup(x => x.Commit());
            transactionMock.Setup(x => x.Rollback());
            transactionMock.Setup(x => x.Dispose());
        }

        [Fact]
        public void Add_ShouldReturn_DataResult()
        {
            _productService.Setup(x=>x.GetById(1)).Returns(new ProductModel { Id = 1,Price = 80,IsOfferable = true });
            var offerService = new OfferService(_unitOfWorkCrud.Object, _mapper,_productService.Object);
            var result = offerService.Add(OfferTestModel);

            Assert.NotNull(result.Data);
            Assert.True(result.IsSuccess);
            Assert.Equal(result.Data!.Id, OfferTestModel.Id);
        }

        [Fact]
        public void Update_ShouldReturn_DataResult()
        {
            _productService.Setup(x => x.GetById(1)).Returns(new ProductModel { Id = 1, Price = 80, IsOfferable = true });
            var offerService = new OfferService(_unitOfWorkCrud.Object, _mapper, _productService.Object);
            var result = offerService.Update(OfferTestModel);

            Assert.NotNull(result.Data);
            Assert.True(result.IsSuccess);
            Assert.Equal(result.Data!.Id, OfferTestModel.Id);
        }

        [Fact]
        public void Delete_ShouldReturn_DataResult()
        {
            _unitOfWorkGet.Setup(x => x.Session.GetById(OfferTestEntity.Id)).Returns(OfferTestEntity);
            var offerService = new OfferService(_unitOfWorkGet.Object, _mapper, _productService.Object);
            var result = offerService.Delete(1);

            Assert.NotNull(result.Data);
            Assert.True(result.IsSuccess);
            Assert.Equal(result.Data!.Id, OfferTestModel.Id);
        }

        [Fact]
        public void Get_ShouldReturn_OfferModel()
        {
            var offerService = new OfferService(_unitOfWorkGet.Object, _mapper, _productService.Object);
            _unitOfWorkGet.Setup(x => x.Session.GetById(OfferTestEntity.Id)).Returns(OfferTestEntity);
            var result = offerService.GetById(OfferTestEntity.Id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, OfferTestModel.Id);
        }

        [Fact]
        public void GetAll_ShouldReturn_OfferModelList()
        {
            var categoryList = new List<OfferEntity> { OfferTestEntity };

            var offerService = new OfferService(_unitOfWorkGet.Object, _mapper, _productService.Object);
            _unitOfWorkGet.Setup(x => x.Session.Queries).Returns(categoryList.AsQueryable());

            var result = offerService.GetAll();

            Assert.NotNull(result);
            Assert.Equal(result.First().Id, categoryList.First().Id);
        }
        [Fact]
        public void ApproveTheOffer_ShouldReturn_DataResult()
        {
            var productModel = new ProductModel { Id = 1, Price = 80, IsOfferable = true , IsSold = false,UserId = 1};
            _productService.Setup(x => x.GetById(1)).Returns(productModel);
            _productService.Setup(x => x.Update(productModel)).Returns(new DataResult { IsSuccess = true });
            
            var offerMock = new Mock<OfferService> (_unitOfWorkGet.Object, _mapper, _productService.Object) { CallBase = true};
            offerMock.Setup(x => x.GetById(OfferTestModel.Id!.Value)).Returns(OfferTestModel);

            var result = offerMock.Object.ApproveTheOffer(OfferTestModel.Id!.Value);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void RejectTheOffer_ShouldReturn_DataResult()
        {
            var productModel = new ProductModel { Id = 1, Price = 80, IsOfferable = true, IsSold = false, UserId = 1 };
            _productService.Setup(x => x.GetById(1)).Returns(productModel);
            _productService.Setup(x => x.Delete(productModel.Id.Value)).Returns(new DataResult { IsSuccess = true });

            var offerMock = new Mock<OfferService>(_unitOfWorkGet.Object, _mapper, _productService.Object) { CallBase = true };
            offerMock.Setup(x => x.GetById(1)).Returns(OfferTestModel);
            offerMock.CallBase = true;

            var result = offerMock.Object.RejectTheOffer(OfferTestModel.Id!.Value);

            Assert.True(result.IsSuccess);
        }

    }
}
