using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using TwitterClone.Domain.Repositories.Cache;

namespace TwitterClone.Infrastructure.Repositories.Cache;

public class InMemoryCacheRepository : ICacheRepository
{
    private readonly IMemoryCache _memoryCache;
    private readonly MemoryCacheEntryOptions? _cacheOptions;

    public InMemoryCacheRepository(
        IMemoryCache memoryCache,
        IOptions<CacheConfiguration> cacheConfig)
    {
        _memoryCache = memoryCache;

        if (cacheConfig.Value is null)
            return;

        _cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.AddHours(cacheConfig.Value.AbsoluteExpirationInHours),
            Priority = CacheItemPriority.High,
            SlidingExpiration = TimeSpan.FromMinutes(cacheConfig.Value.SlidingExpirationInMinutes)
        };
    }

    public ValueTask Remove(string cacheKey)
    {
        _memoryCache.Remove(cacheKey);
        return ValueTask.CompletedTask;
    }

    public ValueTask<T> Set<T>(string cacheKey, T value)
    {
        return ValueTask.FromResult(_memoryCache.Set(cacheKey, value, _cacheOptions));
    }

    public ValueTask<bool> TryGet<T>(string cacheKey, out T value)
    {
        _memoryCache.TryGetValue(cacheKey, out value);

        return ValueTask.FromResult(value is not null);
    }
}

