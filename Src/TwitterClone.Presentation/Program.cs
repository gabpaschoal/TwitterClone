using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using TwitterClone.IoC;
using TwitterClone.Presentation.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

ConfigurationIoC.Configure(builder.Services, builder.Configuration);

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(UoWActionFilter));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.DocExpansion(DocExpansion.None); });
}

var suportedCultures = new[] { new System.Globalization.CultureInfo("en-US"), new System.Globalization.CultureInfo("pt-BR") };

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US"),
    SupportedCultures = suportedCultures,
    SupportedUICultures = suportedCultures,
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
