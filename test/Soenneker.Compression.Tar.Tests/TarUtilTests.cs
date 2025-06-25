using Soenneker.Compression.Tar.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Compression.Tar.Tests;

[Collection("Collection")]
public sealed class TarUtilTests : FixturedUnitTest
{
    private readonly ITarUtil _util;

    public TarUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<ITarUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
