using PayCore.Application.Models;
using PayCore.Domain.Entities;

namespace PayCore.Application.Interfaces.Services
{

    public interface IPersonService : IBusinessService<PersonEntity, PersonModel>
    {
        IEnumerable<PersonModel> GetAll();
    }
}
