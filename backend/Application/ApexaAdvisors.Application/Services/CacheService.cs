using ApexaAdvisors.Domain.Contracts;
using Microsoft.Extensions.Caching.Memory;

namespace ApexaAdvisors.Application.Services;

public class CacheService : ICacheService
{
    private readonly IMemoryCache _cache;

    public CacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void Set<T>(string key, T value)
    {

        _cache.Set(key, value);
    }

    public T? Get<T>(string key)
    {
        _cache.TryGetValue(key, out T? value);
        return value;
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }
}
