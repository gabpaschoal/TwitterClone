using EasyValidation.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Authentication;
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
        services.AddScoped<ITokenService, TokenService>();

        /* Encrypt Service Model */
        services.Configure<EncryptionModel>(configuration.GetSection("Encryption"));

        /* JWT Authentication */
        services.Configure<JWTEncriptionModel>(configuration.GetSection("SecretToken"));
        string keySecretToken = configuration.GetSection("SecretToken")["Key"];
        services.AddAuthentication(x => JWTSchemes(x))
                .AddJwtBearer(x => JWTSetting(x, keySecretToken));

        static void JWTSetting(JwtBearerOptions x, string keySecretToken)
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(keySecretToken)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }

        static void JWTSchemes(AuthenticationOptions x)
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
