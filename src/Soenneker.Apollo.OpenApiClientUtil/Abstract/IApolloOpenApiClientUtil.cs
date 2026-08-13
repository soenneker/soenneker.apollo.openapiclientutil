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
    ValueTask<ApolloOpenApiClient> Get(CancellationToken cancellationToken = default);
}
