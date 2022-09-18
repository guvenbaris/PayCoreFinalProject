using Microsoft.AspNetCore.Mvc;
using Moq;
using Paycore.WebAPI.Controllers;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Models;
using PayCore.Application.Utilities.Results;
using System.Collections.Generic;
using Xunit;

namespace Paycore.TestX.CategoryTests;

public class CategoryControllerTest
{
    private readonly Mock<ICategoryService> _categoryServiceMock;
    private readonly CategoryController _categoryController;
    public List<CategoryModel> CategoryTestModels { get; set; } = 
        new List<CategoryModel> { new CategoryModel { Id = 1 ,CategoryName = "Test-1"}, new CategoryModel { Id = 2, CategoryName = "Test-2" } };
    public CategoryModel CategoryTest { get; set; } = new CategoryModel { Id = 1, CategoryName = "Test-1" };

    public CategoryControllerTest()
    {
        _categoryServiceMock = new Mock<ICategoryService>();
        _categoryController = new CategoryController(_categoryServiceMock.Object);
    }

    [Fact]
    public void GetAll_ShouldReturn_CategoryList()
    {
        _categoryServiceMock.Setup(x => x.GetAll()).Returns(CategoryTestModels);
        var result = _categoryController.GetAll() as OkObjectResult;

        Assert.NotNull(result);
        Assert.NotNull(result!.Value);
        Assert.Equal(200, result!.StatusCode);
    }
    [Fact]
    public void GetById_ShouldReturn_Category()
    {
        _categoryServiceMock.Setup(x => x.GetById(1)).Returns(CategoryTest);
        var result = _categoryController.GetById(1) as OkObjectResult;

        Assert.NotNull(result);
        Assert.NotNull(result!.Value);
        Assert.Equal(200, result!.StatusCode);
    }

    [Fact]
    public void Add_ShouldReturn_AddedEntity()
    {
        _categoryServiceMock.Setup(x => x.Add(CategoryTest)).Returns(new DataResult { IsSuccess = true,Data = CategoryTest});
        var result = _categoryController.Add(CategoryTest) as OkObjectResult;

        Assert.NotNull(result);
        Assert.NotNull(result!.Value);
        Assert.Equal(200, result!.StatusCode);
    }
    [Fact]
    public void Update_ShouldReturn_UpdatedEntity()
    {
        _categoryServiceMock.Setup(x => x.Update(CategoryTest)).Returns(new DataResult { IsSuccess = true, Data = CategoryTest });
        var result = _categoryController.Update(CategoryTest) as OkObjectResult;

        Assert.NotNull(result);
        Assert.NotNull(result!.Value);
        Assert.Equal(200, result!.StatusCode);
    }
    [Fact]
    public void Delete_ShouldReturn_DeletedEntity()
    {
        _categoryServiceMock.Setup(x => x.Delete(CategoryTest.Id!.Value)).Returns(new DataResult { IsSuccess = true, Data = CategoryTest });
        var result = _categoryController.Delete(CategoryTest.Id!.Value) as OkObjectResult;

        Assert.NotNull(result);
        Assert.NotNull(result!.Value);
        Assert.Equal(200, result!.StatusCode);
    }

}
