using Microsoft.AspNetCore.Mvc;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Models;
using PayCore.Application.Utilities.BaseApiTools;
using PayCore.Domain.Entities;

namespace Paycore.WebAPI.Controllers;

[Route("api/categories")]
public class CategoryController : BaseApiController<CategoryEntity,CategoryModel>
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService) : base(categoryService)
    {
        _categoryService = categoryService;
    }
}
