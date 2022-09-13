
namespace PayCore.Application.Utilities.Appsettings
{
    public class PayCoreAppSettings
    {
        public RedisSettings RedisSettings { get; set; }

    }
    public class RedisSettings
    {
        public string? Host { get; set; }
        public string? Port { get; set; }
        public int SlidingExpirationHours { get; set; }
        public int AbsoluteExpirationHours { get; set; }
    }
}
