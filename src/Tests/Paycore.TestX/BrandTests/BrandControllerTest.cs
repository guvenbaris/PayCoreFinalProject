using Microsoft.AspNetCore.Mvc;
using Moq;
using Paycore.WebAPI.Controllers;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Models;
using PayCore.Application.Utilities.Results;
using System.Collections.Generic;
using Xunit;

namespace Paycore.TestX.BrandTests;

public class BrandControllerTest
{
    private readonly Mock<IBrandService> _brandServiceMock;
    private readonly BrandController _brandController;
    public List<BrandModel> BrandTestModels { get; set; } =
        new List<BrandModel> { new BrandModel { Id = 1, BrandName = "Test-1" }, new BrandModel { Id = 2, BrandName = "Test-2" } };
    public BrandModel BrandTest { get; set; } = new BrandModel { Id = 1, BrandName = "Test-1" };

    public BrandControllerTest()
    {
        _brandServiceMock = new Mock<IBrandService>();
        _brandController = new BrandController(_brandServiceMock.Object);
    }

    [Fact]
    public void GetAll_ShouldReturn_BrandList()
    {
        _brandServiceMock.Setup(x => x.GetAll()).Returns(BrandTestModels);
        var result = _brandController.GetAll() as OkObjectResult;

        Assert.NotNull(result);
        Assert.NotNull(result!.Value);
        Assert.Equal(200, result!.StatusCode);
    }
    [Fact]
    public void GetById_ShouldReturn_Brand()
    {
        _brandServiceMock.Setup(x => x.GetById(1)).Returns(BrandTest);
        var result = _brandController.GetById(1) as OkObjectResult;

        Assert.NotNull(result);
        Assert.NotNull(result!.Value);
        Assert.Equal(200, result!.StatusCode);
    }

    [Fact]
    public void Add_ShouldReturn_AddedEntity()
    {
        _brandServiceMock.Setup(x => x.Add(BrandTest)).Returns(new DataResult { IsSuccess = true, Data = BrandTest });
        var result = _brandController.Add(BrandTest) as OkObjectResult;

        Assert.NotNull(result);
        Assert.NotNull(result!.Value);
        Assert.Equal(200, result!.StatusCode);
    }
    [Fact]
    public void Update_ShouldReturn_UpdatedEntity()
    {
        _brandServiceMock.Setup(x => x.Update(BrandTest)).Returns(new DataResult { IsSuccess = true, Data = BrandTest });
        var result = _brandController.Update(BrandTest) as OkObjectResult;

        Assert.NotNull(result);
        Assert.NotNull(result!.Value);
        Assert.Equal(200, result!.StatusCode);
    }
    [Fact]
    public void Delete_ShouldReturn_DeletedEntity()
    {
        _brandServiceMock.Setup(x => x.Delete(BrandTest.Id!.Value)).Returns(new DataResult { IsSuccess = true, Data = BrandTest });
        var result = _brandController.Delete(BrandTest.Id!.Value) as OkObjectResult;

        Assert.NotNull(result);
        Assert.NotNull(result!.Value);
        Assert.Equal(200, result!.StatusCode);
    }

}
