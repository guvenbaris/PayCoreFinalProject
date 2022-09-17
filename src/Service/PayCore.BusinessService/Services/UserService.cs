using AutoMapper;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Domain.Entities;

namespace PayCore.BusinessService.Services
{
    public class UserService : BusinessService<UserEntity, UserModel>, IUserService
    {
        public UserService(IUnitOfWork<UserEntity, UserModel> unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }

}
