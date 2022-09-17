using Microsoft.AspNetCore.Mvc;
using PayCore.Application.Dtos.Auth;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Utilities.BaseApiTools;

namespace Paycore.WebAPI.Controllers;

public class AuthController : BaseApiResponse
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginDto loginDto)
    {
        return ApiResponse(_authService.Login(loginDto));
    }

    [HttpPost("Register")]
    public IActionResult Register([FromBody] RegisterDto registerDto)
    {
        return ApiResponse(_authService.Register(registerDto));
    }
}
