using AutoMapper;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Domain.Entities;

namespace PayCore.BusinessService.Services
{
    public class BrandService : BusinessService<BrandEntity, BrandModel>, IBrandService
    {
        public BrandService(IUnitOfWork<BrandEntity, BrandModel> unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
