namespace TwitterClone.Domain.Repositories.Cache;

public interface ICacheRepository
{
    ValueTask<bool> TryGet<T>(string cacheKey, out T value);
    ValueTask<T> Set<T>(string cacheKey, T value);
    ValueTask Remove(string cacheKey);
}
