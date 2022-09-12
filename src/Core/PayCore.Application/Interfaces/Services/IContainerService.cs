using PayCore.Application.Interfaces.Sessions;
using PayCore.Application.Models;
using PayCore.Domain.Entities;

namespace PayCore.Application.Interfaces.Services
{
    public interface IContainerService : IBusinessService<Container,ContainerModel>
    {
    }
}
