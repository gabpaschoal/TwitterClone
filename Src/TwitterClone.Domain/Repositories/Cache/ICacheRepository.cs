namespace TwitterClone.Domain.Repositories.Cache;

public interface ICacheRepository
{
    ValueTask<bool> TryGetAsync<T>(string cacheKey, out T value);
    ValueTask<T> SetAsync<T>(string cacheKey, T value);
    ValueTask RemoveAsync(string cacheKey);
}
