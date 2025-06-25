using System.Threading;

namespace Soenneker.Compression.Tar.Abstract;

/// <summary>
/// A utility library dealing with Tar compression and decompression
/// </summary>
public interface ITarUtil
{
    void Extract(string filePath, string outputFilePath, CancellationToken cancellationToken = default);
}
