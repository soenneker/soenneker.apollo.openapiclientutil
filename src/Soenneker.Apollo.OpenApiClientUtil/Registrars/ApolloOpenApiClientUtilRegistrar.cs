using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Apollo.HttpClients.Registrars;
using Soenneker.Apollo.OpenApiClientUtil.Abstract;

namespace Soenneker.Apollo.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class ApolloOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="ApolloOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddApolloOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddApolloOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IApolloOpenApiClientUtil, ApolloOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="ApolloOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddApolloOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddApolloOpenApiHttpClientAsSingleton()
                .TryAddScoped<IApolloOpenApiClientUtil, ApolloOpenApiClientUtil>();

        return services;
    }
}
