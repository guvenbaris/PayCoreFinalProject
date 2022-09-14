using PayCore.Application.Models;
using PayCore.Domain.Entities;

namespace PayCore.Application.Interfaces.Services
{
    public interface IApartmentService : IBusinessService<ApartmentEntity, ApartmentModel>
    {
        IEnumerable<ApartmentModel> GetAll();
    }
}
