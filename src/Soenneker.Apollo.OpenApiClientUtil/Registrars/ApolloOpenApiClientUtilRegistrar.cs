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
    public static IServiceCollection AddApolloOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddApolloOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IApolloOpenApiClientUtil, ApolloOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="ApolloOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddApolloOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddApolloOpenApiHttpClientAsSingleton()
                .TryAddScoped<IApolloOpenApiClientUtil, ApolloOpenApiClientUtil>();

        return services;
    }
}
