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

namespace Paycore.TestX.CategoryTests
{

    public class CategoryServiceTest
    {
        private readonly Mock<UnitOfWork<CategoryEntity, CategoryModel>> _unitOfWorkCrud;
        private readonly Mock<IUnitOfWork<CategoryEntity, CategoryModel>> _unitOfWorkGet;
        private readonly IMapper _mapper;

        public CategoryModel CategoryTestModel { get; set; } = new CategoryModel { Id = 1, CategoryName = "Test-1" };
        public CategoryEntity CategoryTestEntity { get; set; } = new CategoryEntity { Id = 1, CategoryName = "Test-1" };

        public CategoryServiceTest()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new BaseMapperProfile()));
            IMapper mapper = new Mapper(configuration);
            _mapper = mapper;

            var sessionMock = new Mock<NHibernate.ISession>();
            var transactionMock = new Mock<ITransaction>();

            _unitOfWorkCrud = new Mock<UnitOfWork<CategoryEntity, CategoryModel>>(sessionMock.Object);

            _unitOfWorkGet = new Mock<IUnitOfWork<CategoryEntity, CategoryModel>>();

            sessionMock.Setup(x => x.BeginTransaction()).Returns(transactionMock.Object);
            transactionMock.Setup(x => x.Commit());
            transactionMock.Setup(x => x.Rollback());
            transactionMock.Setup(x => x.Dispose());
        }

        [Fact]
        public void Add_ShouldReturn_DataResult()
        {
            var  categoryService = new CategoryService(_unitOfWorkCrud.Object, _mapper);
            var result = categoryService.Add(CategoryTestModel);

            Assert.NotNull(result.Data);
            Assert.True(result.IsSuccess);
            Assert.Equal(result.Data!.Id, CategoryTestModel.Id);
        }

        [Fact]
        public void Update_ShouldReturn_DataResult()
        {
            var categoryService = new CategoryService(_unitOfWorkCrud.Object, _mapper);
            var result = categoryService.Update(CategoryTestModel);
            
            Assert.NotNull(result.Data);
            Assert.True(result.IsSuccess); 
            Assert.Equal(result.Data!.Id, CategoryTestModel.Id);
        }

        [Fact]
        public void Delete_ShouldReturn_DataResult()
        {
            _unitOfWorkGet.Setup(x=>x.Session.GetById(CategoryTestEntity.Id)).Returns(CategoryTestEntity);
            var categoryService = new CategoryService(_unitOfWorkGet.Object, _mapper);
            var result = categoryService.Delete(1);

            Assert.NotNull(result.Data);
            Assert.True(result.IsSuccess);
            Assert.Equal(result.Data!.Id, CategoryTestModel.Id);
        }

        [Fact]
        public void Get_ShouldReturn_CategoryModel()
        {
            var categoryService = new CategoryService(_unitOfWorkGet.Object, _mapper);
            _unitOfWorkGet.Setup(x => x.Session.GetById(CategoryTestEntity.Id)).Returns(CategoryTestEntity);
            var result = categoryService.GetById(CategoryTestEntity.Id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, CategoryTestModel.Id);
        }

        [Fact]
        public void GetAll_ShouldReturn_CategoryModelList()
        {
            var categoryList = new List<CategoryEntity> { CategoryTestEntity };

            var categoryService = new CategoryService(_unitOfWorkGet.Object, _mapper);
            _unitOfWorkGet.Setup(x => x.Session.Queries).Returns(categoryList.AsQueryable());

            var result = categoryService.GetAll();

            Assert.NotNull(result);
            Assert.Equal(result.First().CategoryName , categoryList.First().CategoryName);
        }
    }
}
