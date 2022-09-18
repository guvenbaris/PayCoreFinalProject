using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PayCore.Application.Exceptions;
using PayCore.Application.Interfaces.Cache;
using PayCore.Application.Utilities.Appsettings;
using StackExchange.Redis;
using System.Net;
using System.Text;

namespace PayCore.BusinessService.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IOptions<PayCoreAppSettings> _payCoreAppSettings;
        private readonly IDistributedCache _distributedCache;
        private readonly IServer _server;

        public CacheService(IDistributedCache distributedCache, IOptions<PayCoreAppSettings> payCoreAppSettings)
        {
            ConfigurationOptions configurationOptions = new ConfigurationOptions()
            {
                EndPoints = { $"{ payCoreAppSettings.Value.RedisSettings.Host }:{payCoreAppSettings.Value.RedisSettings.Port} " },
                AllowAdmin = true,
                ConnectTimeout = 60 * 1000,
            };

            _server = ConnectionMultiplexer.Connect(configurationOptions).GetServer($"{payCoreAppSettings.Value.RedisSettings.Host}:{payCoreAppSettings.Value.RedisSettings.Port}");

            _payCoreAppSettings = payCoreAppSettings;
            _distributedCache = distributedCache;
        }
       
        public void Delete(string key)
        {
            try
            {
                _distributedCache.Remove(key);
            }
            catch (Exception)
            {
                throw new CustomException("Redis server error",HttpStatusCode.InternalServerError);
            }
        }

        public void DeleteIfContainName(string name)
        {
            foreach (var key in _server.Keys())
            {
                if (key.ToString().Contains(name))
                {
                    _distributedCache.Remove(key);
                }
            }
        }

        public void FlushAll()
        {
            try
            {
                _server.FlushDatabase();
            }
            catch (Exception)
            {
                throw new CustomException("Redis server error", HttpStatusCode.InternalServerError);
            }
        }

        public string GetByKey(string key)
        {
            try
            {
                var cacheData = _distributedCache.Get(key);
                if (cacheData != null)
                {
                    var array = Encoding.UTF8.GetString(cacheData);
                    var data = JsonConvert.DeserializeObject<string>(array);
                    return data;
                }
                return string.Empty;
            }
            catch (Exception)
            {
                throw new CustomException("Redis server error", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<string> GetByKeyAsync(string key)
        {
            try
            {
                var cacheData = await _distributedCache.GetAsync(key);
                if (cacheData != null)
                {
                    var array = Encoding.UTF8.GetString(cacheData);
                    var data = JsonConvert.DeserializeObject<string>(array);
                    return data;
                }
                return string.Empty;
            }
            catch (Exception)
            {
                throw new CustomException("Redis server error", HttpStatusCode.InternalServerError);
            }
        }

        public void InsertValue(string key, string value, int slidingExpirationHours = 0, int ablosuteExpirationHours = 0)
        {
            try
            {
                slidingExpirationHours = slidingExpirationHours == 0 ? _payCoreAppSettings.Value.RedisSettings.SlidingExpirationHours : slidingExpirationHours;
                ablosuteExpirationHours = ablosuteExpirationHours == 0 ? _payCoreAppSettings.Value.RedisSettings.AbsoluteExpirationHours : ablosuteExpirationHours;

                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddHours(ablosuteExpirationHours))
                    .SetSlidingExpiration(TimeSpan.FromHours(slidingExpirationHours));

                _distributedCache.Set(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), options);
            }
            catch (Exception)
            {
                throw new CustomException("Redis server error", HttpStatusCode.InternalServerError);
            }
        }

        public async Task InsertValueAsync(string key, string value, int slidingExpirationHours = 0, int ablosuteExpirationHours = 0)
        {
            try
            {
                slidingExpirationHours = slidingExpirationHours == 0 ? _payCoreAppSettings.Value.RedisSettings.SlidingExpirationHours : slidingExpirationHours;
                ablosuteExpirationHours = ablosuteExpirationHours == 0 ? _payCoreAppSettings.Value.RedisSettings.AbsoluteExpirationHours : ablosuteExpirationHours;

                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddHours(ablosuteExpirationHours))
                    .SetSlidingExpiration(TimeSpan.FromHours(slidingExpirationHours));

                await _distributedCache.SetAsync(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), options);
            }
            catch (Exception)
            {
                throw new CustomException("Redis server error", HttpStatusCode.InternalServerError);
            }
        }
    }
}
