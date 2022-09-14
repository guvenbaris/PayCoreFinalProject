using PayCore.Application.Utilities.Results;
using PayCore.Domain.Jwt;

namespace PayCore.Application.Interfaces.Jwt
{
    public interface ITokenService
    {
        IDataResult GenerateToken(TokenRequest tokenRequest);
    }
}
