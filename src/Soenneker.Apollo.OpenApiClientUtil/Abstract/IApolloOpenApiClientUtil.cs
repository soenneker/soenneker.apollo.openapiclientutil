using Soenneker.Apollo.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Soenneker.Apollo.OpenApiClientUtil.Abstract;
/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IApolloOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Returns the configured apollo OpenAPI Client used by the Apollo OpenAPI Client.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested apollo OpenAPI Client.</returns>
    ValueTask<ApolloOpenApiClient> Get(CancellationToken cancellationToken = default);
}
