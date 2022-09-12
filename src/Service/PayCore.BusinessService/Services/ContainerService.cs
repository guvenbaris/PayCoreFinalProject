using AutoMapper;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Domain.Entities;

namespace PayCore.BusinessService.Services
{
    public class ContainerService : BusinessService<Container, ContainerModel>, IContainerService
    {
        public ContainerService(IUnitOfWork<Container, ContainerModel> unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
