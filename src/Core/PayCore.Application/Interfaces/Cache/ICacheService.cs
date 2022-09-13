namespace PayCore.Application.Interfaces.Cache;

public interface ICacheService
{
    Task InsertValueAsync(string key, string value, int slidingExpirationHours = 0, int ablosuteExpirationHours = 0);
    void InsertValue(string key, string value, int slidingExpirationHours = 0, int ablosuteExpirationHours = 0);
    Task<string> GetByKeyAsync(string key);
    string GetByKey(string key);
    void Delete(string key);
    void FlushAll();
    void DeleteIfContainName(string name);
}