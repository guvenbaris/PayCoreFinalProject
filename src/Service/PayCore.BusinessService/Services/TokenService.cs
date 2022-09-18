using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PayCore.Application.Interfaces.Jwt;
using PayCore.Application.Models;
using PayCore.Application.Utilities.Appsettings;
using PayCore.Application.Utilities.Results;
using PayCore.Domain.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PayCore.BusinessService.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<PayCoreAppSettings> _payCoreAppSettings;
        public TokenService(IOptions<PayCoreAppSettings> payCoreAppSettings)
        {
            _payCoreAppSettings = payCoreAppSettings;
        }

        public IDataResult GenerateToken(UserModel user)
        {
            var now = DateTime.UtcNow;
            string token = GetToken(user,now);

            TokenResponse tokenResponse = new TokenResponse
            {
                AccessToken = token,
                Expiration = now.AddMinutes(_payCoreAppSettings.Value.JwtSettings.TokenExpirationMinute),
                Role = user.Role,
                SessionTimeInSecond = _payCoreAppSettings.Value.JwtSettings.TokenExpirationMinute * 60
            };

           return new SuccessDataResult<TokenResponse> { Data = tokenResponse };
        }
        private string GetToken(UserModel user, DateTime date)
        {
            Claim[] claims = GetClaims(user);
            byte[] secret = Encoding.ASCII.GetBytes(_payCoreAppSettings.Value.JwtSettings.Key);

            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);

            var jwtToken = new JwtSecurityToken(
                _payCoreAppSettings.Value.JwtSettings.Issuer,
                shouldAddAudienceClaim ? _payCoreAppSettings.Value.JwtSettings.Audience : string.Empty,
                claims,
                expires: date.AddMinutes(_payCoreAppSettings.Value.JwtSettings.TokenExpirationMinute),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return accessToken;
        }
        private Claim[] GetClaims(UserModel user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("AccountId", user.Id.ToString()),
                new Claim("Email",user.Email)
            };

            return claims;
        }
    }
}
