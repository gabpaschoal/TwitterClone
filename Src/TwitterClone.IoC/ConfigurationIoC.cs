using EasyValidation.DependencyInjection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitterClone.Application;
using TwitterClone.Application.Handlers;
using TwitterClone.Application.Services;
using TwitterClone.Domain.Repositories.Cache;
using TwitterClone.Domain.Services;
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

        services.AddEasyValidationValidators(typeof(CustomResultData).Assembly);
        services.AddMediatR(typeof(CustomResultData).Assembly);

        services.AddScoped<IHandlerBus, HandlerBus>();
        services.AddScoped<IEncryptionService, EncryptionService>();

        var encryptionSection = configuration.GetSection("Encryption");
        var encryptionSectionKey = encryptionSection["Key"];
        services.AddScoped<EncryptionModel>(x => new(encryptionSectionKey));

        var cacheSection = configuration.GetSection("CacheConfiguration");
        var cacheSecExpInHours = cacheSection["AbsoluteExpirationInHours"];
        var cacheSecSlidingExpInMin = cacheSection["SlidingExpirationInMinutes"];

        services.AddScoped<CacheConfiguration>(x =>
                    new(AbsoluteExpirationInHours: int.TryParse(cacheSecExpInHours, out int expInHour) ? expInHour : 1,
                         SlidingExpirationInMinutes: int.TryParse(cacheSecSlidingExpInMin, out int expInMin) ? expInMin : 30));
    }
}
