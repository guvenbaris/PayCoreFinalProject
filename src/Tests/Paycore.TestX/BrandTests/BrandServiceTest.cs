using AutoMapper;
using Moq;
using NHibernate;
using PayCore.Application.AutoMapperProfiles;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.BusinessService.Services;
using PayCore.Domain.Entities;
using PayCore.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Paycore.TestX.BrandTests
{

    public class BrandServiceTest
    {
        private readonly Mock<UnitOfWork<BrandEntity, BrandModel>> _unitOfWorkCrud;
        private readonly Mock<IUnitOfWork<BrandEntity, BrandModel>> _unitOfWorkGet;
        private readonly IMapper _mapper;


        public BrandModel BrandTestModel { get; set; } = new BrandModel { Id = 1, BrandName = "Test-1" };
        public BrandEntity BrandTestEntity { get; set; } = new BrandEntity { Id = 1, BrandName = "Test-1" };

        public BrandServiceTest()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new BaseMapperProfile()));
            IMapper mapper = new Mapper(configuration);
            _mapper = mapper;

            var sessionMock = new Mock<NHibernate.ISession>();
            var transactionMock = new Mock<ITransaction>();

            _unitOfWorkCrud = new Mock<UnitOfWork<BrandEntity, BrandModel>>(sessionMock.Object);

            _unitOfWorkGet = new Mock<IUnitOfWork<BrandEntity, BrandModel>>();

            sessionMock.Setup(x => x.BeginTransaction()).Returns(transactionMock.Object);
            transactionMock.Setup(x => x.Commit());
            transactionMock.Setup(x => x.Rollback());
            transactionMock.Setup(x => x.Dispose());
        }

        [Fact]
        public void Add_ShouldReturn_DataResult()
        {
            var brandService = new BrandService(_unitOfWorkCrud.Object, _mapper);
            var result = brandService.Add(BrandTestModel);

            Assert.NotNull(result.Data);
            Assert.True(result.IsSuccess);
            Assert.Equal(result.Data!.Id, BrandTestModel.Id);
        }

        [Fact]
        public void Update_ShouldReturn_DataResult()
        {
            var brandService = new BrandService(_unitOfWorkCrud.Object, _mapper);
            var result = brandService.Update(BrandTestModel);

            Assert.NotNull(result.Data);
            Assert.True(result.IsSuccess);
            Assert.Equal(result.Data!.Id, BrandTestModel.Id);
        }

        [Fact]
        public void Delete_ShouldReturn_DataResult()
        {
            _unitOfWorkGet.Setup(x => x.Session.GetById(BrandTestEntity.Id)).Returns(BrandTestEntity);
            var brandService = new BrandService(_unitOfWorkGet.Object, _mapper);
            var result = brandService.Delete(1);

            Assert.NotNull(result.Data);
            Assert.True(result.IsSuccess);
            Assert.Equal(result.Data!.Id, BrandTestModel.Id);
        }

        [Fact]
        public void Get_ShouldReturn_BrandModel()
        {
            var brandService = new BrandService(_unitOfWorkGet.Object, _mapper);
            _unitOfWorkGet.Setup(x => x.Session.GetById(BrandTestEntity.Id)).Returns(BrandTestEntity);
            var result = brandService.GetById(BrandTestEntity.Id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, BrandTestModel.Id);
        }

        [Fact]
        public void GetAll_ShouldReturn_BrandModelList()
        {
            var brandList = new List<BrandEntity> { BrandTestEntity };

            var brandService = new BrandService(_unitOfWorkGet.Object, _mapper);
            _unitOfWorkGet.Setup(x => x.Session.Queries).Returns(brandList.AsQueryable());

            var result = brandService.GetAll();

            Assert.NotNull(result);
            Assert.Equal(result.First().BrandName, brandList.First().BrandName);
        }
    }
}
