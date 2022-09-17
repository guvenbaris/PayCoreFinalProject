using PayCore.Application.Models;
using PayCore.Application.Utilities.Results;

namespace PayCore.Application.Interfaces.Jwt
{
    public interface ITokenService
    {
        IDataResult GenerateToken(UserModel user);
    }
}
