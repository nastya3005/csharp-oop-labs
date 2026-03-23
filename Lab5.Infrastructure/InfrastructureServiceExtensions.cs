using Lab5.Application.Interfaces.Repositories;
using Lab5.Infrastructure.Data;
using Lab5.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Lab5.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<InMemoryDataContext>();

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
}