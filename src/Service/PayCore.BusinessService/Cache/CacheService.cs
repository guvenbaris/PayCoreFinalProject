using Microsoft.Extensions.Options;
using PayCore.Application.Interfaces.Cache;
using PayCore.Application.Utilities.Appsettings;

namespace PayCore.BusinessService.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IOptions<PayCoreAppSettings> _payCoreAppSettings;
        public CacheService(IOptions<PayCoreAppSettings> payCoreAppSettings)
        {
            _payCoreAppSettings = payCoreAppSettings;

            var deneme = payCoreAppSettings.Value.RedisSettings.SlidingExpirationHours;
        }

        public void Delete(string key)
        {
            throw new NotImplementedException();
        }

        public void DeleteIfContainName(string name)
        {
            throw new NotImplementedException();
        }

        public void FlushAll()
        {
            throw new NotImplementedException();
        }

        public string GetByKey(string key)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetByKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public void InsertValue(string key, string value, int slidingExpirationHours = 0, int ablosuteExpirationHours = 0)
        {
            throw new NotImplementedException();
        }

        public Task InsertValueAsync(string key, string value, int slidingExpirationHours = 0, int ablosuteExpirationHours = 0)
        {
            throw new NotImplementedException();
        }
    }
}
