using PayCore.Application.Utilities.Results;
using PayCore.Domain.Entities;

namespace PayCore.Application.Interfaces.Jwt
{
    public interface ITokenService
    {
        IDataResult GenerateToken(UserEntity user);
    }
}
