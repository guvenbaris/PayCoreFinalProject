using PayCore.Application.Dtos.Auth;
using PayCore.Application.Utilities.Results;
using PayCore.Domain.Jwt;

namespace PayCore.Application.Interfaces.Services;

public interface IAuthService
{
    Task<IDataResult> Register(RegisterDto registerDto);
    Task<IDataResult> Login(LoginDto loginDto);
}
