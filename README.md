[![](https://img.shields.io/nuget/v/soenneker.apollo.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.apollo.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.apollo.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.apollo.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.apollo.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.apollo.openapiclientutil/)

# Soenneker.Apollo.OpenApiClientUtil

Exposes a cached OpenAPI client instance.

## Install

```bash
dotnet add package Soenneker.Apollo.OpenApiClientUtil
```

## Quick start

```csharp
using Soenneker.Apollo.OpenApiClientUtil.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddApolloOpenApiClientUtilAsSingleton();
```

Adds `ApolloOpenApiClientUtil` as a singleton service.

## What you get

- `IApolloOpenApiClientUtil` — Exposes a cached OpenAPI client instance.
- `ApolloOpenApiClientUtilRegistrar` — Registers the OpenAPI client utility for dependency injection.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `ApolloOpenApiClientUtilRegistrar.AddApolloOpenApiClientUtilAsSingleton(services)` | Adds `ApolloOpenApiClientUtil` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `ApolloOpenApiClientUtilRegistrar.AddApolloOpenApiClientUtilAsScoped(services)` | Adds `ApolloOpenApiClientUtil` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Dispose instances you own when their scope ends so held resources can be released.
