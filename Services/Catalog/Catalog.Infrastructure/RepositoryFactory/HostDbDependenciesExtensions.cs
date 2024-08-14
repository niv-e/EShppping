using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;

namespace Catalog.Infrastructure.RepositoryFactory;

public static class HostDbDependenciesExtensions
{
    public static IServiceCollection AddDataAccessor(this IServiceCollection services)
    {
        services.AddScoped(typeof(MongoCollectionFactory));

        return services;
    }

    public static IServiceCollection AddDbHealthCheck(this IServiceCollection services,string? connectionString)
    {
        connectionString = connectionString ??
            throw new NullReferenceException("Connections string must be provide for preforming database health check");
        
        services.AddHealthChecks()
            .AddMongoDb(
                connectionString,
                "Catalog MongoDB health Check",
                HealthStatus.Degraded);

        return services;
    }
}
