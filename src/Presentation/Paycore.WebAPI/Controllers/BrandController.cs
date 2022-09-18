using PayCore.Application.Interfaces.Services;
using PayCore.Application.Models;
using PayCore.Application.Utilities.BaseApiTools;
using PayCore.Domain.Entities;

namespace Paycore.WebAPI.Controllers;

public class BrandController : BaseApiController<BrandEntity, BrandModel>
{
    private readonly IBrandService _brandService;
    public BrandController(IBrandService brandService) : base(brandService)
    {
        _brandService = brandService;
    }
}