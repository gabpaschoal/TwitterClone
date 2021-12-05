namespace TwitterClone.Domain.Repositories.Cache;

public record CacheConfiguration(int AbsoluteExpirationInHours, int SlidingExpirationInMinutes);
