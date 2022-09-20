using Microsoft.AspNetCore.Mvc;
using Moq;
using Paycore.WebAPI.Controllers;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Models;
using PayCore.Application.Utilities.Results;
using System.Collections.Generic;
using Xunit;

namespace Paycore.TestX.ColorTests;

public class ColorControllerTest
{
    private readonly Mock<IColorService> _colorServiceMock;
    private readonly ColorController _colorController;
    public List<ColorModel> ColorTestModels { get; set; } =
        new List<ColorModel> { new ColorModel { Id = 1, ColorName = "Test-1" }, new ColorModel { Id = 2, ColorName = "Test-2" } };
    public ColorModel ColorTest { get; set; } = new ColorModel { Id = 1, ColorName = "Test-1" };

    public ColorControllerTest()
    {
        _colorServiceMock = new Mock<IColorService>();
        _colorController = new ColorController(_colorServiceMock.Object);
    }

    [Fact]
    public void GetAll_ShouldReturn_ColorList()
    {
        _colorServiceMock.Setup(x => x.GetAll()).Returns(ColorTestModels);
        var result = _colorController.GetAll() as OkObjectResult;

        Assert.NotNull(result);
        Assert.NotNull(result!.Value);
        Assert.Equal(200, result!.StatusCode);
    }
    [Fact]
    public void GetById_ShouldReturn_Color()
    {
        _colorServiceMock.Setup(x => x.GetById(1)).Returns(ColorTest);
        var result = _colorController.GetById(1) as OkObjectResult;

        Assert.NotNull(result);
        Assert.NotNull(result!.Value);
        Assert.Equal(200, result!.StatusCode);
    }

    [Fact]
    public void Add_ShouldReturn_AddedEntity()
    {
        _colorServiceMock.Setup(x => x.Add(ColorTest)).Returns(new DataResult { IsSuccess = true, Data = ColorTest });
        var result = _colorController.Add(ColorTest) as OkObjectResult;

        Assert.NotNull(result);
        Assert.NotNull(result!.Value);
        Assert.Equal(200, result!.StatusCode);
    }
    [Fact]
    public void Update_ShouldReturn_UpdatedEntity()
    {
        _colorServiceMock.Setup(x => x.Update(ColorTest)).Returns(new DataResult { IsSuccess = true, Data = ColorTest });
        var result = _colorController.Update(ColorTest) as OkObjectResult;

        Assert.NotNull(result);
        Assert.NotNull(result!.Value);
        Assert.Equal(200, result!.StatusCode);
    }
    [Fact]
    public void Delete_ShouldReturn_DeletedEntity()
    {
        _colorServiceMock.Setup(x => x.Delete(ColorTest.Id!.Value)).Returns(new DataResult { IsSuccess = true, Data = ColorTest });
        var result = _colorController.Delete(ColorTest.Id!.Value) as OkObjectResult;

        Assert.NotNull(result);
        Assert.NotNull(result!.Value);
        Assert.Equal(200, result!.StatusCode);
    }

}
