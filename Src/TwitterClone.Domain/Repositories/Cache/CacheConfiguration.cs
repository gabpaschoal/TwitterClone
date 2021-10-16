namespace TwitterClone.Domain.Repositories.Cache;

public class CacheConfiguration
{
    public int AbsoluteExpirationInHours { get; set; }
    public int SlidingExpirationInMinutes { get; set; }
}
