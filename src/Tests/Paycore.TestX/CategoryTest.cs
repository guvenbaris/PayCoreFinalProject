using AutoMapper;
using Moq;
using NHibernate;
using Paycore.WebAPI.Controllers;
using PayCore.Application.AutoMapperProfiles;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.Sessions;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.BusinessService.Services;
using PayCore.Domain.Entities;
using PayCore.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Paycore.TestX
{
    public class CategoryTest 
    {
        [Fact]
        public void Add_ShouldReturn_Category()
        {
            var myProfile = new BaseMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);


            ////var _mockUnitOfWork = new Mock<IUnitOfWork<CategoryEntity,CategoryModel>>();
            //var _unitOfWork = new Mock<UnitOfWork<CategoryEntity, CategoryModel>>(_session.Object);

            //var _mockCategoryService = new CategoryService(_unitOfWork.Object, mapper);

            //_unitOfWork.Setup(repo => repo.Add(new CategoryEntity { CategoryName = "Test1" }));


            //var data = _mockCategoryService.Add(new CategoryModel { CategoryName = "Test1" });

            // ********* ÇALIŞIYOR ********* GETALL CategoryController
            //var service = new Mock<ICategoryService>();
            //var controller = new CategoryController(service.Object);
            //service.Setup(x=>x.GetAll()).Returns(new List<CategoryModel> { new CategoryModel { CategoryName = "1" } });
            //var deneme =  controller.GetAll();

            // ********* ÇALIŞIYOR ********* GETBYID CategoryService
            //var _mockUnitOfWork = new Mock<IUnitOfWork<CategoryEntity, CategoryModel>>();
            //var categoryService = new CategoryService(_mockUnitOfWork.Object, mapper);
            //_mockUnitOfWork.Setup(x => x.Session.GetById(1)).Returns(new CategoryEntity { Id = 1, CategoryName = "Test" });
            //var data = categoryService.GetById(1);

            /// ÇALIŞIYOR ************** ADD *************
            //var _mockUnitOfWork = new Mock<IUnitOfWork<CategoryEntity, CategoryModel>>();
            //var categoryService = new CategoryService(_mockUnitOfWork.Object, mapper);
            //_mockUnitOfWork.Setup(x => x.Add(new CategoryEntity { Id = 1, CategoryName = "Test" })).Returns(new CategoryEntity { Id = 1, CategoryName = "Test" });
            //var data = categoryService.Add(new CategoryModel { Id = 1, CategoryName = "Test" });

            var sessionMock = new Mock<NHibernate.ISession>();
            var transactionMock = new Mock<ITransaction>();
            var unitOfWork = new UnitOfWork<CategoryEntity,CategoryModel>(sessionMock.Object);

            sessionMock.Setup(x => x.BeginTransaction()).Returns(transactionMock.Object);
            transactionMock.Setup(x => x.Commit());
            transactionMock.Setup(x => x.Rollback());
            transactionMock.Setup(x => x.Dispose());


            var result = unitOfWork.Add(new CategoryEntity { Id = 1, CategoryName = "Test" });


        }
    }
}
