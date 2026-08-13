using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Apollo.HttpClients.Abstract;
using Soenneker.Apollo.OpenApiClientUtil.Abstract;
using Soenneker.Apollo.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Apollo.OpenApiClientUtil;

///<inheritdoc cref="IApolloOpenApiClientUtil"/>
public sealed class ApolloOpenApiClientUtil : IApolloOpenApiClientUtil
{
    private readonly AsyncSingleton<ApolloOpenApiClient> _client;

    public ApolloOpenApiClientUtil(IApolloOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<ApolloOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Apollo:ApiKey");
            string authHeaderName = configuration["Apollo:AuthHeaderName"] ?? "x-api-key";
            string authHeaderValueTemplate = configuration["Apollo:AuthHeaderValueTemplate"] ?? "{token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerName: authHeaderName, headerValue: authHeaderValue),
                httpClient: httpClient);

            return new ApolloOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<ApolloOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
