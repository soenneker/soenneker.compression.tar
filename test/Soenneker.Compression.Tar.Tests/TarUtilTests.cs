using Soenneker.Compression.Tar.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Compression.Tar.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class TarUtilTests : HostedUnitTest
{
    private readonly ITarUtil _util;

    public TarUtilTests(Host host) : base(host)
    {
        _util = Resolve<ITarUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
