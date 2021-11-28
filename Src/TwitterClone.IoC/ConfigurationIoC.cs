using EasyValidation.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TwitterClone.Application;
using TwitterClone.Application.Handlers;
using TwitterClone.Application.Services;
using TwitterClone.Domain.Services;
using TwitterClone.Domain.Services.Models;
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

        IoCRepositories.Configure(services, configuration);

        services.AddEasyValidationValidators(typeof(CustomResultData).Assembly);
        services.AddMediatR(typeof(CustomResultData).Assembly);

        /* Handler Bus */
        services.AddScoped<IHandlerBus, HandlerBus>();

        /* Encrypt Service */
        services.AddScoped<IEncryptionService, EncryptionService>();
        /* Encrypt Service Model */
        var encryptionSection = configuration.GetSection("Encryption");
        var encryptionSectionKey = encryptionSection["Key"];
        services.AddScoped<EncryptionModel>(x => new(encryptionSectionKey));

        /* JWT Authentication */
        var secretTokenSection = configuration.GetSection("SecretToken");
        var secretTokenKey = secretTokenSection["Key"];
        var secretTokenHoursToExpire = int.TryParse(secretTokenSection["HoursToExpire"], out var HoursToExpire) ? HoursToExpire : 1;
        var secretTokenSectionKeyBytes = Encoding.ASCII.GetBytes(secretTokenKey);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretTokenSectionKeyBytes),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        services.AddScoped<JWTEncriptionModel>(x => new(secretTokenKey, secretTokenHoursToExpire));
    }
}
