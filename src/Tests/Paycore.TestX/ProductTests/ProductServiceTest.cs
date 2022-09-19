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
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Paycore.TestX.ProductTests
{
    public class ProductServiceTest
    {
        private readonly Mock<IUnitOfWork<ProductEntity, ProductModel>> _unitOfWorkMock;
        private readonly Mock<UnitOfWork<ProductEntity, ProductModel>> _unitOfWorkMockCrud;
        private readonly Mock<ICategoryService> _categoryMock;
        private readonly IMapper _mapper;


        public ProductModel ProductTestModel { get; set; } = new ProductModel { Id = 1, ProductName = "Test-1" ,CategoryId = 1,Price = 150,IsDeleted = false,
        Description = "Description",BrandId = 1,ColorId = 1};
        public ProductEntity ProductTestEntity { get; set; } = new ProductEntity { Id = 1, ProductName = "Test-1", Price = 150, IsDeleted = false,Description = "Description"};

        public ProductServiceTest()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new BaseMapperProfile()));
            IMapper mapper = new Mapper(configuration);
            _mapper = mapper;

            var sessionMock = new Mock<NHibernate.ISession>();
            var transactionMock = new Mock<ITransaction>();

            _unitOfWorkMock = new Mock<IUnitOfWork<ProductEntity, ProductModel>>();

            _unitOfWorkMockCrud = new Mock<UnitOfWork<ProductEntity, ProductModel>>(sessionMock.Object);
            _categoryMock = new Mock<ICategoryService>();

            sessionMock.Setup(x => x.BeginTransaction()).Returns(transactionMock.Object);
            transactionMock.Setup(x => x.Commit());
            transactionMock.Setup(x => x.Rollback());
            transactionMock.Setup(x => x.Dispose());
        }

        [Fact]
        public void Add_ShouldReturn_DataResult()
        {
            var categoryModel = new CategoryModel {Id = 1 , CategoryName = "Test-1" };
            _categoryMock.Setup(x=>x.GetById(categoryModel.Id.Value)).Returns(new CategoryModel { Id = 1});
           
            var productService = new ProductService(_unitOfWorkMockCrud.Object, _mapper, _categoryMock.Object);
            
            var result = productService.Add(ProductTestModel);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Update_ShouldReturn_DataResult()
        {
            var categoryModel = new CategoryModel { Id = 1, CategoryName = "Test-1" };
            _categoryMock.Setup(x => x.GetById(categoryModel.Id.Value)).Returns(new CategoryModel { Id = 1 });

            var productService = new ProductService(_unitOfWorkMockCrud.Object, _mapper, _categoryMock.Object);
            
            var result = productService.Update(ProductTestModel);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Delete_ShouldReturn_DataResult()
        {
            _unitOfWorkMock.Setup(x => x.Session.GetById(ProductTestEntity.Id)).Returns(ProductTestEntity);
            var productService = new ProductService(_unitOfWorkMock.Object, _mapper, _categoryMock.Object);
            var result = productService.Delete(1);

            Assert.NotNull(result.Data);
            Assert.True(result.IsSuccess);
            Assert.Equal(result.Data!.Id, ProductTestModel.Id);
        }

        [Fact]
        public void Get_ShouldReturn_ProductModel()
        {
            var productService = new ProductService(_unitOfWorkMock.Object, _mapper, _categoryMock.Object);
            _unitOfWorkMock.Setup(x => x.Session.GetById(ProductTestEntity.Id)).Returns(ProductTestEntity);
            var result = productService.GetById(ProductTestEntity.Id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, ProductTestModel.Id);
        }

        [Fact]
        public void GetAll_ShouldReturn_ProductModelList()
        {
            var productList = new List<ProductEntity> { ProductTestEntity };

            var productService = new ProductService(_unitOfWorkMock.Object, _mapper, _categoryMock.Object);
            _unitOfWorkMock.Setup(x => x.Session.Queries).Returns(productList.AsQueryable());

            var result = productService.GetAll();

            Assert.NotNull(result);
            Assert.Equal(result.First().ProductName, productList.First().ProductName);
        }

        [Fact]
        public void SellTheProduct()
        {
            var userModel = new UserModel { Id = 1 };
            _unitOfWorkMock.Setup(x => x.Session.GetById(1)).Returns(ProductTestEntity);
            
            var productMock = new Mock<ProductService>(_unitOfWorkMock.Object, _mapper, _categoryMock.Object) { CallBase = true };
            
             var result = productMock.Object.SellTheProduct(ProductTestModel.Id!.Value, userModel.Id.Value);
        }
    }
}
