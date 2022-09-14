using AutoMapper;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Domain.Entities;

namespace PayCore.BusinessService.Services
{
    public class ManagerService : BusinessService<ManagerEntity, ManagerModel>, IManagerService
    {
        public ManagerService(IUnitOfWork<ManagerEntity, ManagerModel> unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public override IEnumerable<ManagerModel> GetAll()
        {
            return base.GetAll(); 
        }
    }

}
