using PayCore.Application.Dtos.Auth;
using PayCore.Application.Utilities.Results;

namespace PayCore.Application.Interfaces.HelperServices;

public interface IAuthService
{
    IDataResult Register(RegisterDto registerDto);
    IDataResult Login(LoginDto loginDto);
}
