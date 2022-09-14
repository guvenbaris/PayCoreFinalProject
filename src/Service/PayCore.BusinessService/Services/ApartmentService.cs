using AutoMapper;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Domain.Entities;

namespace PayCore.BusinessService.Services
{
    public class ApartmentService : BusinessService<ApartmentEntity, ApartmentModel>, IApartmentService
    {
        public ApartmentService(IUnitOfWork<ApartmentEntity, ApartmentModel> unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public override IEnumerable<ApartmentModel> GetAll()
        {
            return base.GetAll();
        }
    }
}
