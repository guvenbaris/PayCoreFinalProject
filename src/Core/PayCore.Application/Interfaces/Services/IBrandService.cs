using PayCore.Application.Models;
using PayCore.Domain.Entities;

namespace PayCore.Application.Interfaces.Services;

public interface IBrandService : IBusinessService<BrandEntity, BrandModel>
{
}