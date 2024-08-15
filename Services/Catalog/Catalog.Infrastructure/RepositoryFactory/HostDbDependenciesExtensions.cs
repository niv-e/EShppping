using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;

namespace Catalog.Infrastructure.RepositoryFactory;

public static class HostDbDependenciesExtensions
{
    public static IServiceCollection AddDataAccessor(this IServiceCollection services)
    {
        services.AddSingleton(typeof(MongoCollectionFactory));

        return services;
    }

    public static IHealthChecksBuilder AddDbHealthCheck(this IHealthChecksBuilder healthChecksBuilder,string? connectionString)
    {
        connectionString = connectionString ??
            throw new NullReferenceException("Connections string must be provide for preforming database health check");

        healthChecksBuilder
            .AddMongoDb(
                connectionString,
                "Catalog MongoDB health Check",
                HealthStatus.Degraded);

        return healthChecksBuilder;
    }
}
