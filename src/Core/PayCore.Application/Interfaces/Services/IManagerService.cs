using PayCore.Application.Models;
using PayCore.Domain.Entities;

namespace PayCore.Application.Interfaces.Services
{
    public interface IManagerService : IBusinessService<ManagerEntity,ManagerModel>
    {
        IEnumerable<ManagerModel> GetAll();
    }
}
