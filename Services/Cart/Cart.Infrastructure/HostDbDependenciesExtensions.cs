
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cart.Infrastructure;

public static class HostDbDependenciesExtensions
{
    public static IServiceCollection AddDataAccessor(this IServiceCollection services, string? connectionString)
    {
        connectionString = connectionString ??
            throw new NullReferenceException("Connections string must be provide for establish database connection");

        services.AddStackExchangeRedisCache(setup =>
        {
            setup.Configuration = connectionString;
        });

        return services;
    }

    public static IHealthChecksBuilder AddDbHealthCheck(this IHealthChecksBuilder healthChecksBuilder, string? connectionString)
    {
        connectionString = connectionString ??
            throw new NullReferenceException("Connections string must be provide for preforming database health check");

        healthChecksBuilder
            .AddRedis( 
                connectionString,
                "Card Redis health Check",
                HealthStatus.Degraded);

        return healthChecksBuilder;
    }
}
