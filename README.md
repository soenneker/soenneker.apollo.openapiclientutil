[![](https://img.shields.io/nuget/v/soenneker.apollo.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.apollo.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.apollo.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.apollo.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.apollo.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.apollo.openapiclientutil/)

# Soenneker.Apollo.OpenApiClientUtil

The recommended DI entry point for the generated Apollo REST API client.

This package combines the generated `ApolloOpenApiClient`, a cached authenticated `HttpClient`, a Kiota request adapter, and lazy client initialization. Application services retrieve one configured client and call its typed request builders.

## Installation

```bash
dotnet add package Soenneker.Apollo.OpenApiClientUtil
```

## Configuration

```json
{
  "Apollo": {
    "ApiKey": "your-api-key"
  }
}
```

`Apollo:ApiKey` is required when the client is first retrieved. Optional transport settings are:

| Key | Default | Purpose |
| --- | --- | --- |
| `Apollo:ClientBaseUrl` | `https://api.apollo.io/api/v1` | Overrides the API base URL. |
| `Apollo:AuthHeaderName` | `x-api-key` | Overrides the authentication header. |
| `Apollo:AuthHeaderValueTemplate` | `{token}` | Formats the header value after replacing `{token}` with the API key. |

Store the API key outside committed configuration by using environment variables, user secrets, or your application's secret provider.

## Registration

```csharp
using Soenneker.Apollo.OpenApiClientUtil.Registrars;

builder.Services.AddApolloOpenApiClientUtilAsSingleton();
```

This also registers the Apollo HTTP client provider and its shared HTTP client cache. A scoped client utility is available when required by the application's dependency graph:

```csharp
builder.Services.AddApolloOpenApiClientUtilAsScoped();
```

## Call Apollo

The following service retrieves the current Apollo user profile and includes credit-usage information:

```csharp
using Soenneker.Apollo.OpenApiClient;
using Soenneker.Apollo.OpenApiClient.Models;
using Soenneker.Apollo.OpenApiClientUtil.Abstract;

public sealed class ApolloProfileService
{
    private readonly IApolloOpenApiClientUtil _clientUtil;

    public ApolloProfileService(IApolloOpenApiClientUtil clientUtil)
    {
        _clientUtil = clientUtil;
    }

    public async Task<GetCurrentUserProfile200Response?> Get(
        CancellationToken cancellationToken)
    {
        ApolloOpenApiClient client = await _clientUtil.Get(cancellationToken);

        return await client.Users.Api_profile.GetAsync(
            request =>
            {
                request.QueryParameters.IncludeCreditUsage = true;
            },
            cancellationToken);
    }
}
```

The generated client exposes request builders for accounts, contacts, conversations, email accounts, campaigns, fields, labels, companies, people, opportunities, organizations, phone calls, reports, sequences, tasks, usage statistics, users, webhook results, and other operations present in Apollo's OpenAPI document.

Kiota throws generated endpoint-specific exceptions for mapped error responses, such as 401 or 403 models. Catch the generated exception type when the application needs response-specific handling; otherwise allow normal exception policy and retries to operate above this service.

## Lifetime and caching

- `Get` initializes the generated client on first use and returns the cached instance afterward.
- The generated client and its `HttpClient` are intended for reuse across calls.
- Authentication and base-address configuration is captured during initialization. Recreate the owning DI scope or service when those values must change.
- Let the dependency-injection container dispose the utility. If you construct it manually, dispose it with `Dispose` or `DisposeAsync`.
- Registration uses `TryAdd`, so an earlier application-provided `IApolloOpenApiClientUtil` is preserved.

The generated API surface can change when Apollo changes its OpenAPI specification. Prefer wrapping calls in application-owned services instead of exposing generated models throughout the entire codebase.

## Related packages

- [`Soenneker.Apollo.OpenApiClient`](https://www.nuget.org/packages/Soenneker.Apollo.OpenApiClient) contains the generated Kiota client and models.
- [`Soenneker.Apollo.HttpClients`](https://www.nuget.org/packages/Soenneker.Apollo.HttpClients) provides the cached authenticated transport.

## API

| Method | Purpose |
| --- | --- |
| `IApolloOpenApiClientUtil.Get(CancellationToken)` | Returns the lazily initialized, cached generated client. |
| `AddApolloOpenApiClientUtilAsSingleton()` | Registers the complete client stack as singleton services. |
| `AddApolloOpenApiClientUtilAsScoped()` | Registers a scoped client utility with the Apollo transport dependencies. |
