using Microsoft.AspNetCore.Mvc;
using Moq;
using Paycore.WebAPI.Controllers;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Models;
using PayCore.Application.Utilities.Results;
using System.Collections.Generic;
using Xunit;

namespace Paycore.TestX.ProductTests
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly ProductController _productController;
        public List<ProductModel> ProductTestModels { get; set; } =
            new List<ProductModel> { new ProductModel { Id = 1, ProductName = "Test-1",CategoryId = 1, }, new ProductModel { Id = 2, ProductName = "Test-2" } };
        public ProductModel ProductTest { get; set; } = new ProductModel { Id = 1, ProductName = "Test-1" };

        public ProductControllerTest()
        {
            _productServiceMock = new Mock<IProductService>();
            _productController = new ProductController(_productServiceMock.Object);
        }

        [Fact]
        public void GetAll_ShouldReturn_ProductList()
        {
            _productServiceMock.Setup(x => x.GetAll()).Returns(ProductTestModels);
            var result = _productController.GetAll() as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result!.Value);
            Assert.Equal(200, result!.StatusCode);
        }
        [Fact]
        public void GetById_ShouldReturn_Product()
        {
            _productServiceMock.Setup(x => x.GetById(1)).Returns(ProductTest);
            var result = _productController.GetById(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result!.Value);
            Assert.Equal(200, result!.StatusCode);
        }

        [Fact]
        public void Add_ShouldReturn_DataResult()
        {
            _productServiceMock.Setup(x => x.Add(ProductTest)).Returns(new DataResult { IsSuccess = true, Data = ProductTest });
            var result = _productController.Add(ProductTest) as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result!.Value);
            Assert.Equal(200, result!.StatusCode);
        }
        [Fact]
        public void Update_ShouldReturn_DataResult()
        {
            _productServiceMock.Setup(x => x.Update(ProductTest)).Returns(new DataResult { IsSuccess = true, Data = ProductTest });
            var result = _productController.Update(ProductTest) as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result!.Value);
            Assert.Equal(200, result!.StatusCode);
        }
        [Fact]
        public void Delete_ShouldReturn_DataResult()
        {
            _productServiceMock.Setup(x => x.Delete(ProductTest.Id!.Value)).Returns(new DataResult { IsSuccess = true, Data = ProductTest });
            var result = _productController.Delete(ProductTest.Id!.Value) as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result!.Value);
            Assert.Equal(200, result!.StatusCode);
        }
        [Fact]
        public void SellProduct_ShouldReturn_DataResult()
        {
            var userTest = new UserModel {Id = 0 };
            _productServiceMock
                .Setup(x => x.SellTheProduct(ProductTest.Id!.Value, userTest.Id.Value))
                .Returns(new DataResult { IsSuccess = true, Data = ProductTest });

            var result = _productController.SellTheProduct(ProductTest.Id!.Value) as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result!.Value);
            Assert.Equal(200, result!.StatusCode);
        }
    }
}
