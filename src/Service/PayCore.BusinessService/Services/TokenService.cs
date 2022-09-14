using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NHibernate;
using PayCore.Application.Interfaces.Jwt;
using PayCore.Application.Interfaces.Sessions;
using PayCore.Application.Utilities.Appsettings;
using PayCore.Application.Utilities.Results;
using PayCore.Domain.Entities;
using PayCore.Domain.Jwt;
using PayCore.Infrastructure.Sessions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PayCore.BusinessService.Services
{
    public class TokenService : ITokenService
    {
        private readonly IMapperSession<PersonEntity> _mapperSession;
        private readonly IOptions<PayCoreAppSettings> _payCoreAppSettings;
        public TokenService(ISession session, IOptions<PayCoreAppSettings> payCoreAppSettings)
        {
            _mapperSession = new MapperSession<PersonEntity>(session);
            _payCoreAppSettings = payCoreAppSettings;
        }

        public IDataResult GenerateToken(TokenRequest tokenRequest)
        {
            try
            {
                if (tokenRequest is null)
                {
                    return new DataResult<TokenResponse> { ErrorMessage = "Please enter valid informations." };
                }

                var account = _mapperSession.Where(x => x.UserName.Equals(tokenRequest.UserName)).FirstOrDefault();
                if (account is null)
                {
                    return new DataResult<TokenResponse> { ErrorMessage = "Please validate your informations that you provided." };
                }

                if (!account.Password.Equals(tokenRequest.Password))
                {
                    return new DataResult<TokenResponse> { ErrorMessage = "Please validate your informations that you provided." };
                }

                DateTime now = DateTime.UtcNow;
                try
                {
                    account.LastActivity = now;

                    _mapperSession.BeginTransaction();
                    _mapperSession.Update(account);
                    _mapperSession.Commit();
                    _mapperSession.CloseTransaction();
                }
                catch (Exception ex)
                {
                    _mapperSession.Rollback();
                    _mapperSession.CloseTransaction();
                }
                string token = GetToken(account, now);

            
               account.LastActivity = now;

                   

                TokenResponse tokenResponse = new TokenResponse
                {
                    AccessToken = token,
                    Expiration = now.AddMinutes(_payCoreAppSettings.Value.JwtSettings.TokenExpirationMinute),
                    Role = account.Role,
                    UserName = account.UserName,
                    SessionTimeInSecond = _payCoreAppSettings.Value.JwtSettings.TokenExpirationMinute * 60
                };

                return new SuccessDataResult<TokenResponse> { Data = tokenResponse };
            }
            catch (Exception ex)
            {
                return new ErrorDataResult { ErrorMessage = "GenerateToken Error" };
            }
        }
        private string GetToken(PersonEntity person, DateTime date)
        {
            Claim[] claims = GetClaims(person);
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
        private Claim[] GetClaims(PersonEntity person)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, person.Id.ToString()),
                new Claim(ClaimTypes.Name, person.UserName),
                new Claim(ClaimTypes.Role, person.Role),
                new Claim("AccountId", person.Id.ToString()),
                new Claim("Email",person.Email)
            };

            return claims;
        }
    }
}
