using Soenneker.Apollo.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Soenneker.Apollo.OpenApiClientUtil.Abstract;
/// <summary>
/// Creates and caches an authenticated <see cref="ApolloOpenApiClient"/>.
/// </summary>
public interface IApolloOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the cached generated client, creating it on first use.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>The cached Apollo client.</returns>
    ValueTask<ApolloOpenApiClient> Get(CancellationToken cancellationToken = default);
}
