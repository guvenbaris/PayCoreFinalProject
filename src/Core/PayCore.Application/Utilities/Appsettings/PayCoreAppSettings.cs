﻿
namespace PayCore.Application.Utilities.Appsettings
{
    public class PayCoreAppSettings
    {
        public RedisSettings RedisSettings { get; set; }
        public JwtSettings JwtSettings { get; set; }

    }
    public class RedisSettings
    {
        public string? Host { get; set; }
        public string? Port { get; set; }
        public int SlidingExpirationHours { get; set; }
        public int AbsoluteExpirationHours { get; set; }
    }
    public class JwtSettings
    {

        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Key { get; set; }
        public  int TokenExpirationMinute { get; set; }
    }
}
