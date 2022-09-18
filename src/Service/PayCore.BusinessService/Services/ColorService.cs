using AutoMapper;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Domain.Entities;

namespace PayCore.BusinessService.Services
{
    public class ColorService : BusinessService<ColorEntity, ColorModel>, IColorService
    {
        public ColorService(IUnitOfWork<ColorEntity, ColorModel> unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
