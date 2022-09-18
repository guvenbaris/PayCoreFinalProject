using Microsoft.AspNetCore.Mvc;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Models;
using PayCore.Application.Utilities.BaseApiTools;
using PayCore.Domain.Entities;
using System.Security.Claims;

namespace Paycore.WebAPI.Controllers;

public class ProductController : BaseApiController<ProductEntity, ProductModel>
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService) : base(productService)
    {
        _productService = productService;
    }
    public override IActionResult Add([FromBody] ProductModel model)
    {
        model.UserId = Convert.ToInt64(User?.FindFirstValue(ClaimTypes.NameIdentifier));
        return ApiResponse(_productService.Add(model));
    }
    public override IActionResult Update([FromBody] ProductModel model)
    {
        model.UserId = Convert.ToInt64(User?.FindFirstValue(ClaimTypes.NameIdentifier));
        return ApiResponse(_productService.Update(model));
    }

    [HttpPut("SellTheProduct")]
    public IActionResult SellTheProduct(long productId)
    {
        var userId = Convert.ToInt64(User?.FindFirstValue(ClaimTypes.NameIdentifier));
        return ApiResponse(_productService.SellTheProduct(productId,userId));
    }
}
