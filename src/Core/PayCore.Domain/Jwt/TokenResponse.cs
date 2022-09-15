
namespace PayCore.Domain.Jwt
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
        public string Role { get; set; }
        public int SessionTimeInSecond { get; set; }
    }
}
