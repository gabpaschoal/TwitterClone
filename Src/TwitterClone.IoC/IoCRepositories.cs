using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitterClone.Domain.Repositories.Cache;
using TwitterClone.Domain.Repositories.Data;
using TwitterClone.Domain.Repositories.Data.Base;
using TwitterClone.Infrastructure.Repositories.Cache;
using TwitterClone.Infrastructure.Repositories.Data.Base;

namespace TwitterClone.IoC;

internal class IoCRepositories
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        _ = services
                /* Cache Repositories */
                .AddMemoryCache()

                /* Db Repositories */
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<ICacheRepository, InMemoryCacheRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                ;

        var cacheSection = configuration.GetSection("CacheConfiguration");
        var cacheSecExpInHours = cacheSection["AbsoluteExpirationInHours"];
        var cacheSecSlidingExpInMin = cacheSection["SlidingExpirationInMinutes"];

        _ = services.AddScoped<CacheConfiguration>(x =>
                    new(AbsoluteExpirationInHours: int.TryParse(cacheSecExpInHours, out int expInHour) ? expInHour : 1,
                         SlidingExpirationInMinutes: int.TryParse(cacheSecSlidingExpInMin, out int expInMin) ? expInMin : 30));
    }
}
