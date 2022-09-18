using PayCore.Application.Interfaces.Services;
using PayCore.Application.Models;
using PayCore.Application.Utilities.BaseApiTools;
using PayCore.Domain.Entities;

namespace Paycore.WebAPI.Controllers;

public class ColorController : BaseApiController<ColorEntity, ColorModel>
{
    private readonly IColorService _colorService;
    public ColorController(IColorService colorService) : base(colorService)
    {
        _colorService = colorService;
    }
}
