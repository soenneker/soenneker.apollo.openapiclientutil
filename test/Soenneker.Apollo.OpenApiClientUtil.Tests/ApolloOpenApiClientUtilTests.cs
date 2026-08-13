using Soenneker.Apollo.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Apollo.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class ApolloOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IApolloOpenApiClientUtil _openapiclientutil;

    public ApolloOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IApolloOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
