using Microsoft.Extensions.DependencyInjection;
using TwitterClone.Domain.Repositories.Data.Base;
using TwitterClone.Infrastructure.Repositories.Data.Base;

namespace TwitterClone.IoC;

internal class IoCRepositories
{
    public static void Configure(IServiceCollection services)
    {
        _ = services
                /* Cache Repositories */
                .AddMemoryCache()

                /* Db Repositories */
                .AddScoped<IUnitOfWork, UnitOfWork>()
                ;
    }
}
