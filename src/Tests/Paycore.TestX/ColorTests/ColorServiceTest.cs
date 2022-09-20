using AutoMapper;
using Moq;
using NHibernate;
using PayCore.Application.AutoMapperProfiles;
using PayCore.Application.Interfaces.Cache;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Application.Utilities.Results;
using PayCore.BusinessService.Services;
using PayCore.Domain.Entities;
using PayCore.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using Xunit;

namespace Paycore.TestX.ColorTests
{
    public class ColorServiceTest
    {
 
        private readonly Mock<IUnitOfWork<ColorEntity, ColorModel>> _unitOfWorkMock;
        private readonly Mock<UnitOfWork<ColorEntity, ColorModel>> _unitOfWorkCrud;
        private readonly Mock<ICacheService> _cacheServiceMock;
        private readonly IMapper _mapper;

        public ColorModel ColorTestModel { get; set; } = new ColorModel { Id = 1, ColorName = "Test-1" };
        public ColorEntity ColorTestEntity { get; set; } = new ColorEntity { Id = 1, ColorName = "Test-1" };

        public ColorServiceTest()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new BaseMapperProfile()));
            IMapper mapper = new Mapper(configuration);
            _mapper = mapper;

            var sessionMock = new Mock<NHibernate.ISession>();
            var transactionMock = new Mock<ITransaction>();

            _unitOfWorkMock = new Mock<IUnitOfWork<ColorEntity, ColorModel>>();
            _cacheServiceMock = new Mock<ICacheService>();

            _unitOfWorkCrud = new Mock<UnitOfWork<ColorEntity, ColorModel>>(sessionMock.Object);      

            sessionMock.Setup(x => x.BeginTransaction()).Returns(transactionMock.Object);
            transactionMock.Setup(x => x.Commit());
            transactionMock.Setup(x => x.Rollback());
            transactionMock.Setup(x => x.Dispose());

        }

        [Fact]
        public void Add_ShouldReturn_DataResult()
        {
            _cacheServiceMock.Setup(x=>x.DeleteIfContainName("ColorService"));
            var colorServiceMock = new Mock<ColorService>(_unitOfWorkCrud.Object,_mapper, _cacheServiceMock.Object) { CallBase = true};
            var result = colorServiceMock.Object.Add(ColorTestModel);

            Assert.NotNull(result);
            Assert.IsType<SuccessDataResult>(result);
            Assert.True(result.IsSuccess);
        }
        [Fact]
        public void Update_ShouldReturn_DataResult()
        {
            _cacheServiceMock.Setup(x => x.DeleteIfContainName("ColorService"));
            var colorServiceMock = new Mock<ColorService>(_unitOfWorkCrud.Object, _mapper, _cacheServiceMock.Object) { CallBase = true };
            var result = colorServiceMock.Object.Update(ColorTestModel);

            Assert.NotNull(result);
            Assert.IsType<SuccessDataResult>(result);
            Assert.True(result.IsSuccess);
        }
        [Fact]
        public void Delete_ShouldReturn_DataResult()
        {
            _cacheServiceMock.Setup(x => x.DeleteIfContainName("ColorService"));
            _unitOfWorkMock.Setup(x => x.Session.GetById(ColorTestModel.Id.Value)).Returns(ColorTestEntity);
            var colorServiceMock = new Mock<ColorService>(_unitOfWorkMock.Object, _mapper, _cacheServiceMock.Object) { CallBase = true };
            var result = colorServiceMock.Object.Delete(ColorTestModel.Id.Value);

            Assert.NotNull(result);
            Assert.IsType<SuccessDataResult>(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void GetAll_ShouldReturn_ColorModelList()
        {
            _cacheServiceMock.Setup(x => x.InsertValue("testKey", "testValue", 0, 0));
            var colorServiceMock = new Mock<ColorService>(_unitOfWorkMock.Object, _mapper, _cacheServiceMock.Object) { CallBase = false };
            var result = colorServiceMock.Object.GetAll();

            Assert.NotNull(result);
        }

        [Fact]
        public void GetById_ShouldReturn_ColorModel()
        {
            _unitOfWorkMock.Setup(x => x.Session.GetById(ColorTestModel.Id.Value)).Returns(ColorTestEntity);
            var colorServiceMock = new Mock<ColorService>(_unitOfWorkMock.Object, _mapper, _cacheServiceMock.Object) { CallBase = true };
            var result = colorServiceMock.Object.GetById(ColorTestModel.Id.Value);

            Assert.NotNull(result);
        }

    }
}
