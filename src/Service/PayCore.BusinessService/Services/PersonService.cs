using AutoMapper;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Domain.Entities;

namespace PayCore.BusinessService.Services
{
    public class PersonService : BusinessService<PersonEntity, PersonModel>, IPersonService
    {
        public PersonService(IUnitOfWork<PersonEntity, PersonModel> unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public override IEnumerable<PersonModel> GetAll()
        {
            return base.GetAll();
        }
    }
}
