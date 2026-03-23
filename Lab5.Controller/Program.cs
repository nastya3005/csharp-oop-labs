using Lab5.Application;
using Lab5.Application.Common.Interfaces;
using Lab5.Controller;
using Lab5.Infrastructure;
using Lab5.Infrastructure.Settings;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ATM API",
        Version = "v1",
        Description = "API for ATM system",
    });
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddScoped<ISettingsProvider>(_ =>
    new SettingsProvider(PresentationSettings.AdminPassword, PresentationSettings.SessionTimeoutMinutes));

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ATM API v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();