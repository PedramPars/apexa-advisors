using System.Collections.Concurrent;
using ApexaAdvisors.Application.Models;
using ApexaAdvisors.Domain.Contracts;
using ApexaAdvisors.Domain.Models.Dtos;
using Microsoft.Extensions.Options;

namespace ApexaAdvisors.Application.Services;

public class AdvisorCacheService : IAdvisorCacheService
{
    private readonly ICacheService _cacheService;
    private readonly ApplicationSettings _applicationSettings;
    public AdvisorCacheService(IOptions<ApplicationSettings> applicationSettingsOptions, ICacheService cacheService)
    {
        _cacheService = cacheService;
        _applicationSettings = applicationSettingsOptions.Value;
    }

    public AdvisorDetailsResponse? Get(Guid id)
    {
        var cache = _cacheService.Get<ConcurrentDictionary<Guid, AdvisorCache>>(CacheKey.Advisors);
        if (cache is null)
            return null;

        if (!cache.TryGetValue(id, out var item))
            return null;

        return item.Advisor;
    }

    public void Add(AdvisorDetailsResponse advisor)
    {
        var cache = _cacheService.Get<ConcurrentDictionary<Guid, AdvisorCache>>(CacheKey.Advisors);
        cache ??= new ConcurrentDictionary<Guid, AdvisorCache>();

        cache[advisor.Id] = new AdvisorCache
        {
            StoreDate = DateTime.UtcNow,
            Advisor = advisor,
        };

        if (cache.Count > _applicationSettings.AdvisorCacheCapacity)
        {
            var oldest = cache.OrderBy(o => o.Value.StoreDate).FirstOrDefault();
            cache.Remove(oldest.Key, out _);
        }

        _cacheService.Set(CacheKey.Advisors, cache);
    }

    public void Update(AdvisorDetailsResponse advisor)
    {
        var cache = _cacheService.Get<ConcurrentDictionary<Guid, AdvisorCache>>(CacheKey.Advisors);
        cache ??= new ConcurrentDictionary<Guid, AdvisorCache>();

        if (!cache.ContainsKey(advisor.Id))
            return;

        cache[advisor.Id] = new AdvisorCache
        {
            StoreDate = DateTime.UtcNow,
            Advisor = advisor,
        };

        _cacheService.Set(CacheKey.Advisors, cache);
    }

    public void Delete(AdvisorDetailsResponse advisor)
    {
        var cache = _cacheService.Get<ConcurrentDictionary<Guid, AdvisorCache>>(CacheKey.Advisors);
        if (cache is null)
            return;

        cache.Remove(advisor.Id, out _);

        _cacheService.Set(CacheKey.Advisors, cache);
    }
}
