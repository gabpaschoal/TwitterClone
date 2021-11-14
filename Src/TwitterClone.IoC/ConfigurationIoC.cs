using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitterClone.Infrastructure.Contexts;

namespace TwitterClone.IoC;

public static class ConfigurationIoC
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("Dev"));
            opt.EnableSensitiveDataLogging();
        });

        IoCRepositories.Configure(services);
    }
}
