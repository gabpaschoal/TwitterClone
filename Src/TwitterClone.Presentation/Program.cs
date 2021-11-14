using TwitterClone.IoC;
using TwitterClone.Presentation.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

ConfigurationIoC.Configure(builder.Services, builder.Configuration);

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(UoWActionFilter));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
